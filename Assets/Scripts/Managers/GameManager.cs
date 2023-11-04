using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonDontDestroyMono<GameManager>
{
    public int money;
    public int cards;
    public int HealthPotion;
    public int MaxHealthPotion;

    public bool canAttack;
    public bool canMove;

    public bool isDead;

    private void Start()
    {
        canAttack = true;
        canMove = true;
        isDead = false;
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            canAttack = false;
            canMove = false;
        }
    }
}
