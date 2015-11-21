using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMax, yMax, xMin, yMin;
}

public class PlayerMovement2D : MonoBehaviour
{
    private const float kVelocitySmoothingFactor = 1f;

    public Boundary boundary;
    public bool isAccelerating;

    private static Vector3 position;
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
        Input.simulateMouseWithTouches = true;
        this.initialRotation = this.transform.rotation;
    }


    public float speed = 0.1F;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Get movement of the finger since last frame
            Vector3 touchDeltaPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float newX = touchDeltaPosition.x > this.transform.position.x ? 1f : -1f;
            float newY = touchDeltaPosition.y > this.transform.position.y ? 1f : -1f;

            this.body.velocity = Vector3.zero;

            Debug.Log(string.Format("{0}:{1}", newX, newY));
            // Move object across XY plane
            this.body.AddForce(new Vector3(newX, newY, 0), ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        this.body.angularVelocity = new Vector3(this.body.angularVelocity.x, this.body.angularVelocity.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, this.initialRotation, Time.deltaTime);
        position = this.transform.position;
        this.body.position = new Vector3
        (
            Mathf.Clamp(this.body.position.x, this.boundary.xMin, this.boundary.xMax),
            Mathf.Clamp(this.body.position.y, this.boundary.yMin, this.boundary.yMax),
            0.0f
       );


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
