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
        this.StartCoroutine("BringPlayerBack");
        this.StartCoroutine("BringPlayerDown");
        this.initialRotation = this.transform.rotation;
    }

 
    void Update()
    {
        position = this.transform.position;
        if (this.isAccelerating)
        {
            this.body.AddForce(new Vector3(0.1f, 2f), ForceMode.VelocityChange);
           
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, this.initialRotation, Time.deltaTime);
        this.body.velocity = new Vector3(this.body.velocity.x, Mathf.Clamp(this.body.velocity.y, -11f, 8f));
    }

    IEnumerator BringPlayerBack()
    {
        for (;;)
        {
            if (transform.position.x < 9f)
            {

                this.body.velocity = new Vector3(this.body.velocity.x + 0.2f, this.transform.position.y);  
            }
            else if (transform.position.x > 17f)
            {
                this.body.velocity = Vector3.left;
            }
            yield return null;
        }
    }

    IEnumerator BringPlayerDown()
    {
        for (;;)
        {
            if (transform.position.y > 9f)
            {
                this.body.velocity = new Vector3(this.body.velocity.x, - kVelocitySmoothingFactor);
            }
            yield return null;
        }
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
