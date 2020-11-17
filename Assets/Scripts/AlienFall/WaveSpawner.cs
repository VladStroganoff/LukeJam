using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public Transform[] spawnAreas;
    private Vector3 spawnAreaCenter;
    private Vector3 spawnAreaSize;
    private Vector3 spawnRotation;
    public float spawnRotationVariance = 150f;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        if (spawnAreas.Length == 0)
        {
            Debug.LogError("No spawnAreas referenced.");
        }

        if (waves.Length == 0)
        {
            Debug.LogError("No Enemy Waves defined.");
        }

        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        switch (state)
        {
            case SpawnState.COUNTING:
                if (waveCountdown <= 0)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                    state = SpawnState.SPAWNING;
                }
                else
                {
                    waveCountdown -= Time.deltaTime;
                }
                break;
            case SpawnState.WAITING:
                if (!EnemyIsAlive())
                {
                    WaveCompleted();
                }
                else
                {
                    Debug.Log("Enemy is alive!");
                }
                break;
        }  
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        waveCountdown = timeBetweenWaves;

        state = SpawnState.COUNTING;

        if (nextWave + 1 > waves.Length - 1)
        {
            //Win();
            
            Debug.Log("All Waves Completed - Looping to first wave");
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        //Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Debug.Log("Spawning Enemy: " + _enemy.name);
        Transform _sp = spawnAreas[Random.Range(0, spawnAreas.Length)];

        spawnAreaCenter = _sp.transform.position;
        spawnAreaSize = _sp.transform.localScale;
        Vector3 pos = spawnAreaCenter + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2), Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2));
        
        float spawnRotationY = _sp.rotation.y + Random.Range(-spawnRotationVariance, spawnRotationVariance);
        Quaternion rot = Quaternion.Euler(0, spawnRotationY, 0);

        Instantiate(_enemy, pos, rot);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawCube(spawnAreaCenter, spawnAreaSize);
    }

}
