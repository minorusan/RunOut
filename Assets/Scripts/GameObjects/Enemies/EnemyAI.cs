using UnityEngine;
using System.Collections;
using RunOut.Core.Controllers;
using RunOut.Utils;

public class EnemyAI : MonoBehaviour {

    private const float kDefaultEnemyTrackingSpeed = 0.1f;

   
    public float EnemyTrakingSpeedModifier;

    private Rigidbody body;
    private float previousVelocity;
    private float previousZVelocity;
    // Use this for initialization
    void Start () {
        this.EnemyTrakingSpeedModifier = kDefaultEnemyTrackingSpeed;
        this.body = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        var selfPosition = this.transform.position;
        var playerPosition = PlayerMovementController.PlayerPosition;

        switch (CameraController.currentCameraState)
        {
            case CurrentCameraState.CameraStateSide:
                {
                    var differenceInHeight = selfPosition.y - playerPosition.y;

                   
                    var newYVelocityModifier = differenceInHeight > 0 ? -EnemyTrakingSpeedModifier : EnemyTrakingSpeedModifier;
                    if (newYVelocityModifier != previousVelocity)
                    {
                        this.body.velocity = Vector3.zero;
                    }

                    this.previousVelocity = newYVelocityModifier;
                    this.body.AddForce(new Vector3(0, newYVelocityModifier, 0), ForceMode.VelocityChange);
                    break;
                };
            case CurrentCameraState.CameraStateBack:
                {
                    var differenceInHeight = selfPosition.y - playerPosition.y;
                    var differenceInZ = selfPosition.z - playerPosition.z;

                    var newZVelocityModifier = differenceInZ > 0 ? -EnemyTrakingSpeedModifier : EnemyTrakingSpeedModifier;
                    var newYVelocityModifier = differenceInHeight > 0 ? -EnemyTrakingSpeedModifier : EnemyTrakingSpeedModifier;

                    if (newYVelocityModifier != previousVelocity)
                    {
                        this.body.velocity = new Vector3(this.body.velocity.x, 0, this.body.velocity.z);
                    }

                    if (newZVelocityModifier != previousZVelocity)
                    {
                        this.body.velocity = new Vector3(this.body.velocity.x, this.body.velocity.y, 0);
                    }
                    this.previousZVelocity = newZVelocityModifier;
                    this.previousVelocity = newYVelocityModifier;
                    this.body.AddForce(new Vector3(0, newYVelocityModifier, newZVelocityModifier), ForceMode.VelocityChange);
                    break;
                };
            case CurrentCameraState.CameraStateUp:
                {
                    var differenceInHeight = selfPosition.z - playerPosition.z;


                    var newYVelocityModifier = differenceInHeight > 0 ? -EnemyTrakingSpeedModifier : EnemyTrakingSpeedModifier;
                    if (newYVelocityModifier != previousVelocity)
                    {
                        this.body.velocity = Vector3.zero;
                    }

                    this.previousVelocity = newYVelocityModifier;
                    this.body.AddForce(new Vector3(0, 0, newYVelocityModifier), ForceMode.VelocityChange);
                    break;
                };
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (CameraController.currentCameraState)
        {
            case CurrentCameraState.CameraStateSide:
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, PlayerMovementController.PlayerPosition.z);
                break;
            case CurrentCameraState.CameraStateBack:
                break;
            case CurrentCameraState.CameraStateUp:
                this.transform.position = new Vector3(this.transform.position.x, PlayerMovementController.PlayerPosition.y, this.transform.position.z);
                break;
            default:
                break;
        }
    }
}
