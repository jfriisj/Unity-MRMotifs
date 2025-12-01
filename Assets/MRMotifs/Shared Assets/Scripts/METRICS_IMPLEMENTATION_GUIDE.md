# Metrics Collection System - Implementation Guide

## Overview

The metrics collection system provides comprehensive data logging for VR research and performance analysis. It tracks technical performance (FPS, network latency, calibration accuracy), thermal metrics, and session metadata for multi-user co-located VR experiences.

## Components

### 1. MetricsLogger.cs
**Location:** `Assets/MRMotifs/Shared Assets/Scripts/MetricsLogger.cs`

A persistent singleton component that automatically collects and logs performance metrics throughout VR sessions.

**Features:**
- Automatic 1-second interval logging
- CSV export on application quit
- Session metadata tracking (JSON format)
- Network latency measurement (Photon Fusion integration)
- Calibration accuracy tracking
- Temperature estimation based on performance degradation
- Configurable via Unity Inspector

**Key Methods:**
- `GetSessionStats()` - Returns current session statistics as string
- `SaveMetricsNow()` - Manually trigger metrics save (useful for testing)
- `UpdateSessionConfig(scenario, interfaceType)` - Update session configuration during runtime
- `GetMetricsCount()` - Get total number of collected metrics

### 2. ColocationManager.cs (Enhanced)
**Location:** `Assets/MRMotifs/ColocatedExperiences/Scripts/Colocation/ColocationManager.cs`

Extended with calibration accuracy tracking and validation.

**New Features:**
- Automatic calibration error calculation on alignment
- Position delta tracking (millimeters precision)
- Periodic calibration validation
- Calibration drift monitoring

**Key Methods:**
- `GetCurrentCalibrationError()` - Returns current calibration error in mm
- `ValidateCalibration()` - Measures current drift from anchor position
- `ResetCalibration()` - Resets calibration tracking state
- `GetCalibrationStatus()` - Returns detailed calibration status string

### 3. SharedSpatialAnchorManager.cs (Enhanced)
**Location:** `Assets/MRMotifs/ColocatedExperiences/Scripts/Colocation/SharedSpatialAnchorManager.cs`

Extended with timing metrics for colocation operations.

**New Features:**
- Discovery duration tracking
- Anchor localization timing
- Total load time measurement
- Automatic calibration error logging after alignment

## Setup Instructions

### Step 1: Add MetricsLogger to Your Scene

1. Open your main scene (e.g., `ColocationDiscovery.unity`)
2. Create an empty GameObject named `[MR Motifs] MetricsLogger`
3. Add the `MetricsLogger` component from `MRMotifs.SharedAssets`
4. Configure settings in the Inspector:
   - **Enable Metrics Logging:** Check to enable (default: true)
   - **Log Interval:** 1.0 second (recommended)
   - **Participant Count:** 3 (for 3-user studies)
   - **Scenario:** Set to your scenario name (e.g., "colocated_whiteboard")
   - **Interface Type:** "baseline" or "wim"
   - **Room Temperature:** Ambient temperature in °C
   - **Room Humidity:** Percentage
   - **Lighting Condition:** "good", "poor", etc.

### Step 2: Verify ColocationManager Integration

The `ColocationManager` component is automatically enhanced. No additional setup required if you already have it in your scene.

### Step 3: Configure Photon Fusion (if using networking)

Ensure your scene has:
- `NetworkRunner` GameObject with Photon Fusion components
- Proper network configuration for latency measurements

### Step 4: Test Data Collection

1. Build and deploy to Quest 3 headset
2. Run a test session
3. Check Unity console for log messages: `[MetricsLogger] Session X started...`
4. Exit the application to trigger CSV export
5. Use ADB to retrieve data:

```bash
# Connect headset via USB
adb devices

# Pull metrics data
adb pull /sdcard/Android/data/[YOUR_PACKAGE_NAME]/files/metrics/ ./test_data/
```

## Data Output Formats

### Technical Performance CSV

**Filename:** `session_{sessionId}_H{headsetId}_{timestamp}.csv`

**Location:** `Application.persistentDataPath/metrics/`

