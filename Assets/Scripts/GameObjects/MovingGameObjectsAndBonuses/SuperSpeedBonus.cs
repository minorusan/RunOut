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
        private const float kSuperSpeedConstant = 1f;
		private PlayerMovementController player;
        public static float superSpeedTimer;
        public AudioClip bonusTakeSound;



        public GameObject stars;

        private void Awake()
        {
            superSpeedTimer = -3;
        }

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
                MovingGameObject.speedModifier = kSuperSpeedConstant;
                GameSceneController.playerStats.IsImmune = true;
                superSpeedTimer = 5f;
                this.gameObject.SetActive(false);
            }
        }

		private void Update()
		{
			if (superSpeedTimer <= 0 && superSpeedTimer >= -1 && this.player!=null) {
				this.player.regularEngine.SetActive(true);
				this.player.nitroEngine.SetActive(false);
			}
		}
    }
}
