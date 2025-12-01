// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

#if FUSION2
using Fusion;
#endif

namespace MRMotifs.SharedAssets
{
    /// <summary>
    /// Collects and logs technical performance metrics for VR training research.
    /// Tracks frame rate, network latency, calibration accuracy, and thermal performance.
    /// Based on metrics collection guide specifications.
    /// </summary>
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
            public int batteryLevel;
            public float batteryTemp;
            public float cpuUsage;
            public float memoryUsageMB;
        }

        [System.Serializable]
        public class SessionMetadata
        {
            public int sessionId;
            public string date;
            public string startTime;
            public string endTime;
            public int durationMinutes;
            public string scenario;
            public string interfaceType;
            public float roomTempStart;
            public float roomTempEnd;
            public int humidity;
            public string lighting;
        }

        [Header("Metrics Configuration")]
        [SerializeField] private bool enableMetricsLogging = true;
        [SerializeField] private float logInterval = 1.0f; // Log every 1 second
        [SerializeField] private float autoSaveInterval = 300f; // Auto-save every 5 minutes
        [SerializeField] private int maxMetricsInMemory = 10000; // Prevent memory overflow
        [SerializeField] private int participantCount = 3;
        [SerializeField] private string scenario = "colocated_experience";
        [SerializeField] private string interfaceType = "baseline";

        [Header("Environment Settings")]
        [SerializeField] private float roomTemperature = 22.5f;
        [SerializeField] private int roomHumidity = 45;
        [SerializeField] private string lightingCondition = "good";

        public static MetricsLogger Instance { get; private set; }

        private List<TechnicalMetric> metrics = new List<TechnicalMetric>();
        private float lastLogTime;
        private float lastAutoSaveTime;
        private int currentSessionId;
        private int saveCounter = 0;
        private DateTime sessionStartTime;
        private string metricsDirectory;
        private string deviceSerial;

#if FUSION2
        private NetworkRunner cachedRunner;
        private float lastRunnerCheckTime;
        private const float RUNNER_CHECK_INTERVAL = 1.0f;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        private AndroidJavaObject batteryManager;
        private AndroidJavaObject activityContext;
