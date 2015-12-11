using UnityEngine;
using System.Collections;
using RunOut.Utils;
using RunOut.Core.Controllers;

public class EnemyProjectile : MonoBehaviour {

    private const float kBeginScreenChekingFromThisPoint = -2f;
    public const float kDefaultSpeedModifier = 0f;
    public static float speedModifier;
    public float objectMovementSpeed = 10f;

    public GameObject acidExplosion;

    #region Private
    private Rigidbody body;
    private Vector3 initialPosition;
    private MeshRenderer meshRenderer;
    #endregion

    private void Start()
    {
        this.body = GetComponent<Rigidbody>();
        this.meshRenderer = GetComponent<MeshRenderer>();
        this.body.AddForce(new Vector3(-this.objectMovementSpeed, 0), ForceMode.VelocityChange);
    }




    private void FixedUpdate()
    {
        Vector3 movementVector = Vector3.zero;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
        switch (CameraController.currentCameraState)
        {
            case CurrentCameraState.CameraStateSide:
                movementVector = new Vector3(this.transform.position.x, this.transform.position.y, PlayerMovementController.PlayerPosition.z);
                break;
            case CurrentCameraState.CameraStateBack:
                movementVector = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
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

    private void OnCollisionEnter(Collision target)
    {
        Instantiate(this.acidExplosion, this.transform.position, this.transform.rotation);
    }

    private void CheckOutOfScreen()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (!GeometryUtility.TestPlanesAABB(planes, this.meshRenderer.bounds))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}

