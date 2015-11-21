using UnityEngine;
using System.Collections;

public class GravityPin : MonoBehaviour
{
    public GameObject gravityPinnedObject;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0 &&  Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            this.transform.position = Input.GetTouch(0).deltaPosition;

            var differenceVector = this.transform.position - this.gravityPinnedObject.transform.position;

            this.gravityPinnedObject.GetComponent<Rigidbody>().AddForce(differenceVector, ForceMode.Acceleration);
        }
    }
}
