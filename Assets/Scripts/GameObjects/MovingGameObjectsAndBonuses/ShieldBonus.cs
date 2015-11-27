using UnityEngine;
using System.Collections;
using RunOut.Utils;
using RunOut.Core.Controllers;

namespace RunOut.Core.GameObjects.Bonuses
{
    public class ShieldBonus : MonoBehaviour
    {
        private void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag.Equals(Strings.kPLayerTag))
            {
                GameSceneController.playerStats.IsShieldEnabled = true;
                Debug.Log("Shield enabled");
                this.gameObject.SetActive(false);
            }
        }
    }
}

