using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public int number = 1;

    void OnTriggerEnter2D(Collider2D col){
        //Here we destroy the enemy but the player takes damage(unimplemented)
        if(col.gameObject.CompareTag("Player")){
            Destroy(gameObject);        
        }
    }
}
