using UnityEngine;
using System.Collections;
using RunOut.Core.Controllers;
using RunOut.Utils;

public class HealthKitBonus : MonoBehaviour
{
    private const int kDefaultHealthBonusValue = 50;

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals(Strings.kPLayerTag))
        {
            PlayerStats.GetInstance().Health += kDefaultHealthBonusValue;
            this.gameObject.SetActive(false);
        }
    }
}
