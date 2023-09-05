using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DroneState : MonoBehaviour
{
    public abstract DroneState State(DroneController controller);
}
