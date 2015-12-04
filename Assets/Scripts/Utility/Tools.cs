using RunOut.Core.Controllers;
using RunOut.Core.GameObjects;
using RunOut.Core.GameObjects.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RunOut.Core.Utilities
{
    class Tools
    {
        static GameObject bonus;

        public static void RegisterBonus(GameObject effect)
        {
            GameObject.Destroy(bonus);
            bonus = effect;
        }

        public static void DisposeBonus()
        {
            if (bonus != null)
            {
                bonus.GetComponent<BonusExplosionEffect>().ActivateExplosion();
            }
        }


        public static void PerformSuperSpeedCheck()
        {
            if (SuperSpeedBonus.superSpeedTimer > 0)
            {
                SuperSpeedBonus.superSpeedTimer -= Time.deltaTime;
                GameSceneController.playerStats.IsImmune = true;
            }
            else if (SuperSpeedBonus.superSpeedTimer < 0)
            {
                GameSceneController.playerStats.IsImmune = false;
                MovingGameObject.speedModifier = MovingGameObject.kDefaultSpeedModifier;
            }
        }

    }
}
