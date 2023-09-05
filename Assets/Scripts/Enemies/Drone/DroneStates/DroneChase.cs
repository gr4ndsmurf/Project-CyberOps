using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneChase : DroneState
{
    [SerializeField] private float attackDistance = 5f;

    public override DroneState State(DroneController controller)
    {
        if (Vector2.Distance(controller.transform.position, controller.target.position) < attackDistance)
        {
            controller.animator.SetBool("Attack", true);
            controller.InvokeRepeating("HandleShooting", controller.shootingTime, controller.shootingDelay);
            return controller.attack;
        }

        //Look at target
        Vector2 direction = new Vector2(controller.target.transform.position.x - controller.transform.position.x, controller.target.transform.position.y - controller.transform.position.y);
        controller.transform.right = Vector3.Lerp(controller.transform.right, direction, controller.damping);

        //Flip
        Vector3 controllerLocalScale = controller.transform.localScale;
        if (controller.target.transform.position.x <= controller.transform.position.x)
        {
            controllerLocalScale.y = -1f;
        }
        if (controller.target.transform.position.x > controller.transform.position.x)
        {
            controllerLocalScale.y = +1f;
        }
        controller.transform.localScale = controllerLocalScale;

        //Chase

        return this;
    }
}
