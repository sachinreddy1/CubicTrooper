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

    public GameObject spawnEffect;

    private BuildManager buildManager;


    // [Header("Unity Stuff")]
    // public Image healthBar;

    private bool isDead = false;

    void Start() {
        speed = startSpeed;
        health = startHealth;

        buildManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BuildManager>();

        GameObject effectInstance = (GameObject)Instantiate(spawnEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);
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
        buildManager.CollectMoney(killValue);

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
