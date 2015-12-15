using UnityEngine;
using System.Collections;
using RunOut.Core.Controllers;
using RunOut.Utils;


namespace RunOut.Core.Enemies
{
    public class EnemyCannon : MonoBehaviour
    {

        public EnemyProjectile shot;
        // Update is called once per frame
        void Update()
        {
            var selfPosition = this.transform.position;
            var playerPosition = PlayerMovementController.PlayerPosition;

            var differenceInHeight = CameraController.currentCameraState == CurrentCameraState.CameraStateUp ? selfPosition.z - playerPosition.z : selfPosition.y - playerPosition.y;

            if (Mathf.Abs(differenceInHeight) <= 1f && !this.shot.isActiveAndEnabled)
            {
                Instantiate(this.shot, this.transform.position, Quaternion.Euler(Vector3.zero));
            }
        }
    }
}

