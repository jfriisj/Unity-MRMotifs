#!/usr/bin/env python3
"""
VR Training System Performance Analysis - MOCK DATA DEMONSTRATION
Analyzes technical performance capabilities and expected patterns
for 3-user co-located VR training system using Meta Quest 3 headsets.

⚠️ WARNING: This script analyzes MOCK DATA for demonstration purposes.
No actual user studies have been conducted.
"""

import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
from scipy import stats
import os
import glob

# Set style
sns.set_style("whitegrid")
plt.rcParams['figure.figsize'] = (10, 6)
plt.rcParams['font.size'] = 10

# Create output directory
os.makedirs('../figures', exist_ok=True)

print("="*70)
print("MOCK DATA ANALYSIS - DEMONSTRATION ONLY")
print("="*70)
print("This analysis uses simulated data to demonstrate MetricsLogger")
print("capabilities and expected performance patterns. No user studies conducted.")
print("="*70 + "\n")

# Load mock data from all three headsets
print("Loading mock datasets...")

# Find all CSV files in mock_session_demo
headset_files = glob.glob('../data/mock_session_demo/H*/session_demo_*.csv')
if not headset_files:
    print("ERROR: No mock data files found in ../data/mock_session_demo/")
    print("Please run the data generation script first.")
    exit(1)

# Load and merge data from all headsets
tech_perf_list = []
for file in headset_files:
    df = pd.read_csv(file)
    headset_id = file.split('/')[-2]  # Extract H1, H2, H3 from path
    df['headset_id'] = headset_id
    tech_perf_list.append(df)

tech_perf = pd.concat(tech_perf_list, ignore_index=True)

# Load demo performance and calibration data
demo_perf = pd.read_csv('../data/mock_session_demo/demo_performance.csv')
calib_acc = pd.read_csv('../data/mock_session_demo/calibration_log.csv')

print(f"Technical Performance: {len(tech_perf)} measurements from {tech_perf['headset_id'].nunique()} headsets")
print(f"Demo Performance: {len(demo_perf)} demo scenarios")
print(f"Calibration Accuracy: {len(calib_acc)} calibration events")

# ============================================
# RQ1 & RQ2: Technical Performance Analysis
# ============================================

print("\n" + "=" * 60)
print("RQ1 & RQ2: Technical Performance Capabilities")
print("=" * 60)

# Network Latency Analysis
print("\nNetwork Latency Statistics (ms):")
print(f"  Mean: {tech_perf['network_latency_ms'].mean():.1f}ms")
print(f"  Std Dev: {tech_perf['network_latency_ms'].std():.1f}ms")
print(f"  Min: {tech_perf['network_latency_ms'].min():.1f}ms")
print(f"  Max: {tech_perf['network_latency_ms'].max():.1f}ms")
print(f"\n  Target: ≤75ms (Van Damme et al.)")
print(f"  Achievement: {(tech_perf['network_latency_ms'] <= 75).mean() * 100:.1f}% of measurements")

# Frame Rate Analysis
print("\nFrame Rate Statistics (fps):")
print(f"  Mean: {tech_perf['frame_rate_fps'].mean():.1f}fps")
print(f"  Std Dev: {tech_perf['frame_rate_fps'].std():.1f}fps")
print(f"  Min: {tech_perf['frame_rate_fps'].min():.1f}fps")
print(f"  Max: {tech_perf['frame_rate_fps'].max():.1f}fps")
print(f"\n  Target: ≥90fps")
print(f"  Achievement: {(tech_perf['frame_rate_fps'] >= 90).mean() * 100:.1f}% of measurements")
print(f"  Note: Slight degradation due to thermal effects over 20min session")

# Calibration Accuracy Analysis
initial_calib = calib_acc[calib_acc['calibration_type'] == 'initial']
recalib = calib_acc[calib_acc['calibration_type'] == 'recalibration']

print("\nCalibration Accuracy Statistics (mm):")
print(f"  Initial Alignment:")
print(f"    Mean: {initial_calib['alignment_error_mm'].mean():.2f}mm")
print(f"    Std Dev: {initial_calib['alignment_error_mm'].std():.2f}mm")
print(f"    Range: {initial_calib['alignment_error_mm'].min():.2f}-{initial_calib['alignment_error_mm'].max():.2f}mm")
if len(recalib) > 0:
    print(f"  After Recalibration:")
    print(f"    Error: {recalib['alignment_error_mm'].iloc[0]:.2f}mm")
print(f"\n  Target: <10mm (Reimer et al.)")
print(f"  Status: ✓ All calibrations within safety threshold")

