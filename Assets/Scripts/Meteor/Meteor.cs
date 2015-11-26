using UnityEngine;
using System.Collections;
using Assets.Scripts.Utility;

public class Meteor : MonoBehaviour
{

    public float ExplosiveForce = 20.0f;
    public float ExplosiveRadius = 30.0f;

    public GameObject exlposion;
    public AudioClip explosionSound;


    #region Private
    private Rigidbody body;
    private Vector3 initialPosition;
    private MeshRenderer meshRenderer; 
    #endregion

    private void Awake()
    {
        this.body = GetComponent<Rigidbody>();
        this.meshRenderer = GetComponent<MeshRenderer>();
        GameSceneController.AddMeteorToList(this);
        this.initialPosition = this.transform.position;
        this.Reset();
    }

    public void Reset()
    {
        this.transform.position = new Vector3(this.initialPosition.x, this.initialPosition.y, this.initialPosition.z);
        this.body.velocity = Vector3.zero;

        this.transform.rotation = new Quaternion();
        this.body.angularVelocity = Vector3.forward;

        this.body.AddForce(new Vector3(-10f, 0), ForceMode.VelocityChange);
    }


    private void LateUpdate()
    {
        Vector3 movementVector = Vector3.zero;

        switch (GameSceneController.currentCameraState)
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

        if (this.transform.position.x < -2f)
            this.CheckOutOfScreen();
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals(Strings.kPLayerTag) == true)
        {
            Instantiate(this.exlposion, this.transform.position, this.transform.rotation);

            AudioSource.PlayClipAtPoint(this.explosionSound, this.transform.position);

            target.rigidbody.velocity = Vector3.zero;

            target.rigidbody.AddExplosionForce(this.ExplosiveForce, this.transform.position, this.ExplosiveRadius);
            target.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            this.gameObject.SetActive(false);
        }
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
