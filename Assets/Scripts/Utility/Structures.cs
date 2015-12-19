using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunOut.Utils
{
    [System.Serializable]
    public struct Boundary
    {
        public float xMax, yMax, xMin, yMin, zMin, zMax;
    }

    public delegate void PlayerHealthChangedHadler(PlayerStatChangedEventArgs args, object sender);

    public class PlayerStatChangedEventArgs : EventArgs
    {
        public int OldValue { get; set; }
        public int NewValue { get; set; }
    }

    public class PlayerStats
    {
        private static PlayerStats instance;
        private const int kMaxPlayerHealth = 300;

        private int shieldValue;
        private int health;

        public event PlayerHealthChangedHadler PlayerHeathEventChanged;
        public event PlayerHealthChangedHadler PlayerShieldEventChanged;


        public bool IsShieldEnabled
        {
            get
            {
                return this.shieldValue > 0;
            }
            set
            {
                if (value == true)
                {
                    this.shieldValue = kMaxPlayerHealth;
                }
                else
                {
                    this.shieldValue = 0;
                }
            }
        }

        public bool IsImmune { get; set; }

        PlayerStats()
        {
           
        }

       

        public static PlayerStats GetInstance()
        {
            return instance == null ? instance = new PlayerStats() : instance;
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                if (this.PlayerHeathEventChanged != null)
                {
                    PlayerHeathEventChanged(new PlayerStatChangedEventArgs { OldValue = health, NewValue = value }, null);
                }
                var oldValue = health;
                health = value;
               
                if (health > kMaxPlayerHealth)
                {
                    health = kMaxPlayerHealth;
                }

                if (health < 0)
                {
                    health = 0;
                }
                if (this.PlayerHeathEventChanged != null)
                {
                    PlayerHeathEventChanged(new PlayerStatChangedEventArgs { OldValue = oldValue, NewValue = health }, null);
                }

            }
        }

        public bool IsVampiricEnabled { get; set; }

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
            this.Health = kMaxPlayerHealth;
            this.IsImmune = false;
        }
    }

    public class GameStats
    {
        private static GameStats instance;

        public double GainedScore { get; set; }

        private GameStats()
        {

        }

        public static GameStats GetInstance()
        {
            return instance == null ? instance = new GameStats() : instance;
        }


    }


}