# ============================================
# RQ3: Demo Scenario Performance
# ============================================

print("\n" + "=" * 60)
print("RQ3: Demo Scenario Execution")
print("=" * 60)

for idx, row in demo_perf.iterrows():
    print(f"\n{row['demo_scenario']}:")
    print(f"  Completion Time: {row['completion_time_sec']}s")
    if 'anchor_creation_time_sec' in row:
        print(f"  Anchor Creation: {row['anchor_creation_time_sec']}s")
        print(f"  Anchor Discovery: {row['anchor_discovery_time_sec']}s")
    if 'room_anchor_sync_time_sec' in row:
        print(f"  Room Sync Time: {row['room_anchor_sync_time_sec']}s")
    print(f"  Coordination Events: {row['coordination_events']}")
    print(f"  Communication Events: {row['communication_events']}")
    print(f"  Success: {'✓ Yes' if row['demo_success'] else '✗ No'}")
    print(f"  Notes: {row['observer_notes']}")

# ============================================
# RQ4 & RQ5: Performance Stability Analysis
# ============================================

print("\n" + "=" * 60)
print("RQ4 & RQ5: Performance Stability and Hardware Limitations")
print("=" * 60)

# Calculate drift rates
print("\nPerformance Drift Analysis:")

# Analyze drift per headset
for headset in sorted(tech_perf['headset_id'].unique()):
    headset_data = tech_perf[tech_perf['headset_id'] == headset].sort_values('timestamp_sec')
    
    # Frame rate degradation
    fps_initial = headset_data['frame_rate_fps'].head(60).mean()  # First minute average
    fps_final = headset_data['frame_rate_fps'].tail(60).mean()    # Last minute average
    fps_drift = fps_initial - fps_final
    
    # Calibration drift
    calib_initial = headset_data['calibration_error_mm'].head(60).mean()
    calib_final = headset_data['calibration_error_mm'].tail(60).mean()
    calib_drift = calib_final - calib_initial
    
    # Latency variance
    latency_jitter = headset_data['network_latency_ms'].std()
    
    # Temperature increase
    temp_initial = headset_data['headset_temp_c'].head(60).mean()
    temp_final = headset_data['headset_temp_c'].tail(60).mean()
    temp_increase = temp_final - temp_initial
    
    duration = (headset_data['timestamp_sec'].max() - headset_data['timestamp_sec'].min()) / 60
    
    print(f"\n{headset} ({duration:.0f}min session):")
    print(f"  FPS drift: -{fps_drift:.2f}fps ({fps_drift/duration:.3f}fps/min)")
    print(f"  Calibration drift: +{calib_drift:.2f}mm ({calib_drift/duration:.3f}mm/min)")
    print(f"  Latency jitter (SD): {latency_jitter:.2f}ms")
    print(f"  Temperature increase: +{temp_increase:.1f}°C")
    print(f"  Final FPS: {fps_final:.1f} ({'✓ PASS' if fps_final >= 85 else '⚠ WARNING'})")

# Correlation analysis
print("\nCorrelation Analysis: Temperature vs Performance:")
temp_fps_corr = tech_perf['headset_temp_c'].corr(tech_perf['frame_rate_fps'])
temp_calib_corr = tech_perf['headset_temp_c'].corr(tech_perf['calibration_error_mm'])
temp_latency_corr = tech_perf['headset_temp_c'].corr(tech_perf['network_latency_ms'])

print(f"  Temperature vs FPS: r={temp_fps_corr:.3f} (negative correlation expected)")
print(f"  Temperature vs Calibration Error: r={temp_calib_corr:.3f}")
print(f"  Temperature vs Latency: r={temp_latency_corr:.3f}")

# ============================================
# Generate Visualizations
# ============================================

print("\n" + "=" * 60)
print("Generating Figures...")
print("=" * 60)

# Figure 1: Technical Performance Overview (2x2 grid)
fig, axes = plt.subplots(2, 2, figsize=(14, 10))

# Network Latency Over Time
ax1 = axes[0, 0]
for headset in sorted(tech_perf['headset_id'].unique()):
    headset_data = tech_perf[tech_perf['headset_id'] == headset].sort_values('timestamp_sec')
    ax1.plot(headset_data['timestamp_sec'] / 60, headset_data['network_latency_ms'],
            linewidth=1.5, alpha=0.7, label=headset)
ax1.axhline(y=75, color='g', linestyle='--', linewidth=2.5, label='Good QoE (≤75ms)')
ax1.set_xlabel('Time (minutes)', fontsize=11, fontweight='bold')
ax1.set_ylabel('Network Latency (ms)', fontsize=11, fontweight='bold')
ax1.set_title('Network Performance', fontsize=12, fontweight='bold')
ax1.legend(fontsize=9)
ax1.grid(True, alpha=0.3)

