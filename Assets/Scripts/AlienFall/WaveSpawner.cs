using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING, GAMEOVER };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] Waves;
    private int _nextWave = 0;
    public Transform[] SpawnAreas;
    private Vector3 _spawnAreaCenter;
    private Vector3 _spawnAreaSize;
    public float SpawnRotationVariance = 150f;

    public float TimeBetweenWaves = 5f;
    private float _waveCountdown;

    private float _searchCountDown = 1f;

    private SpawnState _state = SpawnState.COUNTING;

    private void Start()
    {
        if (SpawnAreas.Length == 0)
            Debug.LogError("No spawnAreas referenced.");

        if (Waves.Length == 0)
            Debug.LogError("No Enemy Waves defined.");

        _waveCountdown = TimeBetweenWaves;
    }

    private void Update()
    {
        switch (_state)
        {
            case SpawnState.COUNTING:
                if (_waveCountdown > 0)
                    _waveCountdown -= Time.deltaTime;
                else
                {
                    StartCoroutine(SpawnWave(Waves[_nextWave]));
                    _state = SpawnState.SPAWNING;
                }
                break;

            case SpawnState.WAITING:
                if (!EnemyIsAlive())
                    WaveCompleted();
                else
                    Debug.Log("Enemy is alive!");
                break;
        }  
    }

    private void WaveCompleted()
    {
        _waveCountdown = TimeBetweenWaves;

        _state = SpawnState.COUNTING;

        if (_nextWave + 1 <= Waves.Length - 1)
            _nextWave++;
        else
            Win();
    }


    private bool EnemyIsAlive()
    {
        _searchCountDown -= Time.deltaTime;

        if (_searchCountDown > 0f)
            return true;

        _searchCountDown = 1f;

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
            return false;

        return true;
    }

    private IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        _state = SpawnState.SPAWNING;

        for (var i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        _state = SpawnState.WAITING;

        yield break;
    }

    private void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = SpawnAreas[Random.Range(0, SpawnAreas.Length)];

        _spawnAreaCenter = _sp.transform.position;
        _spawnAreaSize = _sp.transform.localScale;
        Vector3 pos = _spawnAreaCenter + 
            new Vector3
            (
                x: Random.Range(-_spawnAreaSize.x / 2, _spawnAreaSize.x / 2),
                y: Random.Range(-_spawnAreaSize.y / 2, _spawnAreaSize.y / 2),
                z: Random.Range(-_spawnAreaSize.z / 2, _spawnAreaSize.z / 2)
            );
        
        var spawnRotationY = _sp.rotation.y + Random.Range(-SpawnRotationVariance, SpawnRotationVariance);
        var rot = Quaternion.Euler(0, spawnRotationY, 0);

        Instantiate(_enemy, pos, rot);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawCube(_spawnAreaCenter, _spawnAreaSize);
    }

    private void Win()
    {
        _state = SpawnState.GAMEOVER;
        throw new System.NotImplementedException();
    }
}
