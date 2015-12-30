using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RunOut.Utils
{
    class Constants
    {
        #region CameraController
        /// <summary>
        /// Defines how fast camera interpolates from pos to pos.
        /// </summary>
        internal const int kCameraRotationSpeedModifier = 4;
        internal static readonly Vector3 kCameraBackRotationAngle = new Vector3(0, 90, 0);
        internal static readonly Vector3 kCameraTopRotationAngle = new Vector3(90, 0, 0);
        internal static readonly Vector3 kCameraSideRotationAngle = Vector3.zero;

        [Obsolete]
        internal static readonly Vector3 kCameraDistanceFromPlayer = new Vector3(20, 20, 20);
        #endregion

        #region EnemyAI
        /// <summary>
        /// How fast enemy follows and reacts to player's actions
        /// </summary>
        internal const float kDefaultEnemyTrackingSpeed = 0.1f;

        /// <summary>
        /// How long enemy exists by default, if value was not set explicitly
        /// </summary>
        internal const float kDefaultActivityTime = 6f;
        #endregion

        #region EnemyProjectile
        internal const float kBeginScreenChekingFromThisPoint = -2f;
        internal const float kDefaultSpeedModifier = 0f;
        internal const int kDefaultEnemyProjectileDamage = 1;
        #endregion

        #region Bonuses and moving GO
        internal const int kDefaultHealthBonusValue = 50;
        internal const int kDefaultMeteorDamage = 50;
        
        /// <summary>
        /// Determines by which value MovingGameObjects move faster, when super speed is active
        /// </summary>
        internal const float kSuperSpeedConstant = 1f;

        /// <summary>
        /// Max health and shield value
        /// </summary>
        internal const int kMaxPlayerResilence = 300;
        #endregion

        #region Player movement and game stats
        /// <summary>
        /// How fast player reacts to joystic movement
        /// </summary>
        internal const float kPlayerVelocityChangeModifiyer = 2f;


        internal const double kDefaultScoreGain = 0.1;
        internal const double kDefaultScoreMultiplier = 1;

        /// <summary>
        /// Default super speed effect time in seconds
        /// </summary>
        internal const double kDefaultSuperSpeedTime = 6;
        #endregion



    }
}
