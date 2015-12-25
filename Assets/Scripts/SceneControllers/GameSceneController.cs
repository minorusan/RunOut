using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RunOut.Utils;
using RunOut.Core.GameObjects;
using RunOut.Core.GameObjects.Bonuses;
using RunOut.Core.Utilities;
using UnityEngine.SceneManagement;


namespace RunOut.Core.Controllers
{
    public class GameSceneController : MonoBehaviour
    {
        public GameObject speedStars;
        public BarSizeChanger healthBar;
        public BarSizeChanger shieldBar;

        #region Private
        private static List<MovingGameObject> gameObjects = new List<MovingGameObject>();
        #endregion

		private void Awake()
		{
			PlayerStats.GetInstance().ResetPlayerStats();
			PlayerStats.GetInstance().PlayerHeathEventChanged += GameSceneController_PlayerHeathEventChanged;
			PlayerStats.GetInstance().PlayerShieldEventChanged += GameSceneController_PlayerShieldEventChanged;
		}

        private void Start()
        {
            if (this.shieldBar != null)
            {
                this.shieldBar.UpdateWithValue(1);
			} 
        }

        private void GameSceneController_PlayerShieldEventChanged(PlayerStatChangedEventArgs args, object sender)
        {
            this.shieldBar.UpdateWithValue(args.NewValue);
        }

        private void GameSceneController_PlayerHeathEventChanged(PlayerStatChangedEventArgs args, object sender)
        {
            this.healthBar.UpdateWithValue(args.NewValue);
        }

       

        private void Update()
        {
           
			Tools.ManageGameObjects ();
            Tools.PerformSuperSpeedCheck();

            if (PlayerStats.GetInstance().Health <= 0)
            {
                Debug.LogError("Game ended");
                PlayerStats.GetInstance().PlayerHeathEventChanged -= GameSceneController_PlayerHeathEventChanged;
                PlayerStats.GetInstance().PlayerShieldEventChanged -= GameSceneController_PlayerShieldEventChanged;
                SceneManager.LoadSceneAsync("Main");
            }
        }

        private void LateUpdate()
        {
            GameStats.GetInstance().GainedScore += 0.1;
        }

		private void OnDestroy()
		{
			Tools.ResetGameObjects ();
		}
    }
}

