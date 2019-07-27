using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public float rate = 1f;
    public Wave[] waves;
    public float timeBetweenWaves = 5f;
    public Text waveCountdownText;
    private float countDown = 10f;
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
        // If any enemies alive, return
        if (EnemiesAlive > 0) {
            waveUIAnimator.SetBool("isActive", false);
            return;
        }
        // If countdown @ 0, SpawnWave, return
        if (countDown <= 0f) {
            waveUIAnimator.SetBool("isActive", false);
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }

        // If waveNumber = # of waves, win level (Should remove this)
        // if (waveNumber == waves.Length) {
        //     Debug.Log("Level Complete.");
        //     waveUIAnimator.SetBool("isActive", false);
        //     gameManager.EndGame();
        //     this.enabled = false;
        //     return;
        // } 

        waveUIAnimator.SetBool("isActive", true);
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countDown);
    }

    IEnumerator SpawnWave() {
        // Get the wave
        int point = Random.Range(0, waves.Length);
        Wave wave = waves[point];

        PlayerStats.Rounds++;
        waveNumber++;

        // Set Enemies alive to the number of enemies in the wave
        EnemiesAlive = waveNumber * wave.countMultiplier;
        // Spawn all enemies
        for (int i = 0; i < waveNumber * wave.countMultiplier; i++)
        {
            yield return new WaitForSeconds(rate);
            SpawnEnemy(wave.enemy);
        }
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
