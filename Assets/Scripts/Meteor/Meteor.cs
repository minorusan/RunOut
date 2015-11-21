using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

    public float ExplosiveForce = 20.0f;
    public float ExplosiveRadius = 30.0f;

    public GameObject exlposion;
    public AudioClip explosionSound;

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
        this.transform.position = new Vector3(30, PlayerMovement2D.PlayerPosition.y , PlayerMovement2D.PlayerPosition.z);
        this.body.velocity = Vector3.zero;

        this.transform.rotation = new Quaternion();
        this.body.angularVelocity = Vector3.forward;

        this.body.AddForce(new Vector3(-10f, 0), ForceMode.VelocityChange);
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, PlayerMovement2D.PlayerPosition.z);
        //this.transform.position = new Vector3(transform.position.x, transform.position.y, PlayerMovement2D.PlayerPosition.z);
        if (this.transform.position.x < 12)
            this.CheckOutOfScreen();
	}

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Player") == true)
        {
            Instantiate(this.exlposion, this.transform.position, this.transform.rotation);

            AudioSource.PlayClipAtPoint(this.explosionSound, this.transform.position);

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
