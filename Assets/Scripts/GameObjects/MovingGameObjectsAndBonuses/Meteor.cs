using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using RunOut.Utils;
using RunOut.Core.Controllers;

namespace RunOut.Core.GameObjects
{
    class Meteor : MonoBehaviour
    {
        public float ExplosiveForce = 20.0f;
        public float ExplosiveRadius = 30.0f;

       

        public GameObject exlposion;
        public AudioClip explosionSound;

        void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag.Equals(Strings.kPLayerTag))
            {
                Instantiate(this.exlposion, this.transform.position, this.transform.rotation);

#warning REFACTOR
                if (!GameSceneController.playerStats.IsImmune && !GameSceneController.playerStats.IsShieldEnabled)
                {
                    GameSceneController.playerStats.Health--;
                }
                else if (GameSceneController.playerStats.IsVampiricEnabled)
                {
                    GameSceneController.playerStats.Health++;
                    GameSceneController.playerStats.IsVampiricEnabled = false;
                }
                else
                {
                    GameSceneController.playerStats.IsShieldEnabled = false;
                }
               

                AudioSource.PlayClipAtPoint(this.explosionSound, this.transform.position);

                target.rigidbody.velocity = Vector3.zero;

                target.rigidbody.AddExplosionForce(this.ExplosiveForce, this.transform.position, this.ExplosiveRadius);
                target.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                this.gameObject.SetActive(false);
            }
        }
    }
}
