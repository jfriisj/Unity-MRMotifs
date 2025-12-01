# Metrics Data Collection Guide

## Quick Start

### Option 1: Using Bash Script (Recommended for Git Bash)

```bash
cd /c/github/Unity-MRMotifs
chmod +x scripts/collect_metrics.sh
./scripts/collect_metrics.sh my_session_name
```

### Option 2: Using Batch Script (Windows CMD/PowerShell)

```cmd
cd C:\github\Unity-MRMotifs
scripts\collect_metrics.bat my_session_name
```

### Option 3: Manual Collection

```bash
# Set ADB path
export ADB="/c/Program Files/Unity/Hub/Editor/2022.3.62f2/Editor/Data/PlaybackEngines/AndroidPlayer/SDK/platform-tools/adb.exe"

# Check connected devices
"$ADB" devices

# Pull metrics from each headset
MSYS_NO_PATHCONV=1 "$ADB" -s <DEVICE_SERIAL> pull /sdcard/Android/data/com.Prototype.MRMotifs/files/metrics ./metrics-data/
```

---

## Pre-Collection Checklist

### Before Starting a Session

- [ ] All headsets fully charged (>95%)
- [ ] USB debugging enabled on all headsets
- [ ] MetricsLogger component active in Unity scene
- [ ] WiFi network configured and stable
- [ ] Physical space prepared with markers

### During Session

- [ ] Monitor battery levels (keep >20%)
- [ ] Check for thermal warnings (>40°C)
- [ ] Verify network latency (<10ms)
- [ ] Note any tracking issues or incidents
- [ ] Record manual observation data

### After Session

- [ ] Let headsets cool down before charging
- [ ] Collect metrics immediately (before device reset)
- [ ] Verify CSV files contain data
- [ ] Backup to multiple locations

---

## Collection Workflow

### Step 1: Connect Headsets

**USB Connection:**
1. Connect Quest headsets to PC via USB-C cables
2. Put on headset and allow USB debugging when prompted
3. Verify connection:
   ```bash
   "$ADB" devices
   ```
   You should see: `<SERIAL>  device`

**Wireless ADB (Optional):**
```bash
# Enable wireless debugging on headset first
"$ADB" tcpip 5555
"$ADB" connect <HEADSET_IP>:5555
```

### Step 2: Run Collection Script

**Automatic (Recommended):**
```bash
./scripts/collect_metrics.sh session_$(date +%Y%m%d_%H%M%S)
```

**What it collects:**
- ✓ All CSV metrics files
- ✓ Session metadata JSON
- ✓ Unity log output
- ✓ Device information
- ✓ Battery stats
- ✓ Thermal zone data

### Step 3: Verify Data

**Check collected files:**
```bash
ls -R ./metrics-data/<session_name>/
```

**Expected structure:**
```
metrics-data/
└── session_20251130_143000/
    ├── H1/
    │   ├── metrics/
    │   │   ├── session_1_H1_20251130_143000.csv
    │   │   └── session_1_H1_metadata_20251130_143000.json
    │   ├── device_info.txt
    │   ├── battery_info.txt
    │   ├── thermal_zones.txt
    │   └── unity_logcat.txt
    ├── H2/ (same structure)
    ├── H3/ (same structure)
    └── collection_summary.txt
```

**Validate CSV files:**
```bash
# Check row count
wc -l metrics-data/<session>/H*/metrics/*.csv

# Preview data
head -20 metrics-data/<session>/H1/metrics/*.csv
```

---

## Data Quality Checks

### Immediate Checks (After Collection)

**1. File Existence:**
```bash
# Should find CSV and JSON for each headset
find ./metrics-data/<session>/ -name "*.csv" -o -name "*.json"
```

**2. Data Completeness:**
```bash
# Check CSV has headers and data rows
for file in ./metrics-data/<session>/H*/metrics/*.csv; do
    lines=$(wc -l < "$file")
    echo "$file: $lines lines"
    if [ $lines -lt 10 ]; then
        echo "  ⚠️  Warning: File has very few rows"
    fi
done
```

