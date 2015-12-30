using RunOut.Core.GameObjects;
using System;
using System.Timers;

namespace RunOut.Utils
{
    [System.Serializable]
    public struct Boundary
    {
        public float xMax, yMax, xMin, yMin, zMin, zMax;
    }

    public delegate void PlayerHealthChangedHadler(PlayerStatChangedEventArgs args, object sender);

	public class MusicFromDeviceDataModel
	{
		public string SongName {get; set;}
		public string FullPath {get; set;}
	}

    public class PlayerStatChangedEventArgs : EventArgs
    {
        public int OldValue { get; set; }
        public int NewValue { get; set; }
    }

    public class PlayerStats
    {
        #region Private
        private static PlayerStats instance;
        private int shieldValue;
        private int health;
        #endregion

        #region Events
        public event PlayerHealthChangedHadler PlayerHeathEventChanged;
        public event PlayerHealthChangedHadler PlayerShieldEventChanged; 
        #endregion

        PlayerStats()
        {

        }

        public static PlayerStats GetInstance()
        {
            return instance == null ? instance = new PlayerStats() : instance;
        }

        public int HealthValue
        {
            get
            {
                return this.health;
            }

            set
            {
                if (this.PlayerHeathEventChanged != null)
                {
                    this.PlayerHeathEventChanged(new PlayerStatChangedEventArgs { OldValue = health, NewValue = value }, null);
                }
                var oldValue = health;
                this.health = value;

                if (this.health > Constants.kMaxPlayerResilence)
                {
                    this.health = Constants.kMaxPlayerResilence;
                }

                if (this.health < 0)
                {
                    this.health = 0;
                }
                if (this.PlayerHeathEventChanged != null)
                {
                    this.PlayerHeathEventChanged(new PlayerStatChangedEventArgs { OldValue = oldValue, NewValue = this.health }, null);
                }

            }
        }

        public int ShieldValue
        {
            get
            {
                return shieldValue;
            }

            set
            {
                if (this.PlayerShieldEventChanged != null)
                {
                    this.PlayerShieldEventChanged(new PlayerStatChangedEventArgs { OldValue = shieldValue, NewValue = value }, null);
                }

                shieldValue = value;
                if (shieldValue <= 0)
                {
                    shieldValue = 0;
                }
            }
        }

        internal void ResetPlayerStats()
        {
            this.HealthValue = Constants.kMaxPlayerResilence;
        }
    }


    [Serializable]
    public class GameStats
    {
        private static GameStats instance;
        private double gainedScore;
        private Timer timer;

        public bool IsSuperSpeedEnabled
        {
            get; private set;
        }

        private GameStats()
        {
            this.ScoreMultiplier = Constants.kDefaultScoreMultiplier;
        }
        
        public void BeginSuperSpeedEffect(double superSpeedTimer = Constants.kDefaultSuperSpeedTime)
        {
            this.timer = new Timer();
            this.timer.Interval = superSpeedTimer;
            this.timer.Elapsed += OnSuperSpeedTimerElapsed;
            MovingGameObject.speedModifier = Constants.kSuperSpeedConstant;
            this.IsSuperSpeedEnabled = true;
            this.timer.Start();
        }

        private void OnSuperSpeedTimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.timer.Stop();
            MovingGameObject.speedModifier = Constants.kDefaultSpeedModifier;
            this.IsSuperSpeedEnabled = false;
        }

        /// <summary>
        /// Gained score point is multiplied by that value on every score gaining tick.
        /// </summary>
        public double ScoreMultiplier { get; set; }

        /// <summary>
        /// Use ModifyScoreByValue to set or change
        /// </summary>
        public double GainedScore
        {
            get
            {
                return this.gainedScore;
            }
        }

        public void ModifyScoreByValue(double value)
        {
            value *= this.ScoreMultiplier;
            this.gainedScore += value;
        }

        public static GameStats GetInstance()
        {
            return instance == null ? instance = new GameStats() : instance;
        }
    }
}
