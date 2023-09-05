using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttack : DroneState
{
    [SerializeField] private float shootingRange = 7f;
    [SerializeField] private Transform movebackPoint;

    private bool shootingDelayed;
    public override DroneState State(DroneController controller)
    {
        if (Vector2.Distance(controller.transform.position, controller.target.position) > shootingRange)
        {
            controller.animator.SetBool("Chase", true);
            controller.animator.SetBool("Attack", false);
            controller.CancelInvoke("HandleShooting");
            return controller.chase;
        }

        //Moving Back
        if (Vector2.Distance(controller.transform.position, controller.target.position) < 1.09f)
        {
            controller.aiDestinationSetter.target = movebackPoint;
            controller.aiPath.maxSpeed = controller.speed * 2f;
        }
        else
        {
            controller.aiDestinationSetter.target = controller.target;
            controller.aiPath.maxSpeed = controller.speed * 1f;
        }

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

        //Look at target
        Vector2 direction = new Vector2(controller.target.transform.position.x - controller.transform.position.x, controller.target.transform.position.y - controller.transform.position.y);
        controller.transform.right = Vector3.Lerp(controller.transform.right, direction, controller.damping);

        //Attack

        return this;
    }
}