**3. Timestamp Continuity:**
```bash
# Check for gaps in timestamps (should be ~1 second intervals)
awk -F',' 'NR>1 {if(prev) {diff=$3-prev; if(diff>5) print "Gap at line " NR ": " diff " seconds"} prev=$3}' \
    metrics-data/<session>/H1/metrics/*.csv
```

### Metrics Validation

**Expected Ranges:**
- `frame_rate_fps`: 60-120 (Quest 3: 72/90/120 Hz modes)
- `frame_time_ms`: 8-16 ms typical
- `network_latency_ms`: 0 (if no Fusion session) or 5-50 ms
- `calibration_error_mm`: 0-10 mm (good), 10-50 mm (acceptable)
- `headset_temp_c`: 30-42°C normal, >42°C thermal throttling
- `battery_level_pct`: 0-100
- `battery_temp_c`: 30-45°C normal
- `cpu_usage_pct`: 20-80% typical
- `memory_usage_mb`: 500-2000 MB typical

**Quick validation script:**
```bash
awk -F',' 'NR>1 {
    if($4<30 || $4>130) print "FPS out of range: line " NR ": " $4;
    if($9>45) print "High temp: line " NR ": " $9 "°C";
    if($10<10) print "Low battery: line " NR ": " $10 "%";
}' metrics-data/<session>/H1/metrics/*.csv
```

---

## Device Information Collection

### Battery Health Check

```bash
# Extract battery info for each headset
for i in 1 2 3; do
    echo "=== Headset H$i ==="
    grep -E "level|temperature|health|status" metrics-data/<session>/H$i/battery_info.txt
    echo ""
done
```

**Key fields:**
- `level`: Battery percentage (0-100)
- `temperature`: Battery temp in tenths of °C (e.g., 350 = 35.0°C)
- `health`: Battery condition (2 = Good)
- `status`: Charging status (2 = Charging, 3 = Discharging)

### Thermal Status

```bash
# Show thermal zones and temperatures
paste \
    <(cat metrics-data/<session>/H1/thermal_types.txt) \
    <(cat metrics-data/<session>/H1/thermal_zones.txt | awk '{print $1/1000 "°C"}')
```

**Important zones for Quest 3:**
- `cpu-*`: CPU core temperatures
- `gpu-*`: GPU temperatures  
- `battery`: Battery sensor
- `skin-*`: Surface/thermal limit sensors

---

## Troubleshooting

### Issue: "No devices found"

**Solutions:**
1. Check USB cable is connected and data-capable (not charge-only)
2. Enable Developer Mode on Quest:
   - Settings → System → Developer → Enable Developer Mode
3. Allow USB debugging when prompted on headset
4. Try different USB port (prefer USB 3.0)
5. Restart ADB server:
   ```bash
   "$ADB" kill-server
   "$ADB" start-server
   "$ADB" devices
   ```

### Issue: "Metrics directory not found"

**Solutions:**
1. Verify app was actually run on the headset
2. Check MetricsLogger is enabled in Unity scene
3. Ensure app has storage permissions
4. Check app didn't crash (review unity_logcat.txt)
5. Manually check path:
   ```bash
   "$ADB" shell ls -la /sdcard/Android/data/com.Prototype.MRMotifs/files/metrics
   ```

### Issue: "Empty or incomplete CSV files"

**Solutions:**
1. Session was too short (need at least 10 seconds of runtime)
2. OnApplicationQuit didn't fire (app was force-killed)
3. Use manual save during session:
   - Call `MetricsLogger.Instance.SaveMetricsNow()` from debug menu
4. Check Unity logs for errors:
   ```bash
   grep -i error metrics-data/<session>/H*/unity_logcat.txt
   ```

### Issue: "Git Bash path conversion errors"

**Solution:**
Always use `MSYS_NO_PATHCONV=1` prefix:
```bash
MSYS_NO_PATHCONV=1 "$ADB" pull /sdcard/Android/data/...
```

### Issue: "Permission denied accessing /sdcard"

**Solutions:**
1. App must be installed and run at least once
2. Check Android 11+ scoped storage permissions
3. Try accessing via app-specific path only
4. Grant storage permissions manually:
   ```bash
   "$ADB" shell pm grant com.Prototype.MRMotifs android.permission.WRITE_EXTERNAL_STORAGE
   ```