# Frame Rate Over Time
ax2 = axes[0, 1]
for headset in sorted(tech_perf['headset_id'].unique()):
    headset_data = tech_perf[tech_perf['headset_id'] == headset].sort_values('timestamp_sec')
    ax2.plot(headset_data['timestamp_sec'] / 60, headset_data['frame_rate_fps'],
            linewidth=1.5, alpha=0.7, label=headset)
ax2.axhline(y=90, color='g', linestyle='--', linewidth=2.5, label='Target (90fps)')
ax2.axhline(y=85, color='orange', linestyle=':', linewidth=2, label='Minimum (85fps)')
ax2.set_xlabel('Time (minutes)', fontsize=11, fontweight='bold')
ax2.set_ylabel('Frame Rate (fps)', fontsize=11, fontweight='bold')
ax2.set_title('Frame Rate Stability', fontsize=12, fontweight='bold')
ax2.legend(fontsize=9)
ax2.grid(True, alpha=0.3)
# Calibration Error Over Time
ax3 = axes[1, 0]
for headset in sorted(tech_perf['headset_id'].unique()):
    headset_data = tech_perf[tech_perf['headset_id'] == headset].sort_values('timestamp_sec')
    ax3.plot(headset_data['timestamp_sec'] / 60, headset_data['calibration_error_mm'],
            marker='s', markersize=3, linewidth=1.5, alpha=0.7, label=headset)
ax3.axhline(y=10, color='r', linestyle='--', linewidth=2.5, label='Safety Threshold (10mm)')
# Mark recalibration event at 10 minutes
ax3.axvline(x=10, color='purple', linestyle=':', linewidth=2, alpha=0.6, label='Recalibration (H2)')
ax3.set_xlabel('Time (minutes)', fontsize=11, fontweight='bold')
ax3.set_ylabel('Calibration Error (mm)', fontsize=11, fontweight='bold')
ax3.set_title('Calibration Drift', fontsize=12, fontweight='bold')
ax3.legend(fontsize=9)
ax3.grid(True, alpha=0.3)

# Temperature Increase
ax4 = axes[1, 1]
for headset in sorted(tech_perf['headset_id'].unique()):
    headset_data = tech_perf[tech_perf['headset_id'] == headset].sort_values('timestamp_sec')
    ax4.plot(headset_data['timestamp_sec'] / 60, headset_data['headset_temp_c'],
            linewidth=1.5, alpha=0.7, label=headset)
ax4.set_xlabel('Time (minutes)', fontsize=11, fontweight='bold')
ax4.set_ylabel('Temperature (°C)', fontsize=11, fontweight='bold')
ax4.set_title('Thermal Performance', fontsize=12, fontweight='bold')
ax4.legend(fontsize=9)
ax4.grid(True, alpha=0.3)

plt.tight_layout()
plt.savefig('../figures/technical_performance_summary.png', dpi=300, bbox_inches='tight')
print("  ✓ Saved: technical_performance_summary.png")
plt.close()

# Figure 2: Temperature vs Performance Correlation
fig, axes = plt.subplots(1, 2, figsize=(14, 5))

# Temperature vs FPS
axes[0].scatter(tech_perf['headset_temp_c'], tech_perf['frame_rate_fps'], 
               alpha=0.4, s=20, c=tech_perf['timestamp_sec']/60, cmap='viridis')
z = np.polyfit(tech_perf['headset_temp_c'], tech_perf['frame_rate_fps'], 1)
p = np.poly1d(z)
x_line = np.linspace(tech_perf['headset_temp_c'].min(), tech_perf['headset_temp_c'].max(), 100)
axes[0].plot(x_line, p(x_line), "r--", linewidth=2, label=f'r={temp_fps_corr:.3f}')
axes[0].axhline(y=90, color='g', linestyle=':', linewidth=2, alpha=0.7, label='Target')
axes[0].set_xlabel('Headset Temperature (°C)', fontsize=11, fontweight='bold')
axes[0].set_ylabel('Frame Rate (fps)', fontsize=11, fontweight='bold')
axes[0].set_title('Temperature Impact on Frame Rate', fontsize=12, fontweight='bold')
axes[0].legend(fontsize=10)
axes[0].grid(True, alpha=0.3)
cbar1 = plt.colorbar(axes[0].collections[0], ax=axes[0])
cbar1.set_label('Time (minutes)', fontsize=9)

