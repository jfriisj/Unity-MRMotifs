@echo off
REM Metrics Collection Script for Unity-MRMotifs (Windows Batch)
REM Collects metrics data from Meta Quest 3 headsets
REM Usage: collect_metrics.bat [session_name]

setlocal enabledelayedexpansion

REM Configuration
set "PACKAGE_NAME=com.Prototype.MRMotifs"
set "METRICS_PATH=/sdcard/Android/data/%PACKAGE_NAME%/files/metrics"
set "ADB_PATH=C:\Program Files\Unity\Hub\Editor\2022.3.62f2\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools\adb.exe"

REM Session name (default to timestamp if not provided)
if "%~1"=="" (
    for /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c%%a%%b)
    for /f "tokens=1-2 delims=/: " %%a in ('time /t') do (set mytime=%%a%%b)
    set "SESSION_NAME=session_!mydate!_!mytime!"
) else (
    set "SESSION_NAME=%~1"
)

set "OUTPUT_DIR=.\metrics-data\%SESSION_NAME%"

echo === Unity-MRMotifs Metrics Collection ===
echo Session: %SESSION_NAME%
echo Output Directory: %OUTPUT_DIR%
echo.

REM Check if ADB exists
if not exist "%ADB_PATH%" (
    echo Error: ADB not found at %ADB_PATH%
    echo Please update ADB_PATH in the script to match your Unity installation.
    pause
    exit /b 1
)

REM Get list of connected devices
"%ADB_PATH%" devices > temp_devices.txt
set DEVICE_COUNT=0
for /f "skip=1 tokens=1" %%a in (temp_devices.txt) do (
    if not "%%a"=="List" if not "%%a"=="" (
        set /a DEVICE_COUNT+=1
        set "DEVICE_!DEVICE_COUNT!=%%a"
    )
)
del temp_devices.txt

if %DEVICE_COUNT%==0 (
    echo No devices found!
    echo Please connect Quest headsets via USB and enable USB debugging.
    pause
    exit /b 1
)

echo Found %DEVICE_COUNT% device^(s^)
echo.

REM Create output directory
if not exist "%OUTPUT_DIR%" mkdir "%OUTPUT_DIR%"

REM Collect data from each device
for /l %%i in (1,1,%DEVICE_COUNT%) do (
    set "SERIAL=!DEVICE_%%i!"
    set "DEVICE_DIR=%OUTPUT_DIR%\H%%i"
    
    echo === Collecting from Device H%%i ^(!SERIAL!^) ===
    
    REM Create device directory
    if not exist "!DEVICE_DIR!" mkdir "!DEVICE_DIR!"
    
    REM Pull metrics files
    echo   Pulling metrics files...
    "%ADB_PATH%" -s !SERIAL! pull "%METRICS_PATH%/" "!DEVICE_DIR!\" 2>nul
    
    REM Pull Unity logcat
    echo   Pulling Unity logs...
    "%ADB_PATH%" -s !SERIAL! logcat -d -s Unity:* > "!DEVICE_DIR!\unity_logcat.txt" 2>nul
    
    REM Get device info
    echo   Collecting device info...
    "%ADB_PATH%" -s !SERIAL! shell getprop ro.product.model > temp_model.txt 2>nul
    "%ADB_PATH%" -s !SERIAL! shell getprop ro.build.version.release > temp_android.txt 2>nul
    
    (
        echo Device Serial: !SERIAL!
        type temp_model.txt 2>nul
        type temp_android.txt 2>nul
        echo Collection Time: %date% %time%
    ) > "!DEVICE_DIR!\device_info.txt"
    
    del temp_model.txt temp_android.txt 2>nul
    
    REM Get battery info
    echo   Collecting battery stats...
    "%ADB_PATH%" -s !SERIAL! shell dumpsys battery > "!DEVICE_DIR!\battery_info.txt" 2>nul
    
    REM Count collected files
    set CSV_COUNT=0
    for %%f in ("!DEVICE_DIR!\*.csv") do set /a CSV_COUNT+=1
    set JSON_COUNT=0
    for %%f in ("!DEVICE_DIR!\*.json") do set /a JSON_COUNT+=1
    
    if !CSV_COUNT!==0 (
        echo   Warning: No CSV files found for H%%i
        echo   Make sure MetricsLogger is enabled and the app has been running.
    ) else (
        echo   Collected !CSV_COUNT! CSV and !JSON_COUNT! JSON files
    )
    
    echo   Done with H%%i
    echo.
)

REM Create collection summary with file listings
set "SUMMARY_FILE=%OUTPUT_DIR%\collection_summary.txt"
(
    echo === Metrics Collection Summary ===
    echo Session: %SESSION_NAME%
    echo Collection Time: %date% %time%
    echo Devices Collected: %DEVICE_COUNT%
    echo.
    echo Files Collected:
) > "%SUMMARY_FILE%"

REM Add file counts per device
for /l %%i in (1,1,%DEVICE_COUNT%) do (
    set "DEVICE_DIR=%OUTPUT_DIR%\H%%i"
    if exist "!DEVICE_DIR!" (
        echo   H%%i: >> "%SUMMARY_FILE%"
        dir /b "!DEVICE_DIR!\*.csv" 2>nul | find /c /v "" > temp_count.txt
        set /p CSV_COUNT=<temp_count.txt
        dir /b "!DEVICE_DIR!\*.json" 2>nul | find /c /v "" > temp_count.txt
        set /p JSON_COUNT=<temp_count.txt
        echo     !CSV_COUNT! CSV files, !JSON_COUNT! JSON files >> "%SUMMARY_FILE%"
        echo     Files: >> "%SUMMARY_FILE%"
        for %%f in ("!DEVICE_DIR!\*.csv" "!DEVICE_DIR!\*.json") do (
            if exist "%%f" echo       - %%~nxf >> "%SUMMARY_FILE%"
        )
        del temp_count.txt 2>nul
    )
)

echo. >> "%SUMMARY_FILE%"
echo Data saved to: %OUTPUT_DIR% >> "%SUMMARY_FILE%"

echo === Collection Complete ===
type "%SUMMARY_FILE%"
echo.

REM Check for incremental part files
set HAS_PARTS=0
for /r "%OUTPUT_DIR%" %%f in (*part*.csv) do set HAS_PARTS=1

if %HAS_PARTS%==1 (
    echo Note: Found incremental part files ^(from auto-saves^)
    echo These files contain non-overlapping data segments.
    echo.
)

echo Next steps:
echo   1. Review the collected CSV files in each H* directory
if %HAS_PARTS%==1 (
    echo   2. Merge incremental parts: python scripts\merge_metrics.py %OUTPUT_DIR%
    echo   3. Run analysis: python scripts\analyze_metrics.py %OUTPUT_DIR%
    echo   4. Check device_info.txt and battery_info.txt for device status
) else (
    echo   2. Run analysis: python scripts\analyze_metrics.py %OUTPUT_DIR%
    echo   3. Check device_info.txt and battery_info.txt for device status
)
echo.
pause
