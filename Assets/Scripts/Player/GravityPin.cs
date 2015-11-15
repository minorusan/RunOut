using UnityEngine;
using System.Collections;

public class GravityPin : MonoBehaviour {


    public GameObject gravityPinnedObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var differenceVector = this.transform.position - this.gravityPinnedObject.transform.position;
        if (!CheckOutOfScreen())
        {
            this.gravityPinnedObject.GetComponent<Rigidbody>().velocity = this.gravityPinnedObject.GetComponent<Rigidbody>().velocity/2;
            this.gravityPinnedObject.GetComponent<Rigidbody>().AddForce(differenceVector, ForceMode.Acceleration);
        }
	}

    private bool CheckOutOfScreen()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (!GeometryUtility.TestPlanesAABB(planes, this.gravityPinnedObject.GetComponentInChildren<MeshRenderer>().bounds))
        {
            Debug.Log("Out");

            return false;
        }
        return true;
    }
}
