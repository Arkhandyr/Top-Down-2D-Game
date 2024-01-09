using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float damage = 20;
    Vector2 rightAttackOffset;
    private bool enemyInRange = false;

    private void Start()
    {
        rightAttackOffset = transform.position;
    }

    public void AttackRight() 
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft() 
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack() 
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Enemy" && !enemyInRange) 
        {
            Enemy enemy = target.GetComponent<Enemy>();

            if (enemy != null) {
                enemy.Health -= damage;
                print(enemy.Health);
            }

            enemyInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Enemy")
        {
            enemyInRange = false;
        }
    }
}
