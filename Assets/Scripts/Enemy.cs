using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    
    public void TakeDamage(int damage){
        health -= damage;
        if (health <= 0f){
            Die();
        }
    }

    void Die() {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }
}
