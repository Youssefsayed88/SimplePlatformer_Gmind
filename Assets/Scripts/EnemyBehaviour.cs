using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Enemy[] enemyType;

    Enemy enemy;

    Rigidbody2D rb;

    public GameObject coin;

    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        enemy = RandomizeEnemy();
        speed = enemy.speed;
        gameObject.GetComponent<SpriteRenderer>().color = enemy.color;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    Enemy RandomizeEnemy(){
        return enemyType[Random.Range(0, enemyType.Length)];
    }

    void DropCoin()
    {
        GameObject coinSpawn = Instantiate(coin, transform.position, Quaternion.identity);
        coinSpawn.GetComponent<SpriteRenderer>().color = enemy.color;
        coinSpawn.GetComponent<CoinBehaviour>().number = enemy.number;
    }
  
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Wall")){
            Debug.Log("done");
            speed *= -1;
        }
        //Here we destroy the enemy but the player takes damage(unimplemented)
        if(col.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        //Here we destroy the enemy without the player taking damage
        if(col.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }

    void OnDestroy(){
        if(!this.gameObject.scene.isLoaded) return;
        DropCoin();
    }

    

}
