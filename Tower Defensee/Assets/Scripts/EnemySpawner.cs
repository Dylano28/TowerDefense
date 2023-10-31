using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int EnemySpawn = 4;
    [SerializeField] private float enemiesPerSecond = 1f;
    [SerializeField] private float timeBetweenWaves = 100f;
    [SerializeField] private float difficultyScallingFactor = 0.75f;
    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    // Start is called before the first frame update

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond)&& enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        if (enemiesAlive == 0 && enemiesLeftToSpawn== 0)
        {
            EndWave();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Time.timeScale = 3;
        }        
        if (Input.GetKeyDown(KeyCode.J  ))
        {
            Time.timeScale = 1;
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, Levelmanager.main.Startingpoint.position, Quaternion.identity);
        Debug.Log("Spawn Enemy");
    }
       private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();

    }
    private void EndWave()
    {
        isSpawning =false;
        timeSinceLastSpawn = 0f;
        StartCoroutine(StartWave());
    }
   

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(EnemySpawn * Mathf.Pow(currentWave, 0f));
    }


    // Update is called once per frame
 
}
