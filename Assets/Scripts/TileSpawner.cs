using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TileSpawner : MonoBehaviour
{
    public LevelDataSO levelDataSO;
    [SerializeField] private GameObject tilePrefab;
    private int _tilesCount = 0;
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

    private string _levelName;
    private int _levelIndex;
    private float _playTime;
    private int _tilesVariantCount;
    private List<LevelTile> _levelTiles = new();

    private float playTime;
    [SerializeField] Slider timer;
    public UnityEvent OnWon;

    public UnityEvent OnTimerEnd;

    void Start()
    {
        int _loadedIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 0);
        if (_loadedIndex < 0 || _loadedIndex >= levelDataSO.levelContent.Count)
        {
            // Reset back to LV0
            PlayerPrefs.SetInt("CurrentLevelIndex", 0);
            _loadedIndex = 0;
            // Debug.LogError("Invalid Level Index!");
        }
        _levelIndex = levelDataSO.levelContent[_loadedIndex].levelNumber - 1;
        _levelName = levelDataSO.levelContent[_levelIndex].levelName;
        _playTime = levelDataSO.levelContent[_levelIndex].playTime;
        _tilesVariantCount = levelDataSO.levelContent[_levelIndex].levelTiles.Count;
        _levelTiles = levelDataSO.levelContent[_levelIndex].levelTiles;

        foreach (LevelTile levelTile in _levelTiles)
        {
            _tilesCount += levelTile.tileChance * 3;
        }
        for (int currentTileIndex = 0; currentTileIndex < _tilesVariantCount; currentTileIndex++)
        {
            int numberOfEachTile = _levelTiles[currentTileIndex].tileChance * 3;
            for (int i = 0; i < numberOfEachTile; i++)
            {
                Vector3 randomPosition = new Vector3(
                Random.Range(-X_LIMIT, X_LIMIT),
                Random.Range(Y_LOWER_LIMIT, Y_UPPER_LIMIT),
                Random.Range(-Z_LIMIT, Z_LIMIT)
                );
                Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                GameObject instantiatedPrefab = Instantiate(tilePrefab, randomPosition, randomRotation);
                Tile tile = instantiatedPrefab.GetComponent<Tile>();

                tile.SetTileParameter(
                _levelTiles[currentTileIndex].tileType.ToString(),
                _levelTiles[currentTileIndex].tileSprite,
                _levelTiles[currentTileIndex].tileChance
                );
            }
        }
        playTime = _playTime;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (playTime > 0)
        {
            timer.value = playTime / _playTime;
            yield return null;
            playTime -= Time.deltaTime;
        }

        OnTimerEnd?.Invoke();
    }

    public float getCurrentLevelPlayTime()
    {
        return _playTime;
    }

    public void HandleOnMatchTiles()
    {
        _tilesCount -= 3;
        if (_tilesCount <= 0)
        {
            OnWon.Invoke();
        }
    }

}
