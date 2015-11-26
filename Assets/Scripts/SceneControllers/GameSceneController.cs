using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RunOut.Utils;
using RunOut.Core.GameObjects;
using RunOut.Core.GameObjects.Bonuses;

namespace RunOut.Core.Controllers
{
    public class GameSceneController : MonoBehaviour
    {
        #region Private
        private static List<MovingGameObject> gameObjects = new List<MovingGameObject>();
        #endregion

        public static void AddGameObjectToList(MovingGameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        private void Update()
        {
            foreach (var meteor in gameObjects.Where(m => !m.gameObject.activeSelf))
            {
                meteor.gameObject.SetActive(true);
                meteor.Reset();
            }


            if (SuperSpeedBonus.superSpeedTimer > 0)
            {
                SuperSpeedBonus.superSpeedTimer -= Time.deltaTime;
            }
            else
            {
                MovingGameObject.speedModifier = MovingGameObject.kDefaultSpeedModifier;
                Debug.Log("Turned off super speed");
            }



        }
    }
}

