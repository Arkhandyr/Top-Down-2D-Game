using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    public float Health 
    { 
        set 
        {
            health = value;

            if (health > 0)
            {
                Damaged();
            }
            else
            {
                Defeated();
            }
        } 
        get 
        {
            return health;
        }
    }

    public float health = 100;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Damaged()
    {
        animator.SetTrigger("Damaged");
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
