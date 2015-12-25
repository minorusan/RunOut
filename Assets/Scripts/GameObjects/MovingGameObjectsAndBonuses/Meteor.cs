using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using RunOut.Utils;
using RunOut.Core.Controllers;
using RunOut.Core.Utilities;

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

                Tools.DamagePlayer(Constants.kDefaultMeteorDamage);
   
                AudioSource.PlayClipAtPoint(this.explosionSound, this.transform.position);

                target.rigidbody.AddExplosionForce(this.ExplosiveForce, this.transform.position, this.ExplosiveRadius);
                this.gameObject.SetActive(false);
            }
        }
    }
}
