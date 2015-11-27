using UnityEngine;
using System.Collections;
using RunOut.Core.Controllers;
using RunOut.Utils;
using RunOut.Core.GameObjects.Bonuses;

namespace RunOut.Core.GameObjects
{
    public class MovingGameObject : MonoBehaviour
    {
        private const float kBeginScreenChekingFromThisPoint = -2f;
        public  const float kDefaultSpeedModifier = 0f;
        public static float speedModifier;
        public float objectMovementSpeed = 10f;


        #region Private
        private Rigidbody body;
        private Vector3 initialPosition;
        private MeshRenderer meshRenderer;
        #endregion

        private void Start()
        {
            this.body = GetComponent<Rigidbody>();
            this.meshRenderer = GetComponent<MeshRenderer>();
            this.initialPosition = this.transform.position;
            this.Reset();

            GameSceneController.AddGameObjectToList(this);
        }

        public void Reset()
        {
            this.transform.position = new Vector3(this.initialPosition.x, this.initialPosition.y, this.initialPosition.z);
            this.body.velocity = Vector3.zero;

            this.transform.rotation = new Quaternion();
            this.body.angularVelocity = Vector3.forward;

            this.body.AddForce(new Vector3(-this.objectMovementSpeed, 0), ForceMode.VelocityChange);
        }

        private void Update()
        {
            if (SuperSpeedBonus.superSpeedTimer > 0)
            {
                this.body.AddForce(new Vector3(-speedModifier, 0), ForceMode.VelocityChange);
            }
        }


        private void FixedUpdate()
        {
            Vector3 movementVector = Vector3.zero;

            switch (CameraController.currentCameraState)
            {
                case CurrentCameraState.CameraStateSide:
                    movementVector = new Vector3(this.transform.position.x, this.transform.position.y, PlayerMovementController.PlayerPosition.z);
                    break;
                case CurrentCameraState.CameraStateBack:
                    movementVector = new Vector3(this.transform.position.x, this.transform.position.y, this.initialPosition.z);
                    break;
                case CurrentCameraState.CameraStateUp:
                    movementVector = new Vector3(this.transform.position.x, PlayerMovementController.PlayerPosition.y, this.transform.position.z);
                    break;
                default:
                    break;
            }

            this.transform.position = movementVector;

            if (this.transform.position.x < kBeginScreenChekingFromThisPoint)
                this.CheckOutOfScreen();
        }

        private void CheckOutOfScreen()
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

            if (!GeometryUtility.TestPlanesAABB(planes, this.meshRenderer.bounds))
            {
                this.gameObject.SetActive(false);
            }

        }

    }
}

