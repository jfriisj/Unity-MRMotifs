# Metrics Collection Guide - 3-User Co-Located VR System

## Overview

This guide provides step-by-step instructions for collecting technical performance and collaboration metrics using three Meta Quest 3 headsets in the Unity-MRMotifs co-located VR training system.

---

## Table of Contents

1. [Pre-Session Setup](#pre-session-setup)
2. [Hardware Preparation](#hardware-preparation)
3. [Network Configuration](#network-configuration)
4. [Unity Project Configuration](#unity-project-configuration)
5. [Calibration Procedure](#calibration-procedure)
6. [Data Collection During Sessions](#data-collection-during-sessions)
7. [Post-Session Data Extraction](#post-session-data-extraction)
8. [Data Analysis](#data-analysis)
9. [Troubleshooting](#troubleshooting)

---

## Pre-Session Setup

### Required Equipment

**Hardware:**
- 3× Meta Quest 3 headsets (fully charged)
- WiFi 6E router/access point (dedicated 6GHz channel)
- Development PC with Unity 2022.3 LTS
- USB-C cables for headset connection (3×)
- Measuring tape or laser distance meter
- Spatial markers (for calibration points)

**Software:**
- Unity 2022.3 LTS with Android Build Support
- Meta XR SDK v78.0.0+
- Photon Fusion 2.0.8
- ADB (Android Debug Bridge)
- Python 3.8+ with pandas, numpy, matplotlib (for analysis)

**Physical Space:**
- 6m × 6m clear area (minimum)
- Padded flooring or safety mats
- Good lighting for hand tracking
- Temperature monitoring capability

### Headset Labeling

Label each headset clearly:
```
Headset 1 (H1) - Player/Host
Headset 2 (H2) - Player/Client
Headset 3 (H3) - Player/Client
```

Record serial numbers:
```
H1: [Serial Number]
H2: [Serial Number]
H3: [Serial Number]
```

---

## Hardware Preparation

### 1. Charge All Headsets

Ensure all headsets are at **100% battery** before each session:
```bash
# Check battery levels via ADB
adb devices
adb -s <DEVICE_SERIAL> shell dumpsys battery | grep level
```

**Target:** All headsets >95% charge for 60-minute sessions

### 2. Enable Developer Mode

On each headset:
1. Go to **Settings** → **System** → **Developer**
2. Enable **Developer Mode**
3. Enable **USB Debugging**
4. Enable **Performance Overlay** (for FPS monitoring)

### 3. Configure Guardian Boundaries

Set up identical Guardian boundaries on all headsets:
1. Define 6m × 6m play area
2. Mark floor-level boundaries
3. Save as "Training Area" profile
4. Verify all three boundaries align physically

### 4. Temperature Baseline

Record ambient room temperature:
```
Room Temperature: _____°C
Humidity: _____%
```

---

## Network Configuration

### WiFi 6E Setup

**Router Configuration:**
1. Use dedicated 6GHz channel (149-165 recommended)
2. Channel Width: 160 MHz
3. Enable QoS with highest priority for VR traffic
4. Disable band steering and automatic channel selection
5. Set static DHCP reservations for headsets

**Network Settings per Headset:**
```
H1: IP 192.168.1.101, Priority: Highest
H2: IP 192.168.1.102, Priority: Highest  
H3: IP 192.168.1.103, Priority: Highest
```

**Verify Network Performance:**
```bash
# Ping test between headsets
adb -s <H1_SERIAL> shell ping -c 10 192.168.1.102
adb -s <H1_SERIAL> shell ping -c 10 192.168.1.103

# Target: <5ms latency, 0% packet loss
```

---

## Unity Project Configuration

### 1. Enable Logging Systems

In Unity, ensure these components are active in the scene:

**MetricsLogger.cs** (Create if not exists):
```csharp
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class MetricsLogger : MonoBehaviour
{
    [System.Serializable]
    public class TechnicalMetric
    {
        public int sessionId;
        public int participantCount;
        public float timestamp;
        public float frameRate;
        public float frameTime;
        public float networkLatency;
        public float packetLoss;
        public float calibrationError;
        public float headsetTemp;
    }

    private List<TechnicalMetric> metrics = new List<TechnicalMetric>();
    private float logInterval = 1.0f; // Log every 1 second
    private float lastLogTime;
    private int currentSessionId;
    
    void Start()
    {
        currentSessionId = PlayerPrefs.GetInt("SessionID", 1);
        lastLogTime = Time.time;
        
        // Create persistent data path
        string path = Path.Combine(Application.persistentDataPath, "metrics");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    void Update()
    {
        if (Time.time - lastLogTime >= logInterval)
        {
            LogMetric();
            lastLogTime = Time.time;
        }
    }

    void LogMetric()
    {
        TechnicalMetric metric = new TechnicalMetric
        {
            sessionId = currentSessionId,
            participantCount = 3,
            timestamp = Time.time,
            frameRate = 1.0f / Time.deltaTime,
            frameTime = Time.deltaTime * 1000f,
            networkLatency = GetNetworkLatency(),
            packetLoss = GetPacketLoss(),
            calibrationError = GetCalibrationError(),
            headsetTemp = GetHeadsetTemperature()
        };
        
        metrics.Add(metric);
    }

    float GetNetworkLatency()
    {
        // Get from Photon Fusion Runner
        var runner = FindObjectOfType<Fusion.NetworkRunner>();
        if (runner != null)
            return runner.SimulationConfig.TickRate > 0 ? 
                   (1000f / runner.SimulationConfig.TickRate) : 0f;
        return 0f;
    }

    float GetPacketLoss()
    {
        // Implement packet loss calculation from network stats
        return 0f; // Placeholder
    }

    float GetCalibrationError()
    {
        // Get from Colocation Manager
        var colocationManager = FindObjectOfType<ColocationManager>();
        if (colocationManager != null)
            return colocationManager.GetCurrentCalibrationError();
        return 0f;
    }

    float GetHeadsetTemperature()
    {
        // Temperature estimation (requires native plugin or system access)
        // For now, estimate based on performance degradation
        float perfDegradation = 1.0f - (1.0f / Time.deltaTime) / 90f;
        return 32f + (perfDegradation * 10f); // Estimate 32-42°C range
    }

    void OnApplicationQuit()
    {
        SaveMetricsToCSV();
    }

    void SaveMetricsToCSV()
    {
        string filename = $"session_{currentSessionId}_H{GetHeadsetID()}_{System.DateTime.Now:yyyyMMdd_HHmmss}.csv";
        string filepath = Path.Combine(Application.persistentDataPath, "metrics", filename);
        
        using (StreamWriter writer = new StreamWriter(filepath))
        {
            // Write header
            writer.WriteLine("session_id,participant_count,timestamp_sec,frame_rate_fps,frame_time_ms,network_latency_ms,packet_loss_pct,calibration_error_mm,headset_temp_c");
            
            // Write metrics
            foreach (var metric in metrics)
            {
                writer.WriteLine($"{metric.sessionId},{metric.participantCount},{metric.timestamp:F2}," +
                               $"{metric.frameRate:F1},{metric.frameTime:F2},{metric.networkLatency:F1}," +
                               $"{metric.packetLoss:F3},{metric.calibrationError:F2},{metric.headsetTemp:F1}");
            }
        }
        
        Debug.Log($"Metrics saved to: {filepath}");
    }

    int GetHeadsetID()
    {
        // Implement headset identification logic
        string deviceName = SystemInfo.deviceName;
        if (deviceName.Contains("1")) return 1;
        if (deviceName.Contains("2")) return 2;
        return 3;
    }
}
```

### 2. Build and Deploy

Build APK for all headsets:
```bash
# In Unity, configure Build Settings:
# - Platform: Android
# - Texture Compression: ASTC
# - Development Build: ENABLED
# - Script Debugging: ENABLED
# - Wait for Managed Debugger: DISABLED

# Build APK
# Unity Menu: File → Build Settings → Build
```

Install on each headset:
```bash
adb devices
adb -s <H1_SERIAL> install -r YourApp.apk
adb -s <H2_SERIAL> install -r YourApp.apk
adb -s <H3_SERIAL> install -r YourApp.apk
```

---

## Calibration Procedure

### Initial Calibration (Pre-Session)

**Setup Calibration Points:**
1. Place three physical markers in the play area:
   - Point 1: Center of play area (0, 0, 0)
   - Point 2: 2m North of center (0, 0, 2)
   - Point 3: 2m East of center (2, 0, 0)

2. Measure exact positions with laser distance meter
3. Record ground truth coordinates

**Calibration Steps (Perform on Each Headset):**

1. **Launch Application** on all three headsets
2. **Select "Calibration Mode"** from main menu
3. **For each headset sequentially:**
   ```
   Instruction: "User 1, place both hands at Point 1"
   → Record 3D position from hand tracking
   → Repeat for Points 2 and 3
   → Calculate transformation matrix
   ```

4. **Validation:**
   - System displays calibration error for each point
   - Target: <10mm per point
   - If error >10mm, recalibrate

**Record Initial Calibration Data:**
```
Session ID: _____
Date/Time: _____
Scenario: [ ] Basic [ ] Medium [ ] Complex

Headset 1:
  Point 1: X=____ Y=____ Z=____ Error=____mm
  Point 2: X=____ Y=____ Z=____ Error=____mm
  Point 3: X=____ Y=____ Z=____ Error=____mm
  Mean Error: ____mm

Headset 2:
  Point 1: X=____ Y=____ Z=____ Error=____mm
  Point 2: X=____ Y=____ Z=____ Error=____mm
  Point 3: X=____ Y=____ Z=____ Error=____mm
  Mean Error: ____mm

Headset 3:
  Point 1: X=____ Y=____ Z=____ Error=____mm
  Point 2: X=____ Y=____ Z=____ Error=____mm
  Point 3: X=____ Y=____ Z=____ Error=____mm
  Mean Error: ____mm
```

### Mid-Session Recalibration (Every 45 Minutes)

If session exceeds 45 minutes:
1. Pause training scenario
2. Trigger recalibration routine
3. Users repeat hand placement procedure
4. Record updated calibration errors
5. Resume training

---

## Data Collection During Sessions

### Automatic Logging

**Technical Metrics** (logged every 1 second):
- Frame rate (FPS)
- Frame time (ms)
- Network latency (RTT ms)
- Packet loss (%)
- Calibration error (mm)
- Estimated device temperature (°C)

**Logged Automatically by MetricsLogger component**

### Manual Observations

**Observer Checklist** (fill during session):

```
Session ID: _____
Observer Name: _____
Start Time: _____
End Time: _____

Every 5 minutes, record:
[ ] All headsets tracking properly
[ ] No visible lag/jitter
[ ] Users moving freely
[ ] No collision warnings
[ ] Network stable

Incidents:
Time ____ : Tracking lost on H___
Time ____ : Performance drop observed  
Time ____ : User collision (distance: ___cm)
Time ____ : Network lag spike
Time ____ : Recalibration triggered
```

### Collaboration Metrics

**Task Performance Data** (record per task):
```
Task ID: _____
Interface Type: [ ] Baseline [ ] WIM
Scenario Complexity: [ ] Basic [ ] Medium [ ] Complex

Start Time: _____
End Time: _____
Completion Time: _____ seconds

Coordination Errors: _____
  - Collision events: _____
  - Communication failures: _____
  - Spatial awareness issues: _____

Task Success: [ ] Complete [ ] Failed [ ] Partial

Communication Events: _____
  - Verbal instructions: _____
  - Gestures: _____
  - WIM interface usage: _____

Spatial Awareness Score (1-10): _____
  Observer assessment of team spatial coordination
```

### Performance Overlay (Live Monitoring)

Enable on-headset overlay (Developer Mode):
1. Three-finger tap → Performance HUD
2. Monitor in real-time:
   - FPS (target: >85)
   - CPU/GPU usage
   - Memory usage
   - Thermal throttling indicator

---

## Post-Session Data Extraction

### Extract Logs from Headsets

```bash
# Create session data directory
mkdir -p data/session_$(date +%Y%m%d_%H%M%S)
cd data/session_$(date +%Y%m%d_%H%M%S)

# Pull metrics from each headset
adb -s <H1_SERIAL> pull /sdcard/Android/data/com.yourcompany.vrsystem/files/metrics/ ./H1/
adb -s <H2_SERIAL> pull /sdcard/Android/data/com.yourcompany.vrsystem/files/metrics/ ./H2/
adb -s <H3_SERIAL> pull /sdcard/Android/data/com.yourcompany.vrsystem/files/metrics/ ./H3/

# Pull Unity logs
adb -s <H1_SERIAL> logcat -d > H1_logcat.txt
adb -s <H2_SERIAL> logcat -d > H2_logcat.txt
adb -s <H3_SERIAL> logcat -d > H3_logcat.txt

# Pull Photon stats (if available)
adb -s <H1_SERIAL> pull /sdcard/Android/data/com.yourcompany.vrsystem/files/photon_stats.log ./
```

### Organize Data Files

Expected file structure:
```
data/
└── session_20250126_143000/
    ├── H1/
    │   ├── session_1_H1_20250126_143000.csv
    │   └── calibration_1_H1.json
    ├── H2/
    │   ├── session_1_H2_20250126_143000.csv
    │   └── calibration_1_H2.json
    ├── H3/
    │   ├── session_1_H3_20250126_143000.csv
    │   └── calibration_1_H3.json
    ├── collaboration_performance.csv (manual entry)
    ├── observer_notes.txt
    └── session_metadata.json
```

### Create Session Metadata

`session_metadata.json`:
```json
{
  "session_id": 1,
  "date": "2025-01-26",
  "start_time": "14:30:00",
  "end_time": "15:25:00",
  "duration_minutes": 55,
  "scenario": "scenario_medium",
  "interface_type": "wim",
  "participants": [
    {"id": 1, "headset": "H1", "serial": "1WMHH8XXXX"},
    {"id": 2, "headset": "H2", "serial": "1WMHH8YYYY"},
    {"id": 3, "headset": "H3", "serial": "1WMHH8ZZZZ"}
  ],
  "environment": {
    "room_temperature_start": 22.5,
    "room_temperature_end": 23.2,
    "humidity": 45,
    "lighting": "good"
  },
  "network": {
    "router": "WiFi 6E - 6GHz",
    "channel": 149,
    "bandwidth": "160MHz"
  },
  "incidents": [
    {"time": "14:45:00", "type": "tracking_loss", "headset": "H2", "duration_sec": 3},
    {"time": "15:10:00", "type": "recalibration", "reason": "45min_threshold"}
  ]
}
```

---

## Data Analysis

### Merge Data from All Headsets

Use Python script to combine CSV files:

```python
import pandas as pd
import glob
import os

def merge_session_data(session_dir):
    """Merge metrics from all three headsets"""
    
    all_data = []
    
    for headset in ['H1', 'H2', 'H3']:
        csv_files = glob.glob(f"{session_dir}/{headset}/*.csv")
        
        for csv_file in csv_files:
            df = pd.read_csv(csv_file)
            df['headset_id'] = headset
            all_data.append(df)
    
    merged_df = pd.concat(all_data, ignore_index=True)
    merged_df = merged_df.sort_values('timestamp_sec')
    
    output_file = f"{session_dir}/merged_technical_performance.csv"
    merged_df.to_csv(output_file, index=False)
    
    print(f"Merged data saved to: {output_file}")
    return merged_df

# Usage
session_dir = "data/session_20250126_143000"
df = merge_session_data(session_dir)
```

### Calculate Summary Statistics

```python
def calculate_session_statistics(df):
    """Generate summary statistics for session"""
    
    stats = {
        'duration_min': df['timestamp_sec'].max() / 60,
        'mean_fps': df['frame_rate_fps'].mean(),
        'min_fps': df['frame_rate_fps'].min(),
        'fps_std': df['frame_rate_fps'].std(),
        'mean_latency': df['network_latency_ms'].mean(),
        'max_latency': df['network_latency_ms'].max(),
        'mean_calibration_error': df['calibration_error_mm'].mean(),
        'max_calibration_error': df['calibration_error_mm'].max(),
        'mean_packet_loss': df['packet_loss_pct'].mean(),
        'temp_start': df['headset_temp_c'].iloc[0],
        'temp_end': df['headset_temp_c'].iloc[-1],
        'temp_delta': df['headset_temp_c'].iloc[-1] - df['headset_temp_c'].iloc[0]
    }
    
    # Check benchmark achievement
    stats['latency_achieved'] = (df['network_latency_ms'] <= 75).mean() * 100
    stats['fps_achieved'] = (df['frame_rate_fps'] >= 90).mean() * 100
    stats['calibration_achieved'] = (df['calibration_error_mm'] < 10).mean() * 100
    
    return stats

stats = calculate_session_statistics(df)
print("\nSession Statistics:")
for key, value in stats.items():
    print(f"{key}: {value:.2f}")
```

### Generate Visualizations

```python
import matplotlib.pyplot as plt

def plot_session_metrics(df, session_id):
    """Create summary plots for session"""
    
    fig, axes = plt.subplots(2, 2, figsize=(14, 10))
    
    # Frame rate over time
    for headset in ['H1', 'H2', 'H3']:
        headset_data = df[df['headset_id'] == headset]
        axes[0, 0].plot(headset_data['timestamp_sec']/60, 
                       headset_data['frame_rate_fps'], 
                       label=headset, alpha=0.7)
    axes[0, 0].axhline(90, color='g', linestyle='--', label='Target')
    axes[0, 0].set_xlabel('Time (minutes)')
    axes[0, 0].set_ylabel('Frame Rate (fps)')
    axes[0, 0].set_title('Frame Rate Stability')
    axes[0, 0].legend()
    axes[0, 0].grid(alpha=0.3)
    
    # Network latency
    for headset in ['H1', 'H2', 'H3']:
        headset_data = df[df['headset_id'] == headset]
        axes[0, 1].plot(headset_data['timestamp_sec']/60, 
                       headset_data['network_latency_ms'], 
                       label=headset, alpha=0.7)
    axes[0, 1].axhline(75, color='g', linestyle='--', label='Target')
    axes[0, 1].set_xlabel('Time (minutes)')
    axes[0, 1].set_ylabel('Network Latency (ms)')
    axes[0, 1].set_title('Network Performance')
    axes[0, 1].legend()
    axes[0, 1].grid(alpha=0.3)
    
    # Calibration error
    for headset in ['H1', 'H2', 'H3']:
        headset_data = df[df['headset_id'] == headset]
        axes[1, 0].plot(headset_data['timestamp_sec']/60, 
                       headset_data['calibration_error_mm'], 
                       label=headset, alpha=0.7)
    axes[1, 0].axhline(10, color='r', linestyle='--', label='Safety Threshold')
    axes[1, 0].set_xlabel('Time (minutes)')
    axes[1, 0].set_ylabel('Calibration Error (mm)')
    axes[1, 0].set_title('Calibration Drift')
    axes[1, 0].legend()
    axes[1, 0].grid(alpha=0.3)
    
    # Temperature
    for headset in ['H1', 'H2', 'H3']:
        headset_data = df[df['headset_id'] == headset]
        axes[1, 1].plot(headset_data['timestamp_sec']/60, 
                       headset_data['headset_temp_c'], 
                       label=headset, alpha=0.7)
    axes[1, 1].set_xlabel('Time (minutes)')
    axes[1, 1].set_ylabel('Temperature (°C)')
    axes[1, 1].set_title('Thermal Performance')
    axes[1, 1].legend()
    axes[1, 1].grid(alpha=0.3)
    
    plt.tight_layout()
    plt.savefig(f'session_{session_id}_analysis.png', dpi=300)
    print(f"Saved plot: session_{session_id}_analysis.png")
    plt.close()
```

---

## Troubleshooting

### Common Issues

**Issue: Headset Not Connecting to Network**
```
Solution:
1. Verify WiFi 6E is enabled on headset
2. Check router 6GHz band is active
3. Forget network and reconnect
4. Verify correct password and security (WPA3)
5. Check DHCP reservation is active
```

**Issue: High Network Latency (>75ms)**
```
Solution:
1. Check for interference (other WiFi networks)
2. Move router closer to play area
3. Reduce channel width to 80MHz if interference present
4. Verify QoS settings prioritize VR traffic
5. Check for background downloads on headsets
```

**Issue: Calibration Error >10mm**
```
Solution:
1. Improve lighting conditions for hand tracking
2. Clean headset cameras
3. Recalibrate Guardian boundaries
4. Ensure markers are precisely positioned
5. Use controller-based calibration if hand tracking fails
```

**Issue: FPS Drops Below 85**
```
Solution:
1. Check device temperature (if >40°C, pause and cool down)
2. Close background apps on headsets
3. Reduce scene complexity/polygon count
4. Lower texture quality settings
5. Disable performance-heavy features (shadows, reflections)
```

**Issue: Tracking Lost During Session**
```
Solution:
1. Check lighting (too bright/too dark affects tracking)
2. Ensure play area has visual features (avoid blank walls)
3. Clean headset cameras
4. Redefine Guardian boundary
5. Update firmware if persistent
```

**Issue: MetricsLogger Not Saving Data**
```
Solution:
1. Verify storage permissions granted
2. Check available storage space (need >500MB)
3. Review Unity logcat for errors
4. Ensure Application.persistentDataPath is accessible
5. Test write permissions with simple file creation
```

### Data Quality Checks

**Before analyzing data:**
```python
def validate_session_data(df):
    """Check data quality"""
    
    issues = []
    
    # Check for missing timestamps
    if df['timestamp_sec'].isnull().any():
        issues.append("Missing timestamps detected")
    
    # Check for unrealistic FPS values
    if (df['frame_rate_fps'] < 30).any() or (df['frame_rate_fps'] > 120).any():
        issues.append("Unrealistic FPS values detected")
    
    # Check for negative latencies
    if (df['network_latency_ms'] < 0).any():
        issues.append("Negative latency values detected")
    
    # Check for data gaps (>5 second intervals)
    time_diffs = df['timestamp_sec'].diff()
    if (time_diffs > 5).any():
        issues.append(f"Data gaps detected: {(time_diffs > 5).sum()} gaps")
    
    # Check calibration errors
    if (df['calibration_error_mm'] > 50).any():
        issues.append("Extreme calibration errors (>50mm)")
    
    if issues:
        print("⚠️  Data Quality Issues:")
        for issue in issues:
            print(f"  - {issue}")
        return False
    else:
        print("✓ Data quality check passed")
        return True
```

---

## Session Checklist

### Pre-Session (30 minutes before)

- [ ] Charge all headsets to 100%
- [ ] Configure network (WiFi 6E, QoS)
- [ ] Set up physical space (markers, boundaries)
- [ ] Install/update APK on all headsets
- [ ] Verify ADB connectivity
- [ ] Test network latency (<5ms ping)
- [ ] Prepare data collection forms
- [ ] Brief participants on procedure

### Start of Session (10 minutes)

- [ ] Record session metadata
- [ ] Perform initial calibration (all headsets)
- [ ] Verify calibration errors <10mm
- [ ] Enable performance overlays
- [ ] Start observer notes
- [ ] Confirm all logging systems active
- [ ] Take baseline temperature readings

### During Session

- [ ] Monitor performance overlays
- [ ] Record observer notes every 5 min
- [ ] Watch for tracking issues
- [ ] Check for user discomfort
- [ ] Trigger recalibration at 45 min (if needed)
- [ ] Log any incidents immediately

### End of Session (15 minutes)

- [ ] Record final session time
- [ ] Extract logs from all headsets
- [ ] Save collaboration performance data
- [ ] Complete observer notes
- [ ] Backup data to multiple locations
- [ ] Verify all CSV files generated
- [ ] Clean and charge headsets
- [ ] Debrief participants (if appropriate)

### Post-Session Analysis

- [ ] Merge data from all headsets
- [ ] Run data quality validation
- [ ] Calculate summary statistics
- [ ] Generate visualization plots
- [ ] Compare to evidence-based benchmarks
- [ ] Document any anomalies
- [ ] Update session log database
- [ ] Archive raw data

---

## Contact & Support

For issues with:
- **Unity Project:** Check Unity console and Player.log
- **Headset Connectivity:** Meta Quest support
- **Network Issues:** Check router logs and WiFi analyzer
- **Data Analysis:** Review Python error messages and data formats

**Project Repository:** https://github.com/jfriisj/Unity-MRMotifs
**Documentation:** See `/research-paper/` for detailed methodology

---

## Appendix: File Formats

### Technical Performance CSV Format

```csv
session_id,participant_count,timestamp_sec,frame_rate_fps,frame_time_ms,network_latency_ms,packet_loss_pct,calibration_error_mm,headset_temp_c
1,3,0.0,91.2,10.96,28.4,0.1,7.2,32.1
1,3,1.0,91.1,10.98,28.6,0.1,7.2,32.2
...
```

### Collaboration Performance CSV Format

```csv
session_id,scenario,interface_type,task_completion_time_sec,coordination_errors,communication_events,task_success,spatial_awareness_score
1,scenario_basic,baseline,285,3,42,1,6.2
2,scenario_medium,wim,356,3,61,1,8.1
...
```

### Calibration Accuracy CSV Format

```csv
session_id,headset_id,calibration_point,x_error_mm,y_error_mm,z_error_mm,total_error_mm,timestamp_min
1,headset_1,point_1,2.3,1.8,3.1,4.2,0
1,headset_1,point_2,1.9,2.4,2.8,4.1,0
...
```

---

**Version:** 1.0  
**Last Updated:** November 26, 2025  
**Authors:** Unity-MRMotifs Research Team
