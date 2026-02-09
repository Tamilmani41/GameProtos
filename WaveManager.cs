using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This script manages waves of enemies, spawning new waves when all enemies are defeated.
public class WaveManager : MonoBehaviour
{
    public EnemySpawner spawner;

    public int currentWave = 0;
    public int baseEnemyCount = 2;
    public float timeBetweenWaves = 2f;

    private List<GameObject> aliveEnemies = new List<GameObject>();
    private bool spawningWave = false;

    public WaveUI waveUI;

    private bool waitingForNextWave = false;

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        currentWave++;
        if(waveUI != null)
            waveUI.ShowWave(currentWave);

        spawningWave = true;

        int enemyCount = baseEnemyCount + currentWave;

        StartCoroutine(SpawnWave(enemyCount));
    }

    private System.Collections.IEnumerator SpawnWave(int count)
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        aliveEnemies.Clear();

        for (int i = 0; i < count; i++)
        {
            GameObject enemy = spawner.SpawnEnemy(currentWave);
            aliveEnemies.Add(enemy);
        }

        spawningWave = false;
    }

    private void Update()
    {
        if (spawningWave || waitingForNextWave)
            return;

        aliveEnemies.RemoveAll(e => e == null || !e.activeInHierarchy);

        if (aliveEnemies.Count == 0)
        {
            waitingForNextWave = true;
            StartCoroutine(NextWaveDelay());
        }
    }
    private IEnumerator NextWaveDelay()
    {
        yield return new WaitForSeconds(2f); // small buffer
        waitingForNextWave = false;
        StartNextWave();
    }


}
