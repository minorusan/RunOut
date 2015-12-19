using UnityEngine;
using System.Collections;
using RunOut.Core.Controllers;

namespace RunOut.Utils
{
    public class CameraController : MonoBehaviour
    {
        private const int kCameraRotationSpeedModifier = 4;
        private readonly Vector3 kCameraBackRotationAngle = new Vector3(0, 90, 0);
        private readonly Vector3 kCameraTopRotationAngle = new Vector3(90, 0, 0);
        private readonly Vector3 kCameraSideRotationAngle = Vector3.zero;
        private readonly Vector3 kCameraDistanceFromPlayer = new Vector3(20, 20, 20);




        public static CurrentCameraState currentCameraState;
        public float cameraChangingSpeed = 70f;

        #region Private
        private Vector3 previousCameraTransformPosition;
        private Vector3 requiredCameraTransformPosition;
        private Vector3 previosCameraRotation;
        private Vector3 requiredCameraRotation;

        private Vector3 cameraSideTransformPosition;
        private Vector3 cameraBackTransformPosition;
        private Vector3 cameraTopTransformPosition;

        private float cameraPositionJourneyLength;
        private float cameraRotationJourneyLength;
        private float cameraInterpolationStartTime;
        #endregion

        public CurrentCameraState CurrentCameraState
        {
            get
            {
                return currentCameraState;
            }
        }

        private void FixedUpdate()
        {
            if (this.requiredCameraTransformPosition != this.previousCameraTransformPosition)
            {
                float distCovered = (Time.time - cameraInterpolationStartTime) * cameraChangingSpeed;
                float fracJourney = distCovered / cameraPositionJourneyLength;
                Camera.main.transform.position = Vector3.Lerp(previousCameraTransformPosition, requiredCameraTransformPosition, fracJourney);

                distCovered *= kCameraRotationSpeedModifier;
                fracJourney = distCovered / cameraRotationJourneyLength;
                Camera.main.transform.rotation = Quaternion.Euler(Vector3.Lerp(previosCameraRotation, requiredCameraRotation, fracJourney));
            }
        }

        public void ToggleUI()
        {
            switch (currentCameraState)
            {
                case CurrentCameraState.CameraStateSide:
                    {
                        currentCameraState = CurrentCameraState.CameraStateBack;

                        this.requiredCameraTransformPosition = this.cameraBackTransformPosition;
                        this.previousCameraTransformPosition = this.cameraSideTransformPosition;

                        this.requiredCameraRotation = kCameraBackRotationAngle;
                        this.previosCameraRotation = kCameraSideRotationAngle;

                        break;
                    }
                case CurrentCameraState.CameraStateBack:
                    {
                        currentCameraState = CurrentCameraState.CameraStateUp;

                        this.requiredCameraTransformPosition = this.cameraTopTransformPosition;
                        this.previousCameraTransformPosition = this.cameraBackTransformPosition;

                        this.requiredCameraRotation = kCameraTopRotationAngle;
                        this.previosCameraRotation = kCameraBackRotationAngle;

                        break;
                    }
                case CurrentCameraState.CameraStateUp:
                    {
                        currentCameraState = CurrentCameraState.CameraStateSide;

                        this.requiredCameraTransformPosition = this.cameraSideTransformPosition;
                        this.previousCameraTransformPosition = this.cameraTopTransformPosition;

                        this.requiredCameraRotation = kCameraSideRotationAngle;
                        this.previosCameraRotation = kCameraTopRotationAngle;

                        break;
                    }
                default:
                    break;
            }

            this.cameraInterpolationStartTime = Time.time;
            this.cameraPositionJourneyLength = Vector3.Distance(previousCameraTransformPosition, requiredCameraTransformPosition);
            this.cameraRotationJourneyLength = Vector3.Distance(previosCameraRotation, requiredCameraRotation);

        }

     
        // Use this for initialization
        private void Awake()
        {
            currentCameraState = CurrentCameraState.CameraStateSide;
            this.cameraSideTransformPosition = new Vector3(16, 10, -20);
            this.cameraBackTransformPosition = new Vector3(-15, 10, -2);
            this.cameraTopTransformPosition = new Vector3(16, 33, -2);
        }

      
    }
}

