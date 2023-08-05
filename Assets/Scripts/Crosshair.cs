using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private Sprite mouseCursorSprite;
    [SerializeField] private Sprite crossHairSprite;

    [SerializeField] private WeaponSwitching wS;
    private void Start()
    {
        Cursor.visible = false;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = mouseCursorSprite;
    }
    void Update()
    {
        if (wS.selectedWeapon == 0)
        {
            sr.sprite = mouseCursorSprite;
        }
        else if (wS.selectedWeapon > 0)
        {
            sr.sprite = crossHairSprite;
        }

        //transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,5));
        Vector2 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = aim;

    }
}
