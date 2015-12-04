using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RunOut.Utils;
using RunOut.Core.GameObjects;
using RunOut.Core.GameObjects.Bonuses;
using RunOut.Core.Utilities;

namespace RunOut.Core.Controllers
{
    public class GameSceneController : MonoBehaviour
    {
        public static PlayerStats playerStats;
        public GameObject speedStars;

        #region Private
        private static List<MovingGameObject> gameObjects = new List<MovingGameObject>();
        #endregion

       

        private void Awake()
        {
            gameObjects = new List<MovingGameObject>();
            playerStats.ResetPlayerStats();
        }


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

            Tools.PerformSuperSpeedCheck();

            if (playerStats.Health <= 0)
            {
                Debug.LogError("Game ended");
                Application.LoadLevelAsync("Game");
            }
        }
    }
}

