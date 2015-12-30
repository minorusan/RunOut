using RunOut.Core.Controllers;
using RunOut.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RunOut.Core.GameObjects.Bonuses
{
    class SuperSpeedBonus:MonoBehaviour
    {
        
		private PlayerMovementController player;

        public AudioClip bonusTakeSound;
        public GameObject stars;


        private void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag.Equals(Strings.kPLayerTag))
            {
                Instantiate(stars);
				this.player = target.gameObject.GetComponent<PlayerMovementController>();
				player.regularEngine.SetActive(false);
				player.nitroEngine.SetActive(true);
                this.GetComponent<AudioSource>().PlayOneShot(this.bonusTakeSound);

                AudioSource.PlayClipAtPoint(this.bonusTakeSound, this.transform.position);

                this.gameObject.SetActive(false);
            }
        }

    }
}
