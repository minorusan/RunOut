using UnityEngine;
using System.Collections;
using RunOut.Utils;
using RunOut.Core.Controllers;

namespace RunOut.Core.GameObjects.Bonuses
{
    public class ShieldBonus : MonoBehaviour
    {
        public GameObject shieldEffect;
        public AudioClip shieldTakenSound;

        private void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag.Equals(Strings.kPLayerTag))
            {
                Instantiate(this.shieldEffect, PlayerMovementController.PlayerPosition, Quaternion.Euler(Vector3.zero));
                this.shieldEffect.GetComponent<BonusExplosionEffect>().effectColour = Color.cyan ;
                AudioSource.PlayClipAtPoint(this.shieldTakenSound, this.transform.position);
                PlayerStats.GetInstance().ShieldValue = 300;
                Debug.Log("Shield enabled");
                this.gameObject.SetActive(false);
            }
        }
    }
}

