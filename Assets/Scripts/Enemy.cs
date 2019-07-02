using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;
    private float startHealth = 100;
    public float health;
    public int killValue = 50;
    public GameObject deathEffect;

    // [Header("Unity Stuff")]
    // public Image healthBar;

    private bool isDead = false;

    void Start() {
        speed = startSpeed;
        health = startHealth;
    }
    
    public void TakeDamage(int damage){
        health -= damage;
        // healthBar.fillAmount = health / startHealth;
        if (health <= 0f && !isDead){
            Die();
        }
    }

    public void Slow(float amount) {
        speed = startSpeed * (1f - amount);
    }

    void Die() {
        isDead = true;
        PlayerStats.Money += killValue;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
