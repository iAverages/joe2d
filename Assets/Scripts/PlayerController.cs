using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.01f;
    public ContactFilter2D movementFilter;
    
    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();

    public AudioSource[] collisionSounds;

    public AudioSource collision1;
    public AudioSource collision2;
    public AudioSource collision3;
    public AudioSource collision4;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        collisionSounds = GetComponents<AudioSource>();
        collision1 = collisionSounds[0];
        collision2 = collisionSounds[1];
        collision3 = collisionSounds[2];
        collision4 = collisionSounds[3];
    }

    // Update is called once per frame
    void Update() 
    {
        if (Input.GetKeyDown("f"))
        {     
            PlayRandomSound();
        }
    }

    private void FixedUpdate() {
        bool moved = false;

        // If movement input is not 0, dont move.
        if (movementInput != Vector2.zero) {
            // Try move the player in the direction they want.
            moved = TryMove(movementInput);
            // If they are not able to move, test x and y separately. 
            // This allows for a player to move up a wall if they are
            // right up against it.
            if (!moved) {
                // Try for X
                moved = TryMove(new Vector2(movementInput.x, 0));
                if (!moved) {
                    // Try for Y
                    moved = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }

        animator.SetBool("IsMoving", moved);
        spriteRenderer.flipX = movementInput.x > 0;
    }


    private bool TryMove(Vector2 direction) {
        if (direction == Vector2.zero) return false;
        
        int count = rb.Cast(
                direction,
                movementFilter,
                castCollision,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
        );

        if (count == 0) {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } 
        return false;
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    public void PlayRandomSound()
        { 
            collisionSounds[Random.Range(0, collisionSounds.Length)].Play();
        }
}




    
   
        

            