#endif

        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (!enableMetricsLogging)
            {
                Debug.Log("[MetricsLogger] Metrics logging is disabled.");
                return;
            }

            // Initialize session
            currentSessionId = PlayerPrefs.GetInt("SessionID", 1);
            sessionStartTime = DateTime.Now;
            lastLogTime = Time.realtimeSinceStartup;
            lastAutoSaveTime = Time.realtimeSinceStartup;
            deviceSerial = GetDeviceSerial();

            // Create persistent data directory
            metricsDirectory = Path.Combine(Application.persistentDataPath, "metrics");
            if (!Directory.Exists(metricsDirectory))
            {
                Directory.CreateDirectory(metricsDirectory);
            }

            // Initialize Android system access
            InitializeAndroidSystemAccess();

            Debug.Log($"[MetricsLogger] Session {currentSessionId} started. Metrics directory: {metricsDirectory}");
        }

        private void Update()
        {
            if (!enableMetricsLogging)
                return;

            float currentTime = Time.realtimeSinceStartup;

            // Regular metric logging
            if (currentTime - lastLogTime >= logInterval)
            {
                LogMetric();
                lastLogTime = currentTime;

                // Check memory limit
                if (metrics.Count >= maxMetricsInMemory)
                {
                    Debug.LogWarning($"[MetricsLogger] Memory limit reached ({maxMetricsInMemory}). Forcing save.");
                    SaveMetricsNow();
                    metrics.Clear();
                }
            }

            // Periodic auto-save
            if (currentTime - lastAutoSaveTime >= autoSaveInterval)
            {
                Debug.Log($"[MetricsLogger] Auto-save triggered after {autoSaveInterval}s.");
                SaveMetricsNow();
                metrics.Clear(); // Clear after auto-save to prevent duplication
                lastAutoSaveTime = currentTime;
            }
        }

        private void LogMetric()
        {
            TechnicalMetric metric = new TechnicalMetric
            {
                sessionId = currentSessionId,
                participantCount = participantCount,
                timestamp = Time.realtimeSinceStartup,
                frameRate = 1.0f / Time.deltaTime,
                frameTime = Time.deltaTime * 1000f,
                networkLatency = GetNetworkLatency(),
                packetLoss = GetPacketLoss(),
                calibrationError = GetCalibrationError(),
                headsetTemp = GetHeadsetTemperature(),
                batteryLevel = GetBatteryLevel(),
                batteryTemp = GetBatteryTemperature(),
                cpuUsage = GetCPUUsage(),
                memoryUsageMB = GetMemoryUsage()
            };

            // Validate metric data
            if (ValidateMetric(metric))
            {
                metrics.Add(metric);
            }
            else
            {
                Debug.LogWarning($"[MetricsLogger] Invalid metric detected and skipped at timestamp {metric.timestamp:F2}s");
            }

            // Optional: Log to console for debugging (can be disabled for performance)
            // Debug.Log($"[MetricsLogger] FPS: {metric.frameRate:F1}, Latency: {metric.networkLatency:F1}ms, Calib: {metric.calibrationError:F2}mm");
        }

        private float GetNetworkLatency()
        {
#if FUSION2
            // Check if we need to refresh the NetworkRunner reference
            if (Time.time - lastRunnerCheckTime > RUNNER_CHECK_INTERVAL || cachedRunner == null)
            {
                cachedRunner = FindAnyObjectByType<NetworkRunner>();
                lastRunnerCheckTime = Time.time;
            }

            if (cachedRunner != null && cachedRunner.IsRunning)
            {
                var localPlayer = cachedRunner.LocalPlayer;
                if (localPlayer != PlayerRef.None)
                {
                    // Get actual RTT (Round Trip Time) in milliseconds from Fusion
                    // GetPlayerRtt returns double, cast to float
                    return (float)cachedRunner.GetPlayerRtt(localPlayer);
                }
            }
#endif
            return 0f;
        }

        private float GetPacketLoss()
        {
#if FUSION2
            if (cachedRunner != null && cachedRunner.IsRunning)
            {
                var localPlayer = cachedRunner.LocalPlayer;
                if (localPlayer != PlayerRef.None)
                {
                    // Photon Fusion 2 does not directly expose packet loss in the standard API
                    // Packet loss would need to be calculated from custom network statistics
                    // or estimated from connection quality metrics
                    // For research purposes, this should be monitored via external tools
                    // or calculated from frame drops and synchronization issues
                    return 0f;
                }
            }
#endif
            return 0f;
        }

        private float GetCalibrationError()
        {
            // Try to get calibration error from ColocationManager
            var colocationManager = FindAnyObjectByType<MRMotifs.ColocatedExperiences.Colocation.ColocationManager>();
            if (colocationManager != null)
            {
                return colocationManager.GetCurrentCalibrationError();
            }
            return 0f;
        }

        private float GetHeadsetTemperature()
        {
            // Use actual battery temperature if available (best proxy for SoC temp on Quest)
            float batteryTemp = GetBatteryTemperature();
            if (batteryTemp > 0f)
            {
                return batteryTemp;
            }

            // Fallback: Temperature estimation based on performance degradation
            float targetFPS = 90f;
            float currentFPS = 1.0f / Time.deltaTime;
            float perfDegradation = Mathf.Max(0f, 1.0f - (currentFPS / targetFPS));

            // Estimate temperature range: 32-42°C based on performance
            float estimatedTemp = 32f + (perfDegradation * 10f);
            return Mathf.Clamp(estimatedTemp, 30f, 45f);
        }

        private void OnApplicationQuit()
        {
            if (!enableMetricsLogging || metrics.Count == 0)
                return;

            SaveMetricsToCSV();
            SaveSessionMetadata();

            Debug.Log($"[MetricsLogger] Session {currentSessionId} ended. Total metrics logged: {metrics.Count}");

            // Increment session ID for next run
            PlayerPrefs.SetInt("SessionID", currentSessionId + 1);
            PlayerPrefs.Save();
        }

        private void SaveMetricsToCSV()
        {
            if (metrics.Count == 0)
            {
                Debug.LogWarning("[MetricsLogger] No metrics to save.");
                return;
            }

            string headsetId = GetHeadsetID();
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            saveCounter++;
            string filename = $"session_{currentSessionId}_H{headsetId}_part{saveCounter:D3}_{timestamp}.csv";
            string filepath = Path.Combine(metricsDirectory, filename);

            try
            {
                using (StreamWriter writer = new StreamWriter(filepath, append: false))
                {
                    // Write header
                    writer.WriteLine("session_id,participant_count,timestamp_sec,frame_rate_fps,frame_time_ms,network_latency_ms,packet_loss_pct,calibration_error_mm,headset_temp_c,battery_level_pct,battery_temp_c,cpu_usage_pct,memory_usage_mb");

                    // Write metrics
                    foreach (var metric in metrics)
                    {
                        writer.WriteLine($"{metric.sessionId},{metric.participantCount},{metric.timestamp:F2}," +
                                       $"{metric.frameRate:F1},{metric.frameTime:F2},{metric.networkLatency:F1}," +
                                       $"{metric.packetLoss:F3},{metric.calibrationError:F2},{metric.headsetTemp:F1}," +
                                       $"{metric.batteryLevel},{metric.batteryTemp:F1},{metric.cpuUsage:F1},{metric.memoryUsageMB:F1}");
                    }
                }

                Debug.Log($"[MetricsLogger] {metrics.Count} metrics saved to: {filepath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[MetricsLogger] Failed to save metrics CSV: {e.Message}");
            }
        }

        private void SaveSessionMetadata()
        {
            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - sessionStartTime;

            SessionMetadata metadata = new SessionMetadata
            {
                sessionId = currentSessionId,
                date = sessionStartTime.ToString("yyyy-MM-dd"),
                startTime = sessionStartTime.ToString("HH:mm:ss"),
                endTime = endTime.ToString("HH:mm:ss"),
                durationMinutes = (int)duration.TotalMinutes,
                scenario = scenario,
                interfaceType = interfaceType,
                roomTempStart = roomTemperature,
                roomTempEnd = roomTemperature, // Could be updated if measured during session
                humidity = roomHumidity,
                lighting = lightingCondition
            };

            string headsetId = GetHeadsetID();
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string filename = $"session_{currentSessionId}_H{headsetId}_metadata_part{saveCounter:D3}_{timestamp}.json";
            string filepath = Path.Combine(metricsDirectory, filename);

            try
            {
                string json = JsonUtility.ToJson(metadata, true);
                File.WriteAllText(filepath, json);
                Debug.Log($"[MetricsLogger] Session metadata saved to: {filepath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[MetricsLogger] Failed to save session metadata: {e.Message}");
            }
        }

        private string GetHeadsetID()
        {
            // Use cached device serial if available
            if (!string.IsNullOrEmpty(deviceSerial))
            {
                // Extract last 4 characters as ID
                if (deviceSerial.Length >= 4)
                    return deviceSerial.Substring(deviceSerial.Length - 4);
            }

            // Try to determine headset ID from device name
            string deviceName = SystemInfo.deviceName;

            // Check for numeric identifiers in device name
            if (deviceName.Contains("1")) return "1";
            if (deviceName.Contains("2")) return "2";
            if (deviceName.Contains("3")) return "3";

            // Default to device unique identifier hash
            string deviceId = SystemInfo.deviceUniqueIdentifier;
            if (!string.IsNullOrEmpty(deviceId) && deviceId.Length >= 4)
            {
                return deviceId.Substring(deviceId.Length - 4);
            }

            return "Unknown";
        }

        /// <summary>
        /// Initialize Android system access for battery and thermal monitoring
        /// </summary>
        private void InitializeAndroidSystemAccess()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                    activityContext = activity.Call<AndroidJavaObject>("getApplicationContext");
                    batteryManager = activityContext.Call<AndroidJavaObject>("getSystemService", "batterymanager");
                }
                Debug.Log("[MetricsLogger] Android system access initialized.");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[MetricsLogger] Failed to initialize Android system access: {e.Message}");
            }
