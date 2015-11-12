using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

    public float ExplosiveForce = 20.0f;
    public float ExplosiveRadius = 30.0f;

    private Rigidbody body;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        this.body = GetComponent<Rigidbody>();
        this.meshRenderer = GetComponent<MeshRenderer>();
        GameSceneController.AddMeteorToList(this);
        this.Reset();
    }

	public void Reset ()
    {
        this.transform.position = new Vector3(23, PlayerMovement2D.PlayerPosition.y , PlayerMovement2D.PlayerPosition.z);
        this.body.velocity = Vector3.zero;
        this.transform.rotation = new Quaternion();
        this.body.angularVelocity = Vector3.forward;

        this.body.AddForce(new Vector3(-10f, 0), ForceMode.VelocityChange);

    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
       
        //this.transform.position = new Vector3(transform.position.x, transform.position.y, PlayerMovement2D.PlayerPosition.z);
        if (this.transform.position.x < 12)
            this.CheckOutOfScreen();
	}

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Player") == true)
        {
            target.rigidbody.AddExplosionForce(this.ExplosiveForce, this.transform.position, this.ExplosiveRadius);
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
