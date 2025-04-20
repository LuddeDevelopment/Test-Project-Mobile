using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class O_Obstacle : MonoBehaviour
{
    [Header("General Obstacle Settings")]
    [SerializeField] private float obstacleSpeed = 10f;
    [SerializeField] private float maxObstacleSpeed = 50f;

    #region UNITY METHODS

    private void Awake()
    {
        SetupCollider();
        SetupObstacleSpeed();
    }

    private void SetupObstacleSpeed()
    {
        obstacleSpeed += (0.05f * GameManager.Instance.currentScore);
        obstacleSpeed = Mathf.Clamp(obstacleSpeed, 10f, maxObstacleSpeed);
    }

    private void Update()
    {
        MoveObstacle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerTouchObstacle(other.gameObject);
        }
    }

    #endregion

    #region OBSTACLE BEHAVIOR

    private void SetupCollider()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void MoveObstacle()
    {
        transform.Translate(Vector3.back * obstacleSpeed * Time.deltaTime);
    }

    public virtual void OnBecameInvisible()
    {
        S_ScoreManager.Instance.AddScore(1);
        Destroy(gameObject);
    }


    public abstract void onPlayerTouchObstacle(GameObject player);

    #endregion
}
