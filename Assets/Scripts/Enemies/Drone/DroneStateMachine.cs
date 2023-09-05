using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneStateMachine : MonoBehaviour
{
    public DroneState currentState;
    public DroneController controller;

    private void Start()
    {
        controller = GetComponent<DroneController>();
    }

    private void Update()
    {
        DroneState state = currentState.State(controller);
        if (currentState != null)
        {
            currentState = state;
        }
    }
}
