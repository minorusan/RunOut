using UnityEngine;
using System.Collections;
using RunOut.Core.Controllers;
using RunOut.Core.Utilities;
using UnityEngineInternal;
using UnityEngine.Rendering;
using UnityEngine.Internal;

public class BonusExplosionEffect : MonoBehaviour {

    public GameObject explosion;
    public ParticleSystem explosionParticles;
    public Color effectColour;

    void Start()
    {
        Tools.RegisterBonus(this.gameObject);
    }


    void FixedUpdate()
    {
        this.transform.position = PlayerMovementController.PlayerPosition;
    }


    public void ActivateExplosion()
    {
        this.GetComponent<Done_DestroyByTime>().enabled = true;
        this.explosionParticles.startColor = this.effectColour;
        this.GetComponent<Light>().enabled = false;
       
        this.explosion.SetActive(true);
    }
}
