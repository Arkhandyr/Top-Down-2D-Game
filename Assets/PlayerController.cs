using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    public List<RaycastHit2D> castCollisions = new();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success) 
            {
                success = TryMove(new Vector2(movementInput.x, 0));
            }

            if (!success) 
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }

            if (success) 
            {
                if (movementInput.x != 0) 
                {
                    animator.SetInteger("movementDirection", 1);
                }
                else if (movementInput.y < 0) 
                {
                    animator.SetInteger("movementDirection", 2);
                }
                else if (movementInput.y > 0) 
                {
                    animator.SetInteger("movementDirection", 3);
                }
            }
        } 
        else 
        {
            animator.SetInteger("movementDirection", 0);
        }

        if (movementInput.x < 0) 
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0) 
        {
            spriteRenderer.flipX = false;
        }
    }

    private bool TryMove(Vector2 direction) {
        if (direction != Vector2.zero) {
            int count = rb.Cast(
                direction, 
                movementFilter, 
                castCollisions, 
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0) 
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } 
            else 
            {
                return false;
            }
        }
        else 
        {
            return false;
        }

    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire() {
        animator.SetTrigger("swordAttack");
    }


    public void SwordAttack() 
    {
        moveSpeed = 0.25f;

        if(spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else 
        {
            swordAttack.AttackRight();
        }
    }

    public void NormalSpeed() 
    {
        moveSpeed = 1f;
        swordAttack.StopAttack();
    }
}
