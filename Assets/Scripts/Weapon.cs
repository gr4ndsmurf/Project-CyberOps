using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject armPoint;
    void Update()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        armPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // Silahý mirrorlama
        Vector3 aimLocalScale = transform.localScale;
        Vector3 armPointLocalScale = armPoint.transform.localScale;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.x = -1f;
            armPointLocalScale.x = -1f;
            armPointLocalScale.y = -1f;
            transform.localPosition = new Vector3(0.05f, transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            aimLocalScale.x = +1f;
            armPointLocalScale.x = +1f;
            armPointLocalScale.y = +1f;
            transform.localPosition = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);
        }
        transform.localScale = aimLocalScale;
        armPoint.transform.localScale = armPointLocalScale;
    }


}
