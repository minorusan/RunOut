using UnityEngine;
using System.Collections;
using RunOut.Core.Controllers;

namespace RunOut.Utils
{
    public class CameraController : MonoBehaviour
    {
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

                distCovered *= Constants.kCameraRotationSpeedModifier;
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

                        this.requiredCameraRotation = Constants.kCameraBackRotationAngle;
                        this.previosCameraRotation = Constants.kCameraSideRotationAngle;

                        break;
                    }
                case CurrentCameraState.CameraStateBack:
                    {
                        currentCameraState = CurrentCameraState.CameraStateUp;

                        this.requiredCameraTransformPosition = this.cameraTopTransformPosition;
                        this.previousCameraTransformPosition = this.cameraBackTransformPosition;

                        this.requiredCameraRotation = Constants.kCameraTopRotationAngle;
                        this.previosCameraRotation = Constants.kCameraBackRotationAngle;

                        break;
                    }
                case CurrentCameraState.CameraStateUp:
                    {
                        currentCameraState = CurrentCameraState.CameraStateSide;

                        this.requiredCameraTransformPosition = this.cameraSideTransformPosition;
                        this.previousCameraTransformPosition = this.cameraTopTransformPosition;

                        this.requiredCameraRotation = Constants.kCameraSideRotationAngle;
                        this.previosCameraRotation = Constants.kCameraTopRotationAngle;

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

