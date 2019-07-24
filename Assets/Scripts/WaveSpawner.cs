﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    public float timeBetweenWaves = 5f;
    public Text waveCountdownText;
    private float countDown = 2f;
    private int waveNumber = 0;
    //
    public GameObject spawns;
    public static Transform[] spawnPoints;
    //
    public GameManager gameManager;
    public Animator waveUIAnimator;

    void Awake() 
    {
        spawnPoints = new Transform[spawns.transform.childCount];
        for (int i = 0; i < spawnPoints.Length; i++) 
        {
            spawnPoints[i] = spawns.transform.GetChild(i);
        }
    }

    void Update()
    {
        if (EnemiesAlive > 0) {
            waveUIAnimator.SetBool("isActive", false);
            return;
        }

        if (waveNumber == waves.Length) {
            Debug.Log("Level Complete.");
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countDown <= 0f) {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }

        waveUIAnimator.SetBool("isActive", true);

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countDown);
    }

    IEnumerator SpawnWave() {
        PlayerStats.Rounds++;
        Wave wave = waves[waveNumber];
        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/ wave.rate);
        }
        waveNumber++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        int point = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[point].position, spawnPoints[point].rotation);
    }

    public void SkipWaveTime() {
        countDown = 0f;
        waveCountdownText.text = string.Format("{0:00.00}", countDown);
    }

}