---

## Advanced Collection

### Real-Time Monitoring

Monitor metrics while session is running:
```bash
# Tail logs in real-time
"$ADB" -s <SERIAL> logcat -s Unity:* MetricsLogger:*

# Check current temperature
"$ADB" shell cat /sys/class/thermal/thermal_zone0/temp

# Monitor battery
watch -n 5 '"$ADB" shell dumpsys battery | grep -E "level|temperature"'
```

### Mid-Session Data Pull

Without stopping the app:
```bash
# Trigger save from adb
"$ADB" shell am broadcast -a com.Prototype.MRMotifs.SAVE_METRICS

# Or pull current data
MSYS_NO_PATHCONV=1 "$ADB" pull /sdcard/Android/data/com.Prototype.MRMotifs/files/metrics ./backup_$(date +%H%M%S)/
```

### Network Performance Logging

```bash
# Continuous ping between headsets (requires root or network tools app)
"$ADB" shell ping -i 1 <OTHER_HEADSET_IP> > network_latency.log &
```

---

## Data Organization Best Practices

### Naming Convention

```
session_YYYYMMDD_HHMMSS/
├── H1/  (Host/Player 1)
├── H2/  (Client/Player 2)
├── H3/  (Client/Player 3)
└── collection_summary.txt
```

### Metadata Documentation

Create `session_notes.txt` for each session:
```bash
cat > metrics-data/<session>/session_notes.txt <<EOF
Session ID: <session_id>
Date: $(date +%Y-%m-%d)
Start Time: <HH:MM:SS>
End Time: <HH:MM:SS>
Duration: <XX> minutes

Scenario: <scenario_name>
Interface Type: <baseline/wim/ar_annotations>

Participants:
- H1: <Name> (Serial: <SERIAL>)
- H2: <Name> (Serial: <SERIAL>)
- H3: <Name> (Serial: <SERIAL>)

Environment:
- Room Temp: <XX>°C
- Humidity: <XX>%
- Lighting: <good/poor/artificial>
- Network: WiFi 6E, Channel <XX>

Incidents:
- <Time>: <Description>

Notes:
- <Any observations>
EOF
```

### Backup Strategy

```bash
# Create timestamped backup
tar -czf "metrics_backup_$(date +%Y%m%d_%H%M%S).tar.gz" metrics-data/

# Sync to external drive
rsync -av --progress metrics-data/ /mnt/external/metrics_backup/

# Upload to cloud (example)
rclone copy metrics-data/ remote:research/metrics/
```

---

## Quick Reference Commands

```bash
# Check connected devices
"$ADB" devices

# Pull metrics from device
MSYS_NO_PATHCONV=1 "$ADB" -s <SERIAL> pull /sdcard/Android/data/com.Prototype.MRMotifs/files/metrics ./output/

# View battery status
"$ADB" shell dumpsys battery

# Check device temperature
"$ADB" shell cat /sys/class/thermal/thermal_zone*/temp

# View Unity logs
"$ADB" logcat -s Unity:*

# Clear app data (reset session counter)
"$ADB" shell pm clear com.Prototype.MRMotifs

# Install new APK
"$ADB" install -r path/to/MRMotifs.apk
```

---

## Support

### Log Issues

If collection fails, gather diagnostic info:
```bash
"$ADB" devices -l > diagnostics.txt
"$ADB" shell pm list packages | grep MRMotifs >> diagnostics.txt
"$ADB" shell ls -laR /sdcard/Android/data/com.Prototype.MRMotifs/ >> diagnostics.txt 2>&1
"$ADB" logcat -d >> diagnostics.txt
```

### References

- **Collection Guide:** `research-paper/data/collection guide.md`
- **Implementation:** `Assets/MRMotifs/Shared Assets/Scripts/METRICS_IMPLEMENTATION_GUIDE.md`
- **MetricsLogger:** `Assets/MRMotifs/Shared Assets/Scripts/MetricsLogger.cs`
- **ADB Documentation:** https://developer.android.com/studio/command-line/adb

---

**Version:** 1.1  
**Last Updated:** November 30, 2025  
**Compatible with:** Unity-MRMotifs, Meta Quest 3, Unity 2022.3 LTS
