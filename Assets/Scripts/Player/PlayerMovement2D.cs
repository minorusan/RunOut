using UnityEngine;
using System.Collections;

public class PlayerMovement2D : MonoBehaviour
{
    private const float kVelocitySmoothingFactor = 0.2f;
    private static Vector3 position;
    private bool isAccelerating;
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
    }

 
    void Update()
    {
        position = this.transform.position;
        if (this.isAccelerating)
        {
            this.body.AddForce(new Vector3(0.1f, 2f), ForceMode.VelocityChange);
        }
        this.ClampLocation();
    }

    private void ClampLocation()
    {
        

        this.body.velocity = new Vector3(this.body.velocity.x, Mathf.Clamp(this.body.velocity.y, -11f, 8f));
    }

    IEnumerator BringPlayerBack()
    {
        for (;;)
        {
            if (transform.position.x < 9f)
            {
                this.body.velocity = Vector3.right;
                this.transform.position = new Vector3(this.transform.position.x + kVelocitySmoothingFactor, this.transform.position.y);  
            }
            else if (transform.position.x > 17f)
            {
                this.body.velocity = Vector3.left;
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
