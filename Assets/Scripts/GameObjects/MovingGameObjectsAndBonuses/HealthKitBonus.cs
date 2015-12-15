using UnityEngine;
using System.Collections;
using RunOut.Core.Controllers;
using RunOut.Utils;

public class HealthKitBonus : MonoBehaviour {

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals(Strings.kPLayerTag))
        {
            PlayerStats.GetInstance().Health++;
            this.gameObject.SetActive(false);
        }
    }
}
