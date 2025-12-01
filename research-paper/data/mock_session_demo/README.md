# Mock Data for Co-Located VR System

**⚠️ IMPORTANT: This is MOCK DATA for demonstration purposes only.**

No actual user studies have been conducted. This data demonstrates:
1. Expected data formats from the MetricsLogger system
2. Realistic performance patterns based on Quest 3 specifications
3. Literature benchmark comparisons for future validation

---

## Data Files Overview

### Technical Performance Metrics
```
H1/session_demo_H1_20250201_140000.csv  (1,200 samples @ 1Hz)
H2/session_demo_H2_20250201_140000.csv  (1,200 samples @ 1Hz)
H3/session_demo_H3_20250201_140000.csv  (1,200 samples @ 1Hz)
```

**Columns:**
- `session_id`: Session identifier
- `participant_count`: Number of headsets (3)
- `timestamp_sec`: Time in seconds from session start
- `frame_rate_fps`: Frames per second (Target: ≥90)
- `frame_time_ms`: Frame rendering time in milliseconds
- `network_latency_ms`: Round-trip time via Photon Fusion (Target: ≤75ms)
- `packet_loss_pct`: Packet loss percentage
- `calibration_error_mm`: Spatial alignment error (Target: <10mm)
- `headset_temp_c`: Estimated device temperature

### Session Metadata
```
session_metadata.json
```

Contains:
- Session timing and duration
- Demo scenario information
- Participant/headset assignments
- Environmental conditions
- Network configuration
- Spatial anchor details
- Calibration events
- Incident log

### Demo Performance
```
demo_performance.csv
```

Tracks:
- Demo scenario completion times
- Anchor creation/discovery metrics
- Coordination and communication events
- Success indicators
- Observer notes

### Calibration Log
```
calibration_log.csv
```

Records:
- Initial calibration per headset
- Recalibration events
- Alignment errors
- Localization times
- Success status

---

## Data Characteristics

### Frame Rate Performance
- **Mean:** 87.9 fps
- **Min:** 85.0 fps
- **Target:** ≥90 fps (Quest 3 native refresh)
- **Pattern:** Slight degradation (~5%) over 20 minutes due to thermal effects
- **Literature Benchmark:** Schild et al. [1] identified frame rate stability as critical for presence

### Network Latency
- **Mean:** 25.5 ms
- **Max:** 49-56 ms (occasional spikes)
- **Target:** ≤75 ms for good QoE
- **Pattern:** Low baseline (~25ms) with periodic spikes every ~2 minutes
- **Literature Benchmark:** Van Damme et al. [4] established ≤75ms threshold

### Calibration Accuracy
- **Mean:** 5.6 mm
- **Max:** 9.0 mm
- **Target:** <10 mm for collision prevention
- **Pattern:** Gradual drift (~0.002 mm/sec), reset after recalibration at 10 minutes
- **Literature Benchmark:** Reimer et al. [5] demonstrated ~10mm accuracy threshold

### Temperature
- **Start:** 32°C
- **End:** 40°C
- **Increase:** ~8°C over 20 minutes
- **Pattern:** Linear increase suggesting thermal accumulation
- **Implication:** Extended sessions may require cooling periods

---

## Realistic Features

### 1. Thermal Degradation
Frame rate decreases slightly over time as device temperature increases, simulating real Quest 3 thermal throttling behavior.

### 2. Network Variability
Latency shows:
- Low baseline (local WiFi performance)
- Periodic spikes (realistic interference/congestion)
- Minimal packet loss (<0.2%)

### 3. Calibration Drift
Spatial anchor alignment shows:
- Initial accuracy: 3-6mm
- Gradual drift: 2μm per second
- Recovery after recalibration event

### 4. Realistic Incidents
- Network spike at 5:23 (48ms latency)
- Recalibration triggered at 10:00 (H2 exceeded 10mm threshold)

---

## How to Use This Data

