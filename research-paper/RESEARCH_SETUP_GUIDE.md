# Research Setup Guide: Co-Located Multi-User VR System

This guide helps you set up the Unity-MRMotifs project to complete the research outlined in your paper on 3-user co-located VR training systems.

## Research Questions Mapping

| Research Question | Implementation Component | Status |
|-------------------|--------------------------|--------|
| **RQ1**: Automated spatial colocation | `ColocationManager`, `SharedSpatialAnchorManager` | ✅ Ready |
| **RQ2**: Performance monitoring | `MetricsLogger` | ✅ Ready |
| **RQ3**: Networked object sync | Photon Fusion 2 integration | ✅ Ready |
| **RQ4**: Architecture patterns | Full project structure | ✅ Ready |
| **RQ5**: Hardware feasibility | MetricsLogger + data collection | ✅ Ready |

---

## Step 1: Hardware Requirements

### Required Equipment
- **3× Meta Quest 3 headsets** (~$500 each)
- **WiFi 6 or 6E router** (6GHz dedicated recommended for <30ms latency)
- **Development PC** with Unity 6000.0.62f1
- **USB-C cables** (for ADB debugging and APK deployment)

### Recommended Network Setup
```
Router: WiFi 6E with 6GHz dedicated band
QoS: Enable gaming/VR priority
Channel: Use least congested (survey with WiFi analyzer)
DHCP: Assign static IPs to each Quest (easier debugging)
```

---

## Step 2: Project Configuration

### 2.1 Open Unity Project
```bash
cd C:\github\Unity-MRMotifs
# Open with Unity 6000.0.62f1
```

### 2.2 Verify Scripting Defines
The following should already be configured (Player Settings → Android):
- `FUSION2` ✅
- `PHOTON_VOICE_DEFINED` ✅

