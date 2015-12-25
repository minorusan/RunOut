using RunOut.Core.Controllers;
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

        #region Private
        static GameObject bonus;
        private static List<MovingGameObject> gameObjects = new List<MovingGameObject>();
		#endregion

		public static void ResetGameObjects()
		{
			gameObjects = new List<MovingGameObject> ();
		}

		public static void AddGameObjectToList(MovingGameObject gameObject)
		{
			gameObjects.Add(gameObject);
		}

        /// <summary>
        /// Relaunches deactivated GOs, that were registered
        /// </summary>
		public static void ManageGameObjects()
		{
			foreach (var meteor in gameObjects.Where(m => !m.gameObject.activeSelf))
			{
				meteor.gameObject.SetActive(true);
				meteor.Reset();
			}
		}

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

        internal static void DamagePlayer(int inputDamage)
        {
            if (Tools.IsPossibleToDamagePlayer())
            {
                PlayerStats.GetInstance().HealthValue -= inputDamage;
            }
            else
            {
                PlayerStats.GetInstance().ShieldValue -= inputDamage;
                if (PlayerStats.GetInstance().ShieldValue <= 0)
                {
                    Tools.DisposeBonus();
                }
            }
        }

        private static bool IsPossibleToDamagePlayer()
        {
            return !GameStats.GetInstance().IsSuperSpeedEnabled && PlayerStats.GetInstance().ShieldValue <= 0;
        }
    }
}