**Format:**
```csv
session_id,participant_count,timestamp_sec,frame_rate_fps,frame_time_ms,network_latency_ms,packet_loss_pct,calibration_error_mm,headset_temp_c
1,3,0.00,90.5,11.05,28.4,0.0,4.2,32.1
1,3,1.00,90.3,11.07,29.1,0.0,4.3,32.2
```

**Column Descriptions:**
- `session_id`: Session identifier (increments automatically)
- `participant_count`: Number of participants in session
- `timestamp_sec`: Time since session start (seconds)
- `frame_rate_fps`: Instantaneous frame rate
- `frame_time_ms`: Frame time in milliseconds
- `network_latency_ms`: Round-trip time estimate (0 if no network)
- `packet_loss_pct`: Packet loss percentage (0-100)
- `calibration_error_mm`: Spatial alignment error in millimeters
- `headset_temp_c`: Estimated device temperature (°C)

### Session Metadata JSON

**Filename:** `session_{sessionId}_H{headsetId}_metadata_{timestamp}.json`

**Format:**
```json
{
    "sessionId": 1,
    "date": "2025-01-29",
    "startTime": "14:30:00",
    "endTime": "15:25:00",
    "durationMinutes": 55,
    "scenario": "colocated_whiteboard",
    "interfaceType": "wim",
    "roomTempStart": 22.5,
    "roomTempEnd": 22.5,
    "humidity": 45,
    "lighting": "good"
}
```

## Metrics Collected

### Automatic Metrics (Logged Every Second)

| Metric | Description | Target | Collection Method |
|--------|-------------|--------|-------------------|
| Frame Rate | FPS performance | ≥90 fps | `1.0f / Time.deltaTime` |
| Frame Time | Time per frame | ≤11.1 ms | `Time.deltaTime * 1000f` |
| Network Latency | RTT estimate | ≤75 ms | Fusion `TickRate` calculation |
| Packet Loss | Network reliability | <1% | Fusion network stats (TODO) |
| Calibration Error | Spatial alignment | <10 mm | Position delta from anchor |
| Device Temperature | Thermal estimate | 32-42°C | Performance degradation formula |

### Manual/Observer Metrics (Per Collection Guide)

These should be recorded separately using the collection guide forms:
- Task completion time
- Coordination errors
- Communication events
- Spatial awareness scores
- Incidents and troubleshooting events

## Integration with Existing Scenes

### Colocated Experiences Scenes

The metrics system integrates seamlessly with:
- **ColocationDiscovery.unity** - Full colocation metrics with discovery timing
- **SpatialAnchors.unity** - Basic anchor creation metrics
- **SpaceSharing.unity** - MRUK room sharing metrics

### Shared Activities Scenes

Works with networked scenes:
- **Chess.unity** - Network latency tracking during gameplay
- **MovieCowatching.unity** - Performance monitoring during video sync

## Advanced Usage

### Runtime Configuration

Update session parameters during runtime:

```csharp
using MRMotifs.SharedAssets;

// Change scenario dynamically
MetricsLogger.Instance.UpdateSessionConfig("scenario_complex", "wim");

// Get current statistics
string stats = MetricsLogger.Instance.GetSessionStats();
Debug.Log(stats);

// Trigger manual save (e.g., at checkpoints)
MetricsLogger.Instance.SaveMetricsNow();
```

### Calibration Validation

Periodically check calibration drift:

```csharp
using MRMotifs.ColocatedExperiences.Colocation;

ColocationManager colocationMgr = FindObjectOfType<ColocationManager>();

// Validate calibration (every 45 minutes recommended)
float driftError = colocationMgr.ValidateCalibration();
if (driftError > 10f)
{
    Debug.LogWarning($"Calibration drift detected: {driftError:F2}mm - Recalibration recommended");
}

// Get detailed status
string status = colocationMgr.GetCalibrationStatus();
Debug.Log(status);
```

### Custom Metrics Extension

Extend MetricsLogger for custom metrics:

```csharp
// Add to TechnicalMetric class
public float customMetric;

// Add to LogMetric() method
metric.customMetric = GetCustomMetric();

// Implement getter
private float GetCustomMetric()
{
    // Your custom metric logic
    return 0f;
}

// Update CSV header and write format
```

