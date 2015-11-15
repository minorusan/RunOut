using UnityEngine;
using System.Collections;

public class PlayerMovement2D : MonoBehaviour
{
    private const float kVelocitySmoothingFactor = 1f;
    private static Vector3 position;
    private bool isAccelerating;
    private Quaternion initialRotation;

    private Rigidbody body;

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
        this.initialRotation = this.transform.rotation;
    }

 
    void Update()
    {
        position = this.transform.position;
        if (this.isAccelerating)
        {
            this.body.AddForce(new Vector3(0, 1f), ForceMode.VelocityChange);
           
        }
        this.body.angularVelocity = new Vector3(this.body.angularVelocity.x, this.body.angularVelocity.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, this.initialRotation, Time.deltaTime);
        
    }

    public void BeginAcceleration()
    {
        this.isAccelerating = true;
    }

    public void EndAcceleration()
    {
        this.isAccelerating = false;
    }

}
