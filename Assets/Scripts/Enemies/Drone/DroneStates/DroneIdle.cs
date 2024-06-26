using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneIdle : DroneState
{
    [SerializeField] private float chaseDistance = 3.5f;
    [SerializeField] private Transform[] patrolPoints;

    private int patrolDestination;

    public override DroneState State(DroneController controller)
    {
        if (Vector2.Distance(controller.transform.position, controller.target.position) < chaseDistance)
        {
            controller.animator.SetBool("Chase", true);
            controller.transform.localScale = new Vector3(1, 1, 1);
            return controller.chase;
        }

        if (patrolDestination == 0)
        {
            controller.transform.position = Vector2.MoveTowards(controller.transform.position, patrolPoints[0].position, controller.speed * Time.deltaTime);
            if (Vector2.Distance(controller.transform.position, patrolPoints[0].position) < .2f)
            {
                patrolDestination = 1;
                Flip(controller.gameObject.transform);
            }
        }
        else if (patrolDestination == 1)
        {
            controller.transform.position = Vector2.MoveTowards(controller.transform.position, patrolPoints[1].position, controller.speed * Time.deltaTime);
            if (Vector2.Distance(controller.transform.position, patrolPoints[1].position) < .2f)
            {
                Flip(controller.gameObject.transform);
                patrolDestination = 0;
            }
        }

        controller.transform.rotation = Quaternion.Euler(0, 0, 0);

        return this;
    }

    public void Flip(Transform controller)
    {
        Vector3 localScale = controller.transform.localScale;
        localScale.x *= -1;
        controller.transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(patrolPoints[0].transform.position, 0.5f);
        Gizmos.DrawWireSphere(patrolPoints[1].transform.position, 0.5f);
        Gizmos.DrawLine(patrolPoints[0].transform.position, patrolPoints[1].transform.position);
    }
}
