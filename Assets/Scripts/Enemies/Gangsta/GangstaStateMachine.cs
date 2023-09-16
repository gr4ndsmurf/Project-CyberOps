using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangstaStateMachine : MonoBehaviour
{
    public GangstaState currentState;
    public GangstaController controller;

    private void Start()
    {
        controller = GetComponent<GangstaController>();
    }

    private void Update()
    {
        GangstaState state = currentState.State(controller);
        if (currentState != null)
        {
            currentState = state;
        }
    }
}