## Data Analysis

### Python Analysis Scripts

Use the provided Python scripts in `research-paper/scripts/`:

```bash
cd research-paper/scripts
pip install -r requirements.txt
python analyze_data.py
```

### Merging Multi-Headset Data

```python
import pandas as pd
import glob

def merge_session_data(session_dir):
    all_data = []
    for headset in ['H1', 'H2', 'H3']:
        csv_files = glob.glob(f"{session_dir}/{headset}/*.csv")
        for csv_file in csv_files:
            df = pd.read_csv(csv_file)
            df['headset_id'] = headset
            all_data.append(df)
    
    merged_df = pd.concat(all_data, ignore_index=True)
    merged_df = merged_df.sort_values('timestamp_sec')
    return merged_df
```

## Troubleshooting

### Metrics Not Being Saved

**Issue:** No CSV files generated after session

**Solutions:**
1. Check Unity console for error messages
2. Verify `enableMetricsLogging` is checked in Inspector
3. Confirm `Application.persistentDataPath` is writable
4. Use `MetricsLogger.Instance.GetMetricsCount()` to verify collection
5. Try manual save: `MetricsLogger.Instance.SaveMetricsNow()`

### Calibration Error Always Zero

**Issue:** `calibration_error_mm` column shows 0.0

**Solutions:**
1. Verify `ColocationManager` is in the scene
2. Ensure `AlignUserToAnchor()` is being called
3. Check that spatial anchors are properly localized
4. Test calibration: `colocationManager.GetCalibrationStatus()`

### Network Latency Not Measured

**Issue:** `network_latency_ms` column shows 0.0

**Solutions:**
1. Verify Photon Fusion is active in scene
2. Check `NetworkRunner` is running: `runner.IsRunning`
3. Confirm `FUSION2` scripting define is enabled
4. Test in networked scene (not single-player)

### High Memory Usage

**Issue:** Application runs out of memory during long sessions

**Solutions:**
1. Reduce log interval (increase from 1.0s to 2.0s or 5.0s)
2. Implement periodic saves and clear metrics list
3. Monitor metrics count: `GetMetricsCount()`
4. Manually trigger saves at checkpoints: `SaveMetricsNow()`

## Performance Considerations

### Impact on Frame Rate

- **Logging overhead:** < 0.5ms per sample
- **CSV write:** Only on application quit (no runtime impact)
- **Network checks:** Cached NetworkRunner reference (1s refresh)

### Memory Usage

- **Per metric:** ~40 bytes
- **1-hour session:** ~144 KB (3,600 samples)
- **Recommended:** Enable manual saves for sessions >1 hour

### Storage Requirements

- **Per headset, per session:** ~100-500 KB (CSV + JSON)
- **3 headsets, 10 sessions:** ~1-5 MB total
- **Quest 3 available:** Typically >10 GB free

## Best Practices

1. **Session Management:**
   - Start new session ID for each study session
   - Record session metadata in lab notebook
   - Backup data immediately after collection

2. **Data Quality:**
   - Verify log files are generated before clearing headsets
   - Check for data gaps (>5 second intervals)
   - Validate FPS values are realistic (30-120 fps)

3. **Multi-Headset Studies:**
   - Label headsets clearly (H1, H2, H3)
   - Use consistent naming in device settings
   - Synchronize session start times

4. **Research Compliance:**
   - Follow collection guide procedures
   - Document any deviations from protocol
   - Store data securely according to IRB requirements

## References

- **Collection Guide:** `research-paper/data/collection guide.md`
- **Analysis Scripts:** `research-paper/scripts/analyze_data.py`
- **Meta XR SDK:** [Spatial Anchors Documentation](https://developers.meta.com/horizon/documentation/unity/unity-spatial-anchors-persist-content)
- **Photon Fusion:** [Network Statistics API](https://doc.photonengine.com/fusion/current/manual/connection-and-matchmaking/network-statistics)

## Support

For issues or questions:
- Check Unity console logs for error messages
- Review collection guide troubleshooting section
- Examine CSV output format and data quality
- Test with simplified scenes first

**Version:** 1.0  
**Last Updated:** November 29, 2025  
**Authors:** Unity-MRMotifs Research Team
