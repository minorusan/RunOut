﻿using RunOut.Core.Controllers;
using RunOut.Core.GameObjects;
using RunOut.Core.GameObjects.Bonuses;
using RunOut.Utils;
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
                PlayerStats.GetInstance().IsImmune = true;
            }
            else if (SuperSpeedBonus.superSpeedTimer < 0)
            {
                PlayerStats.GetInstance().IsImmune = false;
                MovingGameObject.speedModifier = MovingGameObject.kDefaultSpeedModifier;
            }
        }

        internal static void DamagePlayer(int v)
        {
            if (!PlayerStats.GetInstance().IsImmune && !PlayerStats.GetInstance().IsShieldEnabled)
            {
                PlayerStats.GetInstance().Health -= v;
            }
            else
            {
                PlayerStats.GetInstance().ShieldValue -= v;
                if (!PlayerStats.GetInstance().IsShieldEnabled)
                {
                    Tools.DisposeBonus();
                }
            }
        }
    }
}