### For Understanding Data Collection:
```python
import pandas as pd

# Load headset data
df_h1 = pd.read_csv('H1/session_demo_H1_20250201_140000.csv')
df_h2 = pd.read_csv('H2/session_demo_H2_20250201_140000.csv')
df_h3 = pd.read_csv('H3/session_demo_H3_20250201_140000.csv')

# Merge all headsets
df_h1['headset_id'] = 'H1'
df_h2['headset_id'] = 'H2'
df_h3['headset_id'] = 'H3'
df_all = pd.concat([df_h1, df_h2, df_h3])

# Calculate statistics
print(f"Mean FPS: {df_all['frame_rate_fps'].mean():.1f}")
print(f"Mean Latency: {df_all['network_latency_ms'].mean():.1f}ms")
print(f"Mean Calibration Error: {df_all['calibration_error_mm'].mean():.2f}mm")
```

### For Visualizing Expected Patterns:
```python
import matplotlib.pyplot as plt

# Plot frame rate over time
for headset in ['H1', 'H2', 'H3']:
    headset_data = df_all[df_all['headset_id'] == headset]
    plt.plot(headset_data['timestamp_sec']/60, 
             headset_data['frame_rate_fps'], 
             label=headset, alpha=0.7)
plt.axhline(90, color='g', linestyle='--', label='Target')
plt.xlabel('Time (minutes)')
plt.ylabel('FPS')
plt.title('Frame Rate Stability')
plt.legend()
plt.show()
```

### For Planning Actual Data Collection:
1. Review file formats and column names
2. Understand expected value ranges
3. Identify key metrics to monitor
4. Plan for realistic patterns (drift, spikes, degradation)
5. Prepare analysis scripts matching these formats

---

## Literature Benchmark Comparisons

| Metric | This Mock Data | Literature Target | Reference |
|--------|----------------|-------------------|-----------|
| Frame Rate | 87.9 fps mean | ≥90 fps | Schild et al. [1] |
| Network Latency | 25.5 ms mean | ≤75 ms | Van Damme et al. [4] |
| Calibration Error | 5.6 mm mean | <10 mm | Reimer et al. [5] |
| Visual Consistency | Symmetric (assumed) | Required | Weiss et al. [7] |

**Key Observations:**
- ✓ Network latency well below threshold (good local WiFi performance)
- ✓ Calibration accuracy maintained within safety limits
- ⚠ Frame rate slightly below target due to thermal effects
- ⚠ Temperature management needed for extended sessions

---

## Data Generation Details

**Method:** Python script using:
- `numpy` for statistical distributions
- `pandas` for data structure
- Random seed 42 for reproducibility

**Assumptions:**
- Quest 3 targets 90fps refresh rate
- Local WiFi achieves <30ms latency
- Spatial anchors maintain <10mm accuracy
- Thermal effects cause 5% performance degradation
- Calibration drift rate: 2μm/second

**Validation Against:**
- Quest 3 technical specifications
- Literature-reported benchmarks
- Consumer VR performance patterns
- Photon Fusion networking characteristics

---

## Important Disclaimers

### This is NOT Real Data
❌ No user studies conducted  
❌ No participants recruited  
❌ No IRB approval obtained  
❌ No actual headset measurements  

### This IS Useful For
✓ Understanding MetricsLogger capabilities  
✓ Planning future data collection protocols  
✓ Developing analysis scripts  
✓ Demonstrating expected data formats  
✓ Teaching data collection methodology  

### For Actual Research
To conduct real validation studies:
1. Obtain IRB approval
2. Recruit participants
3. Implement MetricsLogger in Unity project
4. Run controlled experiments
5. Collect actual measurements
6. Analyze real data against these benchmarks

---

## References

[1] Schild et al. (2018) - Paramedic VR training, presence and usability  
[4] Van Damme et al. (2024) - Network latency impact on collaborative VR QoE  
[5] Reimer et al. (2021) - Calibration methods for Quest devices  
[7] Weiss et al. (2025) - Visual consistency in collaborative VR  

---

**Version:** 1.0  
**Generated:** December 1, 2025  
**Purpose:** Technical demonstration and planning tool  
**Status:** Mock data for educational purposes only