### 2.3 Configure Photon Settings
1. Open `Photon/PhotonUnityNetworking/Resources/PhotonServerSettings.asset`
2. Enter your Photon App ID (get from https://dashboard.photonengine.com)
3. For local testing, you can use a free tier account

---

## Step 3: Scene Setup for Research

### Primary Research Scenes
Located in `Assets/MRMotifs/ColocatedExperiences/Scenes/`:

| Scene | Purpose | Research Use |
|-------|---------|--------------|
| `ColocationDiscovery.unity` | Automatic spatial alignment | RQ1 testing |
| `SpaceSharing.unity` | Shared spatial anchors | Multi-user sync |
| `SpatialAnchorsBasics.unity` | Anchor fundamentals | Calibration study |

### Ensure MetricsLogger is Present
The `ColocationDiscovery.unity` scene already includes MetricsLogger. Verify:
1. Open the scene
2. Check hierarchy for "MetricsLogger" GameObject
3. Configure settings in Inspector:
   - `participantCount`: 3
   - `scenario`: "colocated_research"
   - `logInterval`: 1.0 (seconds)

---

## Step 4: Building for Quest 3

### Build Settings
1. **File → Build Settings**
2. **Switch Platform → Android**
3. **Player Settings:**
   - Texture Compression: ASTC
   - Minimum API Level: 29 (Android 10)
   - Target API Level: 32+
   - Scripting Backend: IL2CPP
   - Target Architectures: ARM64

### Build and Deploy
```bash
# Build APK from Unity
# Deploy to all three headsets:
adb -s <SERIAL1> install -r MRMotifs.apk
adb -s <SERIAL2> install -r MRMotifs.apk
adb -s <SERIAL3> install -r MRMotifs.apk
```

---

## Step 5: Running Research Sessions

### Pre-Session Checklist
- [ ] All headsets charged >95%
- [ ] USB debugging enabled on all Quest devices
- [ ] WiFi connected to same network
- [ ] Guardian boundaries configured
- [ ] Physical space cleared (6m × 6m minimum)
- [ ] Room temperature recorded
- [ ] Lighting conditions documented

### Session Protocol

#### 1. Start Host (Headset 1)
1. Launch MRMotifs app
2. Navigate to ColocationDiscovery scene
3. Host automatically starts advertising
4. Wait for "Advertisement started" log

#### 2. Join Clients (Headsets 2 & 3)
1. Launch MRMotifs app on each
2. Navigate to ColocationDiscovery scene
3. Clients auto-discover and align to host anchor
4. Verify "Colocation complete" log with calibration error

#### 3. Monitor Metrics
Watch for these in Unity logs (via `adb logcat -s Unity:*`):
```
[MetricsLogger] Session X started
[MetricsLogger] FPS: XX.X, Latency: XX.Xms, Calib: X.XXmm
Motif: Colocation complete. Calibration error: X.XXmm
```

### Session Duration Guidelines (from your paper)
| Duration | Status | Notes |
|----------|--------|-------|
| 0-45 min | Optimal | All metrics within thresholds |
| 45-60 min | Acceptable | May need recalibration |
| >60 min | Degraded | Thermal throttling likely |

---

## Step 6: Data Collection

### Automatic Collection
Metrics are automatically saved to:
```
/sdcard/Android/data/com.Prototype.MRMotifs/files/metrics/
```

Files generated:
- `session_X_HY_partZZZ_TIMESTAMP.csv` - Raw metrics
- `session_X_HY_metadata_partZZZ_TIMESTAMP.json` - Session info

### Using Collection Scripts

**Git Bash (Recommended):**
```bash
cd /c/github/Unity-MRMotifs
chmod +x scripts/collect_metrics.sh
./scripts/collect_metrics.sh my_research_session
```

**Windows CMD:**
```cmd
cd C:\github\Unity-MRMotifs
scripts\collect_metrics.bat my_research_session
```

### Manual Collection
```bash
# Set ADB path
ADB="/c/Program Files/Unity/Hub/Editor/6000.0.62f1/Editor/Data/PlaybackEngines/AndroidPlayer/SDK/platform-tools/adb.exe"

# List connected devices
"$ADB" devices

# Pull from each headset
MSYS_NO_PATHCONV=1 "$ADB" -s <SERIAL> pull \
  /sdcard/Android/data/com.Prototype.MRMotifs/files/metrics \
  ./metrics-data/H1/
```

---

## Step 7: Data Analysis

### CSV Structure
```csv
session_id,participant_count,timestamp_sec,frame_rate_fps,frame_time_ms,network_latency_ms,packet_loss_pct,calibration_error_mm,headset_temp_c,battery_level_pct,battery_temp_c,cpu_usage_pct,memory_usage_mb
```

### Key Metrics for Your Research

| Metric | Target | Literature Reference |
|--------|--------|---------------------|
| Network Latency | ≤75ms RTT | Van Damme et al. [1] |
| Calibration Error | <10mm | Reimer et al. [2] |
| Frame Rate | ≥90fps | Quest 3 target |
| Battery Temperature | <40°C | Thermal throttling threshold |

### Python Analysis Example
```python
import pandas as pd
import matplotlib.pyplot as plt

# Load all session data
df = pd.read_csv('metrics-data/session_1_H1_*.csv')

# Calculate statistics per your paper
print(f"Mean Latency: {df['network_latency_ms'].mean():.1f} ± {df['network_latency_ms'].std():.1f}ms")
print(f"Mean Calibration Error: {df['calibration_error_mm'].mean():.2f}mm")
print(f"Mean FPS: {df['frame_rate_fps'].mean():.1f}")

# Check against thresholds
latency_pass = (df['network_latency_ms'] <= 75).mean() * 100
print(f"Latency under 75ms: {latency_pass:.1f}%")
```

---

## Step 8: Addressing Research Questions

### RQ1: Automated Spatial Colocation
**Test Protocol:**
1. Run ColocationDiscovery scene on 3 headsets
2. Record discovery time (logged automatically)
3. Record calibration error from ColocationManager
4. Repeat 5+ times for statistical validity

**Key Logs:**
```
Motif: Discovered session with UUID: XXX. Discovery time: X.XXs
Motif: Colocation complete. Calibration error: X.XXmm
```

### RQ2: Performance Monitoring Infrastructure
**Verification:**
1. Run 45-minute session
2. Collect CSV data
3. Verify all columns populated
4. Check data sampling rate (~1Hz)

### RQ3: Networked Object Synchronization
**Test with SharedActivities samples:**
- Chess sample: Object state sync
- Movie sample: Playback sync across headsets
- Track synchronization latency via RTT metrics

### RQ4: Architecture Patterns
**Documentation:**
- Review `ColocationManager.cs` for alignment pattern
- Review `SharedSpatialAnchorManager.cs` for anchor sharing
- Review `MetricsLogger.cs` for monitoring pattern

### RQ5: Technical Feasibility
**Collect:**
- Extended session data (45-60 min)
- Thermal profiles (battery temperature over time)
- Frame rate degradation curves
- Calibration drift measurements

---

## Troubleshooting

### "No devices found"
```bash
"$ADB" kill-server
"$ADB" start-server
"$ADB" devices
```

### "Colocation failed"
- Ensure all headsets on same WiFi network
- Check Quest developer mode enabled
- Verify OVR Colocation permissions granted

### "MetricsLogger not saving"
- Check app has storage permissions
- Verify `enableMetricsLogging = true`
- Call `MetricsLogger.Instance.SaveMetricsNow()` manually

### "Network latency too high"
- Use 5GHz or 6GHz WiFi band
- Reduce interference (microwave, other devices)
- Enable QoS on router

---

## Quick Reference

### File Locations
| Component | Path |
|-----------|------|
| MetricsLogger | `Assets/MRMotifs/Shared Assets/Scripts/MetricsLogger.cs` |
| ColocationManager | `Assets/MRMotifs/ColocatedExperiences/Scripts/Colocation/ColocationManager.cs` |
| SharedSpatialAnchorManager | `Assets/MRMotifs/ColocatedExperiences/Scripts/Colocation/SharedSpatialAnchorManager.cs` |
| Collection Scripts | `scripts/collect_metrics.sh`, `scripts/collect_metrics.bat` |
| Research Scenes | `Assets/MRMotifs/ColocatedExperiences/Scenes/` |

### Key Commands
```bash
# List devices
"$ADB" devices

# Install APK
"$ADB" install -r MRMotifs.apk

# View logs
"$ADB" logcat -s Unity:* MetricsLogger:*

# Pull metrics
MSYS_NO_PATHCONV=1 "$ADB" pull /sdcard/Android/data/com.Prototype.MRMotifs/files/metrics ./data/

# Check battery/thermal
"$ADB" shell dumpsys battery
```

---

## Next Steps

1. **Build APK** from Unity with Android target
2. **Deploy** to all 3 Quest 3 headsets
3. **Run pilot session** (5-10 min) to verify setup
4. **Collect data** and verify CSV output
5. **Run full research sessions** (45-60 min each)
6. **Analyze data** per your paper's methodology

---

*Last Updated: December 4, 2025*
*Compatible with: Unity 6000.0.62f1, Meta XR SDK v81.0.0, Photon Fusion 1.1.0*
