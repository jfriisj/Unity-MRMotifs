# Metrics Collection System - Quick Start

## What Was Implemented

A comprehensive metrics collection system for VR research that tracks:
- ✅ Frame rate (FPS) and performance
- ✅ Network latency (Photon Fusion)
- ✅ Calibration accuracy (spatial alignment)
- ✅ Device temperature estimates
- ✅ Session metadata
- ✅ Colocation timing (discovery, localization)

## Files Created/Modified

### New Files
1. **`MetricsLogger.cs`** - Main metrics collection singleton
   - Location: `Assets/MRMotifs/Shared Assets/Scripts/MetricsLogger.cs`
   - Automatic logging every 1 second
   - CSV export on application quit

2. **`METRICS_IMPLEMENTATION_GUIDE.md`** - Complete documentation
   - Location: `Assets/MRMotifs/Shared Assets/Scripts/METRICS_IMPLEMENTATION_GUIDE.md`
   - Setup instructions, data formats, troubleshooting

### Modified Files
1. **`ColocationManager.cs`** - Enhanced with calibration tracking
   - Added `GetCurrentCalibrationError()` method
   - Position delta tracking in millimeters
   - Calibration validation and drift monitoring

2. **`SharedSpatialAnchorManager.cs`** - Added timing metrics
   - Discovery duration tracking
   - Localization timing
   - Automatic calibration logging

## Quick Setup (3 Steps)

### Step 1: Add MetricsLogger to Scene

1. Open your scene (e.g., `ColocationDiscovery.unity`)
2. Create empty GameObject: `[MR Motifs] MetricsLogger`
3. Add Component → `MetricsLogger` (from `MRMotifs.SharedAssets`)
4. Configure in Inspector:
   - ✅ Enable Metrics Logging
   - Set Participant Count = 3
   - Set Scenario name (e.g., "colocated_whiteboard")
   - Set Interface Type ("baseline" or "wim")

### Step 2: Build & Deploy

```bash
# Build for Android (Quest 3)
# File → Build Settings → Android → Build

# Install on headset
adb devices
adb install -r YourApp.apk
```

### Step 3: Retrieve Data

```bash
# After session ends, pull metrics
adb pull /sdcard/Android/data/YOUR_PACKAGE_NAME/files/metrics/ ./session_data/
```

## Data Output

### CSV Format
```
session_1_H1_20250129_143000.csv
session_1_H2_20250129_143000.csv
session_1_H3_20250129_143000.csv
```

**Columns:** session_id, participant_count, timestamp_sec, frame_rate_fps, frame_time_ms, network_latency_ms, packet_loss_pct, calibration_error_mm, headset_temp_c

### JSON Metadata
```json
{
    "sessionId": 1,
    "date": "2025-01-29",
    "startTime": "14:30:00",
    "endTime": "15:25:00",
    "durationMinutes": 55,
    "scenario": "colocated_whiteboard",
    "interfaceType": "wim"
}
```

## Usage Examples

### Check Session Stats (Runtime)
```csharp
using MRMotifs.SharedAssets;

string stats = MetricsLogger.Instance.GetSessionStats();
Debug.Log(stats);
// Output: "Session 1 Stats: Duration: 5.2 min, Avg FPS: 89.7, Avg Latency: 30.8 ms..."
```

### Update Configuration
```csharp
MetricsLogger.Instance.UpdateSessionConfig("scenario_complex", "wim");
```

### Manual Save (Optional)
```csharp
MetricsLogger.Instance.SaveMetricsNow();
```

### Check Calibration
```csharp
using MRMotifs.ColocatedExperiences.Colocation;

ColocationManager colocationMgr = FindObjectOfType<ColocationManager>();
float error = colocationMgr.GetCurrentCalibrationError();
Debug.Log($"Calibration error: {error:F2}mm");
```

## Benchmarks (From Collection Guide)

| Metric | Target | How to Achieve |
|--------|--------|----------------|
| Network Latency | ≤75ms | WiFi 6E, dedicated 6GHz channel |
| Frame Rate | ≥90fps | Optimize rendering, monitor thermal |
| Calibration | <10mm | Good lighting, clean cameras |
| Session Duration | 45min optimal | Mid-session recalibration after 45min |

## Integration with Collection Guide

The implementation follows the specifications in:
**`research-paper/data/collection guide.md`**

Supports:
- ✅ 3-user co-located VR sessions
- ✅ Meta Quest 3 headsets
- ✅ Photon Fusion networking
- ✅ Spatial anchor colocation
- ✅ CSV export format matching guide
- ✅ Session metadata tracking

## Analysis

Use Python scripts to analyze collected data:

```bash
cd research-paper/scripts
pip install -r requirements.txt
python analyze_data.py
```

Or merge data from multiple headsets:

```python
import pandas as pd
import glob

all_data = []
for headset in ['H1', 'H2', 'H3']:
    csv_files = glob.glob(f"session_data/{headset}/*.csv")
    for csv_file in csv_files:
        df = pd.read_csv(csv_file)
        df['headset_id'] = headset
        all_data.append(df)

merged_df = pd.concat(all_data, ignore_index=True)
merged_df = merged_df.sort_values('timestamp_sec')
merged_df.to_csv('merged_performance.csv', index=False)
```

## Troubleshooting

### No CSV files generated?
- Check Unity console for `[MetricsLogger]` messages
- Verify `enableMetricsLogging` is checked
- Try `MetricsLogger.Instance.SaveMetricsNow()`

### Calibration error always 0?
- Ensure `ColocationManager` is in scene
- Check spatial anchors are localized
- Test: `colocationManager.GetCalibrationStatus()`

### Network latency shows 0?
- Verify Photon Fusion `NetworkRunner` is active
- Check scene is networked (not single-player)
- Confirm `FUSION2` define is enabled

## Next Steps

1. **Add to your scenes** - Place MetricsLogger GameObject in each colocated scene
2. **Configure settings** - Adjust scenario names, participant counts
3. **Test data collection** - Run short test sessions, verify CSV output
4. **Implement analysis** - Use provided Python scripts or create custom analysis
5. **Follow collection guide** - Use manual observation forms alongside automatic metrics

## Documentation

- **Full Implementation Guide:** `METRICS_IMPLEMENTATION_GUIDE.md`
- **Collection Guide:** `research-paper/data/collection guide.md`
- **Analysis Scripts:** `research-paper/scripts/analyze_data.py`
- **README Updates:** See main project README for metrics system overview

## Support

Questions or issues? Check:
1. Unity console logs (`[MetricsLogger]` prefix)
2. Implementation guide troubleshooting section
3. Collection guide common issues
4. CSV output format validation

**Ready to use!** Add MetricsLogger to your scene and start collecting data.

---
**Version:** 1.0  
**Created:** November 29, 2025  
**Based on:** Collection Guide v1.0 (November 26, 2025)