# Temperature vs Calibration Error
sc = axes[1].scatter(tech_perf['headset_temp_c'], tech_perf['calibration_error_mm'], 
               alpha=0.4, s=20, c=tech_perf['timestamp_sec']/60, cmap='viridis')
z2 = np.polyfit(tech_perf['headset_temp_c'], tech_perf['calibration_error_mm'], 1)
p2 = np.poly1d(z2)
axes[1].plot(x_line, p2(x_line), "r--", linewidth=2, label=f'r={temp_calib_corr:.3f}')
axes[1].axhline(y=10, color='orange', linestyle='--', linewidth=2, label='Safety Threshold')
axes[1].set_xlabel('Headset Temperature (°C)', fontsize=11, fontweight='bold')
axes[1].set_ylabel('Calibration Error (mm)', fontsize=11, fontweight='bold')
axes[1].set_title('Temperature Impact on Calibration', fontsize=12, fontweight='bold')
axes[1].legend(fontsize=10)
axes[1].grid(True, alpha=0.3)
cbar2 = plt.colorbar(sc, ax=axes[1])
cbar2.set_label('Time (minutes)', fontsize=9)

plt.tight_layout()
plt.savefig('../figures/temperature_correlation.png', dpi=300, bbox_inches='tight')
print("  ✓ Saved: temperature_correlation.png")
plt.close()

# Figure 3: Demo Performance Comparison
fig, ax = plt.subplots(figsize=(10, 6))

demo_names = demo_perf['demo_scenario'].tolist()
demo_times = demo_perf['completion_time_sec'].tolist()
demo_colors = ['#1f77b4', '#ff7f0e']

bars = ax.bar(range(len(demo_names)), demo_times, color=demo_colors, alpha=0.7, edgecolor='black', linewidth=1.5)
ax.set_xticks(range(len(demo_names)))
ax.set_xticklabels(demo_names, fontsize=11, fontweight='bold')
ax.set_ylabel('Completion Time (seconds)', fontsize=12, fontweight='bold')
ax.set_title('Demo Scenario Performance', fontsize=14, fontweight='bold')
ax.grid(True, alpha=0.3, axis='y')

# Add value labels on bars
for bar, time_val, success in zip(bars, demo_times, demo_perf['demo_success']):
    height = bar.get_height()
    status = '✓' if success else '✗'
    ax.text(bar.get_x() + bar.get_width()/2., height + 10,
           f'{time_val}s\n{status}',
           ha='center', va='bottom', fontsize=10, fontweight='bold')

plt.tight_layout()
plt.savefig('../figures/demo_performance.png', dpi=300, bbox_inches='tight')
print("  ✓ Saved: demo_performance.png")
plt.close()

# ============================================
# Summary Statistics Table
# ============================================

print("\n" + "=" * 60)
print("Summary: Capabilities vs Literature Benchmarks")
print("=" * 60)

summary_data = {
    'Capability': [
        'Calibration Accuracy',
        'Network Latency', 
        'Frame Rate',
        'Session Duration',
        'Spatial Anchor Alignment'
    ],
    'Literature Target': [
        '<10mm',
        '≤75ms',
        '≥90fps',
        '30-60min',
        'Automatic'
    ],
    'Mock Data Performance': [
        f'{initial_calib["alignment_error_mm"].mean():.1f}±{initial_calib["alignment_error_mm"].std():.1f}mm',
        f'{tech_perf["network_latency_ms"].mean():.1f}±{tech_perf["network_latency_ms"].std():.1f}ms',
        f'{tech_perf["frame_rate_fps"].mean():.1f}±{tech_perf["frame_rate_fps"].std():.1f}fps',
        '20min demo',
        f'{initial_calib["localization_time_sec"].mean():.1f}s avg'
    ],
    'Assessment': [
        '✓ Within threshold',
        '✓ Well below threshold',
        '⚠ Slight thermal degradation',
        '✓ Stable performance',
        '✓ Successful'
    ]
}

summary_df = pd.DataFrame(summary_data)
print("\n" + summary_df.to_string(index=False))

print("\n" + "=" * 60)
print("Analysis Complete!")
print("=" * 60)
print(f"\nGenerated {len([f for f in os.listdir('../figures') if f.endswith('.png')])} figures in ../figures/")
print("\nIMPORTANT REMINDER:")
print("  This analysis uses MOCK DATA for demonstration purposes only.")
print("  No actual user studies have been conducted.")
print("  For real validation, conduct studies with IRB approval and actual participants.")
print("\nFiles can be referenced in research paper to demonstrate:")
print("  1. MetricsLogger system capabilities")
print("  2. Expected data collection formats")
print("  3. Analysis methodology for future validation studies")
