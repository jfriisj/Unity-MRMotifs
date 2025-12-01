A. Colocation System Implementation

1) Spatial Anchor-Based Alignment: The system implements
Meta's OVR Colocation Discovery API for automated spatial
correspondence between co-located headsets. The host device
creates a spatial anchor at a designated alignment point and
advertises it via Bluetooth Low Energy. Guest devices discover
the advertisement, retrieve the shared anchor UUID, and localize
to the same spatial reference frame.

The ColocationManager component performs camera rig align-
ment by transforming each headset's tracking space to match
the shared anchor's coordinate system. Alignment accuracy is
calculated as the Euclidean distance between the camera rig
position and anchor position after transformation, providing a
continuous metric for calibration quality assessment.

**Literature Comparison:** Reimer et al. [5] demonstrated that
manual hand tracking calibration can achieve ∼10mm accuracy
on Quest 1 devices. Our spatial anchor approach provides
automated alignment without manual marker placement, though
accuracy validation requires controlled measurement with ground
truth tracking systems (not conducted in this prototype).

2) Calibration Error Tracking: The ColocationManager
class includes GetCurrentCalibrationError() method that returns
alignment error in millimeters. This enables continuous monitor-
ing of spatial correspondence quality and detection of calibration
drift over extended sessions. The MetricsLogger system samples
this value at 1Hz for time-series analysis.

**Capability Assessment:** The system can measure and log
calibration accuracy, establishing infrastructure for future drift
analysis studies. Literature suggests spatial anchor localization
accuracy varies with environmental factors (lighting, feature
density, headset movement patterns), but systematic validation
requires controlled experimental protocols.

3) Multi-User Spatial Synchronization: Three demonstration
scenes showcase colocation capabilities:
- **ColocationDiscovery:** Implements host/guest workflow with
  anchor creation, advertisement, discovery, and alignment
- **SpaceSharing:** Demonstrates MRUK room anchor sharing for
  synchronized physical environment understanding
- **SpatialAnchorsBasics:** Shows basic spatial anchor persistence
  and loading functionality

B. Network Performance Infrastructure

1) Photon Fusion Integration: The system uses Photon Fusion
1.1.0 for state synchronization networking. NetworkRunner
provides RTT (Round Trip Time) monitoring via GetPlayerRtt()
method, enabling continuous latency measurement. The Metrics-
Logger queries this value at 1Hz and logs to CSV format.

**Literature Context:** Van Damme et al. [4] established that
≤75ms RTT provides good collaborative VR quality of experience,
while >300ms causes severe degradation. Local WiFi networking
in co-located configurations should theoretically achieve <30ms
latency due to direct router communication without internet
routing, but actual performance depends on network infrastruc-
ture quality, congestion, and interference.

**Capability Assessment:** The system can measure and log
network latency, but no performance characterization was con-
ducted. Variables affecting measured latency include: WiFi
standard (802.11ac vs. 802.11ax/WiFi 6), channel selection
(2.4GHz vs. 5GHz vs. 6GHz), router processing capabilities,
concurrent device count, and environmental interference.

2) State Synchronization Architecture: NetworkBehaviour
components enable synchronized object state across headsets.
The demonstration scenes include networked whiteboard panels,
bouncing balls, and avatar representations to showcase real-time
synchronization capabilities.

C. Performance Monitoring System

1) MetricsLogger Implementation: Custom MetricsLogger
class provides comprehensive performance data collection:

**Frame Rate Metrics:**
- FPS calculation from Time.deltaTime
- Frame time in milliseconds
- Continuous 1Hz sampling throughout sessions

**Network Metrics:**
- RTT from Photon Fusion GetPlayerRtt()
- Timestamp-synchronized across all measurements

**Calibration Metrics:**
- Alignment error from ColocationManager
- Spatial anchor position tracking
- Drift detection capability

**Device Metrics:**
- Battery level (Android BatteryManager API)
- Battery temperature (proxy for SoC thermal state)
- Estimated CPU usage (frame time-based approximation)
- Memory usage (Unity Profiler allocation tracking)

**Session Metadata:**
- Session ID, timestamp, duration
- Scenario type, interface configuration
- Environmental conditions (temperature, humidity, lighting)
- Participant count

2) Data Export Capabilities: The system logs metrics to CSV
files with accompanying JSON session metadata, stored in the
device's persistent data path. This enables post-session analysis
using standard data science tools (Python/pandas, R, MATLAB).

D. Performance Targets from Literature

The system design targets evidence-based requirements identified
through systematic literature review:

**Calibration Accuracy (<10mm):** Reimer et al. [5] established
this threshold for collision prevention in co-located VR. Spatial
anchor localization accuracy depends on environmental factors
and tracking quality. Validation requires controlled measurement
setup with ground truth position tracking.

**Network Latency (≤75ms):** Van Damme et al. [4] demonstrated
this threshold for good collaborative QoE. Local WiFi in co-
located settings should achieve substantially lower latency than
internet-routed distributed systems. Actual performance requires
empirical measurement under realistic load conditions.

**Frame Rate (≥90fps):** Schild et al. [1] identified frame rate
stability as critical for presence and usability. Quest 3 targets
90Hz refresh rate. The MetricsLogger tracks FPS to identify
performance degradation patterns over extended sessions.

**Visual Consistency:** Weiss et al. [7] showed asymmetric
representations degrade collaborative performance. The system
uses shared spatial anchors and symmetric NetworkBehaviour
synchronization to ensure consistent virtual content across all
headsets.

E. Technical Limitations and Validation Requirements

**No Empirical Validation Conducted:** This prototype has not
been tested with actual participants in structured training
scenarios. Performance assertions are based on system capabilities
and literature benchmarks, not measured data.

**Required for Empirical Validation:**
1. IRB Approval: Human subjects research requires institutional
   review board approval and informed consent protocols
2. Structured Scenarios: Design task-based training scenarios
   with measurable performance metrics
3. Participant Recruitment: Minimum 12 participants (4 groups
   × 3 users) for statistical power
4. Controlled Environment: Standardized physical space, lighting,
   network infrastructure
5. Extended Sessions: 30-60 minute sessions to assess thermal
   effects and calibration drift
6. Baseline Comparisons: Control conditions for interface
   effectiveness studies

**WIM Interface Not Implemented:** World-in-Miniature spatial
awareness interface is not implemented in the current prototype.
Chen et al. [6] demonstrated WIM effectiveness for collaborative
tasks, representing a high-priority future enhancement.

**Network Infrastructure Variability:** The system does not
mandate specific WiFi hardware. Actual performance depends on
user's available networking equipment. Literature values cited
represent potential performance under optimal conditions.

