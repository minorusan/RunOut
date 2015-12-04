using UnityEngine;
using System.Collections;
using RunOut.Core.Controllers;
using RunOut.Core.Utilities;

public class BonusExplosionEffect : MonoBehaviour {

    public GameObject explosion;
    public GameObject halo;
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
        Component halo = GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
        this.explosion.SetActive(true);
    }
}
