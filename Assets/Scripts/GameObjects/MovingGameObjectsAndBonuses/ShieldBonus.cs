using UnityEngine;
using System.Collections;
using RunOut.Utils;
using RunOut.Core.Controllers;

namespace RunOut.Core.GameObjects.Bonuses
{
    public class ShieldBonus : MonoBehaviour
    {
        public GameObject shieldEffect;

        private void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag.Equals(Strings.kPLayerTag))
            {
                Instantiate(this.shieldEffect, PlayerMovementController.PlayerPosition, Quaternion.Euler(Vector3.zero));
                this.shieldEffect.GetComponent<BonusExplosionEffect>().effectColour = Color.cyan ;
                GameSceneController.playerStats.IsShieldEnabled = true;
                Debug.Log("Shield enabled");
                this.gameObject.SetActive(false);
            }
        }
    }
}

