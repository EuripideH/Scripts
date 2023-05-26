using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D projectileRb;
    [SerializeField] float speed = 5f;

    [SerializeField] float projectileLife = 2.8f;
    [SerializeField] float projectileCount = 0f;

    private PlayerController playerController;
    public bool facingRight;

    private BossHealth bossHealth;


    
    void Start()
    {
        projectileRb = GetComponent<Rigidbody2D>();
        projectileCount = projectileLife;
        
        bossHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        facingRight = playerController.facingRight;


          if(!facingRight)
          {
              transform.rotation = Quaternion.Euler(0, 180,0);

          }
    }

    
    void Update()
    {
       projectileCount -= Time.deltaTime;
        if (projectileCount <= 0.2) 
        {
            Destroy(gameObject);
        }

    }
   public void FixedUpdate()
    {
        if(!facingRight)
        {
            projectileRb.velocity = new Vector2(-speed, projectileRb.velocity.y);
        }
        else
        {
        projectileRb.velocity = new Vector2(speed, projectileRb.velocity.y);
        }

    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Weak point")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Trap")
        {
            
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Boss")
        {
            bossHealth.TakeDamage(1);
            Destroy(gameObject);
         }


        if (collision.gameObject.tag == "Platform")
        {
        Destroy(gameObject);
         }

    }

}

