using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using RunOut.Utils;

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
            if (target.gameObject.tag.Equals(Strings.kPLayerTag) == true)
            {
                Instantiate(this.exlposion, this.transform.position, this.transform.rotation);

                AudioSource.PlayClipAtPoint(this.explosionSound, this.transform.position);

                target.rigidbody.velocity = Vector3.zero;

                target.rigidbody.AddExplosionForce(this.ExplosiveForce, this.transform.position, this.ExplosiveRadius);
                target.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                this.gameObject.SetActive(false);
            }
        }
    }
}
