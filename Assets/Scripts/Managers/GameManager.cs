using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonDontDestroyMono<GameManager>
{
    public int money;
    public int cards;
    public int HealthPotion;
    public int MaxHealthPotion;
}
