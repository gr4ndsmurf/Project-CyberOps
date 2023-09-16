using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GangstaState : MonoBehaviour
{
    public abstract GangstaState State(GangstaController controller);
}
