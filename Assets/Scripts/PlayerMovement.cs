using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject parallaxBackground;
    [SerializeField] float parallaxValue = 0.1f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 20f);
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;

    Vector3 startingPoint;


    [SerializeField] GameObject fish;
    [SerializeField] Transform gun;

    bool isAlive = true;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        startingPoint = transform.position;

    }

    void Update()
    {
        if (!isAlive) return;

        Run();
        FlipSprite();
        ClimbLadder();
        MoveBackground();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;
        Instantiate(fish, gun.position, transform.rotation);
    }


    void OnMove(InputValue value)
    {
        if (!isAlive) return;

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;

        bool isTouchingGroundOrClimbing = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing"));
        if (!isTouchingGroundOrClimbing) return;
        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);

        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0;
        myAnimator.SetBool("isClimbing", Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon);


    }

    void MoveBackground()
    {
        var playerOffset = transform.position;
        parallaxBackground.transform.position = new Vector2(playerOffset.x * parallaxValue, playerOffset.y * parallaxValue / 3);
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetBool("isDead", true);
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            // StartCoroutine(Restart());
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1f);
        isAlive = true;
        myAnimator.SetBool("isDead", false);
        moveInput = new Vector2(0, 0);
        myRigidbody.velocity = new Vector2(0, 0);
        myRigidbody.transform.position = startingPoint;
    }

}



