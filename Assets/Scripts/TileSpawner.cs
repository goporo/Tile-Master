using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    public int tilesCount { get; private set; }
    /// <summary>
    /// Vertical limit
    /// </summary>
    private const float X_LIMIT = 1f;
    /// <summary>
    /// Horizontal limit
    /// </summary>
    private const float Z_LIMIT = 0.6f;
    /// <summary>
    /// Depth Limit
    /// </summary>
    private const float Y_LOWER_LIMIT = 0.06f;
    private const float Y_UPPER_LIMIT = 0.45f;
    public UnityEvent OnWon;

    void Start()
    {
        tilesCount = 36;
        for (int i = 0; i < tilesCount; i++)
        {
            Vector3 randomPosition = new Vector3(
            Random.Range(-X_LIMIT, X_LIMIT),
            Random.Range(Y_LOWER_LIMIT, Y_UPPER_LIMIT),
            Random.Range(-Z_LIMIT, Z_LIMIT)
            );
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            Instantiate(tilePrefabs[i % 6], randomPosition, randomRotation);
        }
    }

    public void HandleOnMatchTiles()
    {
        tilesCount -= 3;
        if (tilesCount <= 0)
        {
            OnWon.Invoke();
        }
    }

}
