using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Obstacle
{
    public GameObject obstaclePrefab;
    public int spawnChance;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Obstacle Settings")]
    [SerializeField] private Transform[] obstacleSpawnPoints;
    [SerializeField] private List<Obstacle> spawnableObstacles;

    [Header("Score Settings")]
    public int currentScore;

    private Coroutine spawnRoutine;

    #region UNITY METHODS

    private void Start()
    {
        if (Instance == null)
            Instance = this;

        StartGame();
    }

    #endregion

    #region ACCESSIBLE METHODS MANAGEMENT

    public Transform GetRandomSpawnPoint()
    {
        return obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Length)];
    }

    public Obstacle GetRandomObstacle()
    {
        int randomValue = Random.Range(1, 101);
        List<Obstacle> eligible = spawnableObstacles.FindAll(o => randomValue <= o.spawnChance);

        if (eligible.Count > 0)
            return eligible[Random.Range(0, eligible.Count)];

        Debug.LogWarning("No obstacle met the spawn chance requirements.");
        return null;
    }

    #endregion

    #region GAME MANAGEMENT

    public void StartGame()
    {
        spawnRoutine = StartCoroutine(SpawnObstacles());
    }

    public void RestartGame()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }

    #endregion

    #region OBSTACLE SPAWNING

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 2f));

            Obstacle obstacleToSpawn = GetRandomObstacle();

            if (obstacleToSpawn?.obstaclePrefab != null)
            {
                SpawnObstacle(obstacleToSpawn.obstaclePrefab);
            }
        }
    }

    private void SpawnObstacle(GameObject obstacle)
    {
        Instantiate(obstacle, GetRandomSpawnPoint().position, Quaternion.identity);
    }

    #endregion
}
