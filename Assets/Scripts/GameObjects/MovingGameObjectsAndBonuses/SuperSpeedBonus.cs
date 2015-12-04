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
        public static float superSpeedTimer;

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
                MovingGameObject.speedModifier = kSuperSpeedConstant;
                GameSceneController.playerStats.IsImmune = true;
                superSpeedTimer = 5f;
                this.gameObject.SetActive(false);
            }
        }
    }
}
