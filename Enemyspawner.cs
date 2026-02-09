using UnityEngine;
// This script spawns enemies at random spawn points and applies wave-based scaling to their stats.
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public GameObject SpawnEnemy(int waveNumber)
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);

        EnemyStats stats = enemy.GetComponent<EnemyStats>();
        stats.ApplyWaveScaling(waveNumber);

        return enemy;
    }
}
