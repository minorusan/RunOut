using RunOut.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RunOut.Core.GameObjects.Bonuses
{
    class SuperSpeedBonus:MonoBehaviour
    {
        private const float kSuperSpeedConstant = 40f;
        public static float superSpeedTimer;

        private void OnCollisionEnter(Collision target)
        {
            if (target.gameObject.tag.Equals(Strings.kPLayerTag) == true)
            {
                MovingGameObject.speedModifier = kSuperSpeedConstant;
                superSpeedTimer = 10f;
                Debug.Log("Turned on super speed");
                this.gameObject.SetActive(false);
            }
        }

       
        
    }
}
