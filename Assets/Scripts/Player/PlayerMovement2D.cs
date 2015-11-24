using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMax, yMax, xMin, yMin, zMin, zMax;
}

public class PlayerMovement2D : MonoBehaviour
{
    private const float kVelocityChangeModifier = 2f;

    public AudioClip accelerationSoundUp;
    public AudioClip accelerationSoundDown;

    public Boundary boundary;
    public bool isAccelerating;

    private AudioSource audioPlayer;
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
        this.audioPlayer = this.GetComponent<AudioSource>();
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
            float newX = touchDeltaPosition.x > this.transform.position.x ? kVelocityChangeModifier : -kVelocityChangeModifier;
            float newY = touchDeltaPosition.y > this.transform.position.y ? kVelocityChangeModifier : -kVelocityChangeModifier;
            float newZ = touchDeltaPosition.z > this.transform.position.z ? kVelocityChangeModifier : -kVelocityChangeModifier;

            this.body.velocity = Vector3.zero;

            if (newY < 0)
            {
                this.audioPlayer.PlayOneShot(this.accelerationSoundUp);
            }
            else
            {
                this.audioPlayer.PlayOneShot(this.accelerationSoundDown);
            }

            this.body.AddForce(new Vector3(newX, newY, 0), ForceMode.VelocityChange);

        }
    }

    void FixedUpdate()
    {
       // this.body.angularVelocity = new Vector3(this.body.angularVelocity.x, this.body.angularVelocity.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, this.initialRotation, 0.1f);
        position = this.transform.position;
        this.body.position = new Vector3
        (
            Mathf.Clamp(this.body.position.x, this.boundary.xMin, this.boundary.xMax),
            Mathf.Clamp(this.body.position.y, this.boundary.yMin, this.boundary.yMax),
            Mathf.Clamp(this.body.position.z, this.boundary.zMin, this.boundary.zMax)
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
