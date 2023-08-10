using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private Sprite defaultCursorSprite;
    [SerializeField] private Sprite onHoverCursorSprite;
    [SerializeField] private Sprite crossHairSprite;

    [SerializeField] private WeaponSwitching wS;

    public bool mouseOnObject;

    private void Awake()
    {
        Cursor.visible = false;
    }
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
        sr.sprite = defaultCursorSprite;
    }
    void Update()
    {
        if (wS.selectedWeapon == 0 && !mouseOnObject)
        {
            sr.sprite = defaultCursorSprite;
            tr.emitting = true;
        }
        else if (wS.selectedWeapon == 0 && mouseOnObject)
        {
            sr.sprite = onHoverCursorSprite;
            tr.emitting = true;
        }
        else if (wS.selectedWeapon > 0)
        {
            sr.sprite = crossHairSprite;
            tr.emitting = false;
        }

        //transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,5));
        Vector2 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = aim;

    }
}
