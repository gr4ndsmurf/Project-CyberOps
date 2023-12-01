using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangstaChase : GangstaState
{
    [SerializeField] private float attackDistance = 5f;

    public override GangstaState State(GangstaController controller)
    {
        if (Vector2.Distance(controller.transform.position, controller.target.position) < attackDistance)
        {
            controller.InvokeRepeating("HandleShooting", controller.shootingTime, controller.shootingDelay);
            return controller.attack;
        }

        //Chase Codes
        if (controller.TargetInDistance() && controller.followEnabled)
        {
            controller.PathFollow();
        }

        controller.CheckVelocity(controller);

        return this;
    }
}
