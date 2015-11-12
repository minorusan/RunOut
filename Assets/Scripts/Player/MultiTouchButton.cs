using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class MultiTouchButton : MonoBehaviour
{
    public Camera Owner;

    public Sprite Default;
    public Sprite Pressed;

    public UnityEvent OnDown;
    public UnityEvent OnHold;
    public UnityEvent OnUp;


    public Image Img;

    public BoxCollider2D BCol;

	// Use this for initialization
	void Start ()
    {
        BCol = GetComponent<BoxCollider2D>();
	}

    bool _pressed = false;
	// Update is called once per frame
	void Update ()
    {
        bool isTouched = false;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            { 
                isTouched = TouchTest(Owner.ScreenToWorldPoint(Input.GetTouch(i).position)); 

                if (isTouched && !_pressed && (Input.GetTouch(i).phase == TouchPhase.Began))
                {
                    OnTouchEnter();
                }

                if (isTouched) break;
            }
        }

        if (!isTouched && _pressed)
        {
            OnTouchLeft();
        }
        else if (isTouched && _pressed)
        {
            OnTouchHold();
        }

    }

    bool TouchTest(Vector3 wp)
    {
        Vector2 touchPos = new Vector2(wp.x, wp.y);

        return (BCol == Physics2D.OverlapPoint(touchPos));
    }

    void OnTouchLeft()
    {
        _pressed = false;
        Img.sprite = Default;

        OnUp.Invoke();
    }

    void OnTouchHold()
    {
        _pressed = true;
        OnHold.Invoke();
    }


    void OnTouchEnter()
    {
        _pressed = true;
        Img.sprite = Pressed;
        OnDown.Invoke();
    }
}