#endif
        }

        /// <summary>
        /// Get battery level percentage (0-100)
        /// </summary>
        private int GetBatteryLevel()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                if (batteryManager != null)
                {
                    // BATTERY_PROPERTY_CAPACITY = 4
                    return batteryManager.Call<int>("getIntProperty", 4);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[MetricsLogger] Failed to get battery level: {e.Message}");
            }
#endif
            return 0;
        }

        /// <summary>
        /// Get battery temperature in Celsius
        /// </summary>
        private float GetBatteryTemperature()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                if (batteryManager != null)
                {
                    // BATTERY_PROPERTY_TEMPERATURE = 6 (returns in tenths of degree Celsius)
                    int tempTenths = batteryManager.Call<int>("getIntProperty", 6);
                    return tempTenths / 10f;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[MetricsLogger] Failed to get battery temperature: {e.Message}");
            }
#endif
            return 0f;
        }

        /// <summary>
        /// Get CPU usage percentage (estimated from Unity profiler)
        /// </summary>
        private float GetCPUUsage()
        {
            // Unity doesn't expose direct CPU usage, but we can estimate from frame time
            // Higher frame time = more CPU usage
            float frameTimeMs = Time.deltaTime * 1000f;
            float targetFrameTime = 1000f / 72f; // Quest 3 baseline (72Hz)
            
            // Estimate CPU usage percentage
            float cpuEstimate = (frameTimeMs / targetFrameTime) * 100f;
            return Mathf.Clamp(cpuEstimate, 0f, 100f);
        }

        /// <summary>
        /// Get memory usage in MB
        /// </summary>
        private float GetMemoryUsage()
        {
            // Get allocated memory from Unity profiler
            long allocatedMemory = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong();
            return allocatedMemory / (1024f * 1024f); // Convert bytes to MB
        }

        /// <summary>
        /// Get device serial number (Android only)
        /// </summary>
        private string GetDeviceSerial()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                using (AndroidJavaClass buildClass = new AndroidJavaClass("android.os.Build"))
                {
                    string serial = buildClass.GetStatic<string>("SERIAL");
                    if (!string.IsNullOrEmpty(serial) && serial != "unknown")
                    {
                        Debug.Log($"[MetricsLogger] Device serial: {serial}");
                        return serial;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[MetricsLogger] Failed to get device serial: {e.Message}");
            }
#endif
            return SystemInfo.deviceUniqueIdentifier;
        }

        /// <summary>
        /// Validate metric data for unrealistic values
        /// </summary>
        private bool ValidateMetric(TechnicalMetric metric)
        {
            // Frame rate validation (10-200 FPS reasonable range)
            if (metric.frameRate < 10f || metric.frameRate > 200f)
            {
                Debug.LogWarning($"[MetricsLogger] Unrealistic FPS: {metric.frameRate:F1}");
                return false;
            }

            // Network latency validation (0-5000ms reasonable range)
            if (metric.networkLatency < 0f || metric.networkLatency > 5000f)
            {
                Debug.LogWarning($"[MetricsLogger] Unrealistic network latency: {metric.networkLatency:F1}ms");
                return false;
            }

            // Calibration error validation (0-1000mm reasonable range)
            if (metric.calibrationError < 0f || metric.calibrationError > 1000f)
            {
                Debug.LogWarning($"[MetricsLogger] Unrealistic calibration error: {metric.calibrationError:F2}mm");
                return false;
            }

            // Temperature validation (0-80°C reasonable range for device)
            if (metric.headsetTemp < 0f || metric.headsetTemp > 80f)
            {
                Debug.LogWarning($"[MetricsLogger] Unrealistic temperature: {metric.headsetTemp:F1}°C");
                return false;
            }

            // Battery level validation (0-100%)
            if (metric.batteryLevel < 0 || metric.batteryLevel > 100)
            {
                Debug.LogWarning($"[MetricsLogger] Invalid battery level: {metric.batteryLevel}%");
                return false;
            }

            // Memory usage validation (should be positive)
            if (metric.memoryUsageMB < 0f)
            {
                Debug.LogWarning($"[MetricsLogger] Invalid memory usage: {metric.memoryUsageMB:F1}MB");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Manually trigger metrics save (useful for testing or mid-session saves)
        /// </summary>
        public void SaveMetricsNow()
        {
            if (!enableMetricsLogging || metrics.Count == 0)
                return;

            SaveMetricsToCSV();
            SaveSessionMetadata();
            Debug.Log($"[MetricsLogger] Manual metrics save triggered. Saved {metrics.Count} metrics.");
        }

        /// <summary>
        /// Update session configuration during runtime
        /// </summary>
        public void UpdateSessionConfig(string newScenario, string newInterfaceType)
        {
            scenario = newScenario;
            interfaceType = newInterfaceType;
            Debug.Log($"[MetricsLogger] Session config updated: Scenario={scenario}, Interface={interfaceType}");
        }

        /// <summary>
        /// Get current metrics count for debugging
        /// </summary>
        public int GetMetricsCount()
        {
            return metrics.Count;
        }

        /// <summary>
        /// Get current session statistics
        /// </summary>
        public string GetSessionStats()
        {
            if (metrics.Count == 0)
                return "No metrics collected yet.";

            float avgFPS = 0f;
            float avgLatency = 0f;
            float avgCalibError = 0f;

            foreach (var metric in metrics)
            {
                avgFPS += metric.frameRate;
                avgLatency += metric.networkLatency;
                avgCalibError += metric.calibrationError;
            }

            avgFPS /= metrics.Count;
            avgLatency /= metrics.Count;
            avgCalibError /= metrics.Count;

            TimeSpan sessionDuration = DateTime.Now - sessionStartTime;

            return $"Session {currentSessionId} Stats:\n" +
                   $"Duration: {sessionDuration.TotalMinutes:F1} min\n" +
                   $"Avg FPS: {avgFPS:F1}\n" +
                   $"Avg Latency: {avgLatency:F1} ms\n" +
                   $"Avg Calib Error: {avgCalibError:F2} mm\n" +
                   $"Total Samples: {metrics.Count}";
        }
    }
}
