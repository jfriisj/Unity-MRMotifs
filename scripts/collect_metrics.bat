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
    
    echo   Done with H%%i
    echo.
)

REM Create collection summary
set "SUMMARY_FILE=%OUTPUT_DIR%\collection_summary.txt"
(
    echo === Metrics Collection Summary ===
    echo Session: %SESSION_NAME%
    echo Collection Time: %date% %time%
    echo Devices Collected: %DEVICE_COUNT%
    echo.
    echo Data saved to: %OUTPUT_DIR%
) > "%SUMMARY_FILE%"

echo === Collection Complete ===
type "%SUMMARY_FILE%"
echo.
echo Next steps:
echo   1. Review the collected CSV files in each H* directory
echo   2. Check device_info.txt and battery_info.txt for device status
echo   3. Run analysis script if available
echo.
pause
