// Copyright (c) Meta Platforms, Inc. and affiliates.

#if FUSION2
using UnityEngine;
using Meta.XR.Samples;

namespace MRMotifs.ColocatedExperiences.Colocation
{
    [MetaCodeSample("MRMotifs-ColocatedExperiences")]
    public class ColocationManager : MonoBehaviour
    {
        private Transform m_cameraRigTransform;
        private Vector3 m_preAlignmentPosition;
        private Quaternion m_preAlignmentRotation;
        private float m_currentCalibrationError = 0f;
        private Vector3 m_anchorPosition;
        private bool m_hasAligned = false;

        private void Awake()
        {
            m_cameraRigTransform = FindAnyObjectByType<OVRCameraRig>().transform;
        }

        /// <summary>
        /// Aligns the player's tracking space and camera rig to the specified anchor.
        /// </summary>
        /// <param name="anchor">The spatial anchor to align to.</param>
        public void AlignUserToAnchor(OVRSpatialAnchor anchor)
        {
            if (!anchor || !anchor.Localized)
            {
                Debug.LogError("Motif: Invalid or un-localized anchor. Cannot align.");
                return;
            }

            var anchorTransform = anchor.transform;

            // Store pre-alignment state for error calculation
            m_preAlignmentPosition = m_cameraRigTransform.position;
            m_preAlignmentRotation = m_cameraRigTransform.rotation;
            m_anchorPosition = anchorTransform.position;

            // Perform alignment
            m_cameraRigTransform.position = anchorTransform.InverseTransformPoint(Vector3.zero);
            m_cameraRigTransform.eulerAngles = new Vector3(0, -anchorTransform.eulerAngles.y, 0);

            // Calculate alignment error
            Vector3 postAlignmentPosition = m_cameraRigTransform.position;
            Vector3 positionDelta = postAlignmentPosition - m_anchorPosition;
            m_currentCalibrationError = positionDelta.magnitude * 1000f; // Convert to millimeters

            m_hasAligned = true;

            Debug.Log($"Motif: Alignment complete. Calibration error: {m_currentCalibrationError:F2}mm");
        }

        /// <summary>
        /// Gets the current calibration error in millimeters.
        /// Returns 0 if no alignment has been performed yet.
        /// </summary>
        public float GetCurrentCalibrationError()
        {
            return m_currentCalibrationError;
        }

        /// <summary>
        /// Validates calibration by measuring position delta from anchor.
        /// Used for periodic calibration checks during sessions.
        /// </summary>
        public float ValidateCalibration()
        {
            if (!m_hasAligned)
            {
                Debug.LogWarning("Motif: Cannot validate calibration - no alignment performed yet.");
                return 0f;
            }

            // Measure current drift from expected position
            Vector3 currentPosition = m_cameraRigTransform.position;
            Vector3 driftDelta = currentPosition - m_anchorPosition;
            float driftError = driftDelta.magnitude * 1000f; // Convert to millimeters

            m_currentCalibrationError = driftError;
            return driftError;
        }

        /// <summary>
        /// Resets calibration tracking state.
        /// </summary>
        public void ResetCalibration()
        {
            m_currentCalibrationError = 0f;
            m_hasAligned = false;
            Debug.Log("Motif: Calibration reset.");
        }

        /// <summary>
        /// Gets calibration status information for debugging.
        /// </summary>
        public string GetCalibrationStatus()
        {
            if (!m_hasAligned)
                return "No alignment performed yet.";

            return $"Calibration Error: {m_currentCalibrationError:F2}mm\n" +
                   $"Status: {(m_currentCalibrationError < 10f ? "PASS" : "WARNING")}\n" +
                   $"Anchor Position: {m_anchorPosition}";
        }
    }
}
#endif
