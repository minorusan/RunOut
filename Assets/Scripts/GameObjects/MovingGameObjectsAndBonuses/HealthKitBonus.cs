using UnityEngine;
using System.Collections;
using RunOut.Core.Controllers;
using RunOut.Utils;

public class HealthKitBonus : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals(Strings.kPLayerTag))
        {
            PlayerStats.GetInstance().HealthValue += Constants.kDefaultHealthBonusValue;
            this.gameObject.SetActive(false);
        }
    }
}
