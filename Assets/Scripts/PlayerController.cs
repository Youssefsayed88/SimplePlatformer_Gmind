using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    int health = 3;
    public float speed = 5f;
    private Rigidbody2D rb;
    private bool isOnGround = false;

    public LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GroundCheck();
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            Jump();
        }
        Move();
    }

    private void GroundCheck(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 5f, groundLayer);
        if(hit.distance < 0.3f && hit.distance > 0f){
            isOnGround = true;
        }else{
            isOnGround = false;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(speed * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
    }
    void OnCollisionEnter2D(Collision2D col){
        //Here we destroy the enemy but the player takes damage(unimplemented)
        if(col.gameObject.CompareTag("Enemy")){
            TakeDamage();
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        //Jump again if enemy hit
        if(col.gameObject.CompareTag("Enemy")){
            Jump();
        }
        if(col.gameObject.CompareTag("Coin")){
            GameManager.instance.checkPuzzle(col.GetComponent<CoinBehaviour>().number);
        }
    }

    void TakeDamage(){
        GameManager.instance.UpdateHp(health);
        health--;
        if(health <= 0){
            Die();
        }
    }

    void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Die(){
        GameManager.instance.Lose();
    }

    void OnBecameInvisible() {
        Die();
    }
}
