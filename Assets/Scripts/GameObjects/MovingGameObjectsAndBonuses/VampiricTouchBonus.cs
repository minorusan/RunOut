using UnityEngine;
using System.Collections;
using RunOut.Utils;
using RunOut.Core.Controllers;

namespace RunOut.Core.GameObjects.Bonuses
{
    public class VampiricTouchBonus : MonoBehaviour
    {

        private void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag.Equals(Strings.kPLayerTag))
            {
                GameSceneController.playerStats.IsVampiricEnabled = true;
                Debug.Log("Vampiric enabled");
                this.gameObject.SetActive(false);
            }
        }
    }
}


