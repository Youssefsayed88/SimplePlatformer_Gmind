using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Enemy[] enemyType;

    Enemy enemy;

    Rigidbody2D rb;

    public GameObject coin;

    int coinToss;

    [SerializeField]
    float speed;
    // Start is called before the first frame update

    void Awake(){
        coinToss = Random.Range(0,2);
    }

    void Start()
    {
        enemy = RandomizeEnemy();
        speed = enemy.speed;
        if (coinToss == 0){
            speed *= -1;
        }
        gameObject.GetComponent<SpriteRenderer>().color = enemy.color;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
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
            speed *= -1;
        }
        //Here we destroy the enemy but the player takes damage
        if(col.gameObject.CompareTag("Player")){
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        //Here we destroy the enemy without the player taking damage
        if(col.gameObject.CompareTag("Player")){
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void OnDestroy(){
        if(!this.gameObject.scene.isLoaded) return;
        DropCoin();
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
