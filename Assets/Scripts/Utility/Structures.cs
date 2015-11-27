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

    [System.Serializable]
    public struct PlayerStats
    {
        private const int kMaxPlayerHealth = 30;
        private int health;

        public bool IsShieldEnabled { get; set; }

        public bool IsImmune { get; set; }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
                if (health > kMaxPlayerHealth)
                {
                    health = kMaxPlayerHealth;
                }

                if (health < 0)
                {
                    health = 0;
                }
            }
        }

        public bool IsVampiricEnabled { get; set; }

        internal void ResetPlayerStats()
        {
            this.Health = kMaxPlayerHealth;
            this.IsImmune = false;
        }
    }

}
