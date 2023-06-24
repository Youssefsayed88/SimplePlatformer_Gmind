using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    float timer;
    public int number = 1;

    void Awake(){
        GetComponent<CircleCollider2D>().enabled = false;
    }
    
    void Update(){
        timer += Time.deltaTime;
        if(timer >= 1f){
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        //Here we destroy the enemy but the player takes damage(unimplemented)
        if(col.gameObject.CompareTag("Player")){
            Destroy(gameObject);        
        }
    }
    
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
