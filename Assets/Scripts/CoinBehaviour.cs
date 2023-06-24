using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    float timer;
    public int number = 1;

    //making the coin uncollectable for the first second
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
        //Here we destroy the coin if it collides with the player
        if(col.gameObject.CompareTag("Player")){
            Destroy(gameObject);        
        }
    }

    //destroy if it gets outside of camera viewspace
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
