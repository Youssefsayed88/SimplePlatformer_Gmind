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

    void Awake(){
        health = 3;
    }

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

    //check if on ground to disallow the player to change midair
    private void GroundCheck(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 5f, groundLayer);
        if(hit.distance < 0.3f && hit.distance > 0f){
            isOnGround = true;
        }else{
            isOnGround = false;
        }
    }

    //basic player movement
    private void Move()
    {
        rb.velocity = new Vector2(speed * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
    }
    
    //if collided with enemy take damage
    void OnCollisionEnter2D(Collision2D col){
        //Here we destroy the enemy but the player takes damage(unimplemented)
        if(col.gameObject.CompareTag("Enemy")){
            TakeDamage();
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        //if landed on enemy head jump
        if(col.gameObject.CompareTag("Enemy")){
            Jump();
        }
        //check if this coin match the one needed for the puzzle
        if(col.gameObject.CompareTag("Coin")){
            GameManager.instance.checkPuzzle(col.GetComponent<CoinBehaviour>().number);
        }
    }

    //take damage
    void TakeDamage(){
        GameManager.instance.UpdateHp(health);
        health--;
        if(health <= 0){
            Die();
        }
    }

    //jump
    void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    //call lose function if player has 0 health
    void Die(){
        GameManager.instance.Lose();
    }

    //lose if outside screen
    void OnBecameInvisible() {
        if(GameManager.instance != null){
            Die();
        }
    }
}
