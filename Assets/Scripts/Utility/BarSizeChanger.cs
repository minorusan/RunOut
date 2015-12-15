using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RunOut.Core.Controllers;

public class BarSizeChanger : MonoBehaviour
{
    private Image BarImage;

    public float initialValueValue;

    private void Start()
    {
        this.BarImage = this.GetComponent<Image>();
    }
	
    public void UpdateWithValue(float newValue)
    {
        var length = newValue/initialValueValue;
        if (this.BarImage)
        {
            this.BarImage.transform.localScale = new Vector3(length, this.BarImage.transform.localScale.y);
        }
       
    }
}
