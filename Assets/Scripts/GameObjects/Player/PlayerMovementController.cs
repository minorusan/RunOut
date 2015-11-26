using UnityEngine;
using System.Collections;
using CnControls;
using RunOut.Utils;

namespace RunOut.Core.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        private const float kVelocityChangeModifier = 2f;

        public AudioClip accelerationSoundUp;
        public AudioClip accelerationSoundDown;

        public float speed = 0.1F;

        public Boundary boundary;

        #region Private
        private AudioSource audioPlayer;
        private static Vector3 position;
        private Quaternion initialRotation;
        private Rigidbody body;
        #endregion

        public static Vector3 PlayerPosition
        {
            get
            {
                return position;
            }
        }

        private void Start()
        {
            this.body = this.GetComponent<Rigidbody>();
            this.audioPlayer = this.GetComponent<AudioSource>();
            this.initialRotation = this.transform.rotation;
        }

        private void Update()
        {
            var newx = CnInputManager.GetAxis(Strings.kHorizontalAxisName);
            var newy = CnInputManager.GetAxis(Strings.kVerticalAxisName);

            this.PlayAccelerationSound(newy);

            switch (CameraController.currentCameraState)
            {
                case CurrentCameraState.CameraStateSide:
                    this.body.AddForce(new Vector3(newx, newy, 0), ForceMode.VelocityChange);
                    break;
                case CurrentCameraState.CameraStateBack:
                    this.body.AddForce(new Vector3(0, newy, -newx), ForceMode.VelocityChange);
                    break;
                case CurrentCameraState.CameraStateUp:
                    this.body.AddForce(new Vector3(newx, 0, newy), ForceMode.VelocityChange);
                    break; 
                default:
                    break;
            }
        }

        private void PlayAccelerationSound(float newy)
        {
            if (newy < 0)
            {
                this.audioPlayer.PlayOneShot(this.accelerationSoundUp);
            }
            else
            {
                this.audioPlayer.PlayOneShot(this.accelerationSoundDown);
            }
        }

        private void FixedUpdate()
        {
            position = this.transform.position;

            this.body.position = new Vector3
            (
                Mathf.Clamp(this.body.position.x, this.boundary.xMin, this.boundary.xMax),
                Mathf.Clamp(this.body.position.y, this.boundary.yMin, this.boundary.yMax),
                Mathf.Clamp(this.body.position.z, this.boundary.zMin, this.boundary.zMax)
           );
        }

        private void LateUpdate()
        {
            transform.rotation = this.initialRotation;
        }

    }
}

