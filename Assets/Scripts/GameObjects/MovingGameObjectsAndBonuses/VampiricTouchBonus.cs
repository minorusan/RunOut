using UnityEngine;
using System.Collections;
using RunOut.Utils;
using RunOut.Core.Controllers;
using RunOut.Core.Utilities;

namespace RunOut.Core.GameObjects.Bonuses
{
    public class VampiricTouchBonus : MonoBehaviour
    {
        public GameObject bloodEffect;
        public AudioClip bonusTakeSound;

        private void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag.Equals(Strings.kPLayerTag))
            {
                GetComponent<AudioSource>().PlayOneShot(this.bonusTakeSound);
                Instantiate(this.bloodEffect, PlayerMovementController.PlayerPosition, Quaternion.Euler(Vector3.zero));
                AudioSource.PlayClipAtPoint(this.bonusTakeSound, this.transform.position);

                this.bloodEffect.GetComponent<BonusExplosionEffect>().effectColour = Color.red;

                Debug.LogError("Vampiric does not work!");
                this.gameObject.SetActive(false);
            }
        }
    }
}


