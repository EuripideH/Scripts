using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour

{
   // PlayerController controls;
    
    [SerializeField] ItemCollector red;
    [SerializeField] Text cherriesText;

    [SerializeField] private AudioSource castingEffect;

    public GameObject projectilePrefab;
    public Transform launchPoint;

    public float shootTime;
    public float shootCounter;
    



    private SpriteRenderer sprite;
    private Animator anim;
    private enum MouvementState { idle, running, jumping, falling, attack }
    [SerializeField] private AudioSource jumpSoundEffect;
    private BoxCollider2D coll;

    // private float dirX = 0f;


    [Header("Player Component References")]
    [SerializeField] Rigidbody2D rb;

    [Header("Player Settings")]
    [SerializeField] float speed;
    [SerializeField] float jumpingPower;

    [Header("Grounding")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    public float horizontal = 0f;

    //new try
    
    InputAction _move;
    Vector2 _moveDirection;

    public bool flippedLeft;
    public bool facingRight;


    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        red = GetComponent<ItemCollector>();
    }

     private void FixedUpdate()
    {
        
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        UpdateAnimationState();
        if (shootCounter >= 0)
        {

        shootCounter -= Time.deltaTime;

        }
}
      


    

    #region PLAYER_CONTROLS 



    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);


        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        


        if (context.performed && shootCounter <= 0 && red.cherries >=2)
        {
            
            castingEffect.Play();
            Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
            
            shootCounter = shootTime;
            anim.SetTrigger("shoot");
            red.cherries--;
            red.cherries--;
            red.cherriesText.text = "X " + red.cherries;
        }


    }

    private void UpdateAnimationState()
    {
        MouvementState state;

        if (horizontal > 0f)
        {
            facingRight = true;
            Flip(true);
            state = MouvementState.running;
            
            

        }
        else if (horizontal < 0f)
        {
            facingRight = false;
            Flip(false);
            state = MouvementState.running;
            
            
        }

        else
        {
            state = MouvementState.idle;

        }

        if (rb.velocity.y > .1f)
        {
            state = MouvementState.jumping;

        }
        else if (rb.velocity
            .y < -.1f)
        {
            state = MouvementState.falling;
        }

        if (shootCounter >= 1)

        {
            state = MouvementState.attack;
        }


        anim.SetInteger("state", (int)state);
    }

    void Flip (bool facingRight)
    {
        if(flippedLeft && facingRight)

        {
            transform.Rotate(0, -180, 0);
            flippedLeft = false;
        }
        if(!flippedLeft && !facingRight)
        {
            transform.Rotate(0, -180, 0);
            flippedLeft=true;
        }
    }
    

    


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        


    }
    #endregion
}
