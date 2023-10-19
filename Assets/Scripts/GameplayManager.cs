using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    private static List<Tile> tiles = new();
    private static int tilesMaxLength;
    private AudioSource audioSource;

    [SerializeField] private Transform targetPoint;
    [SerializeField] private float moveDuration;
    [SerializeField] private float tileWidth;
    [SerializeField] private float tileSizeScaleInHolder;
    [SerializeField] private AudioClip pickUpTileAudio;
    [SerializeField] private AudioClip matchedTileAudio;
    [SerializeField] private GameObject clearTileEffect;

    private bool isTileMoving = false;
    private bool isLost = false;
    private Coroutine rearrangeCoroutine = null;
    private int matchedTiles = 0;
    public UnityEvent OnLost;
    public UnityEvent OnMatchTiles;


    private void Awake()
    {
        tiles.Clear();
    }
    private void Start()
    {
        tilesMaxLength = 7;
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator RearrangeTiles()
    {
        isTileMoving = true;
        float elapsedTime = 0f;

        Vector3[] startPositions = new Vector3[tiles.Count];
        Quaternion[] startRotations = new Quaternion[tiles.Count];
        Vector3[] startScales = new Vector3[tiles.Count];

        for (int i = 0; i < tiles.Count; i++)
        {
            startPositions[i] = tiles[i].transform.position;
            startRotations[i] = tiles[i].transform.rotation;
            startScales[i] = tiles[i].transform.localScale;
        }

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;

            for (int i = 0; i < tiles.Count; i++)
            {
                Vector3 targetPosition = targetPoint.transform.position + new Vector3(0, 0, -tileWidth * i);
                tiles[i].transform.position = Vector3.Lerp(startPositions[i], targetPosition, t);

                float angle = Quaternion.Angle(startRotations[i], Quaternion.identity) * t;
                tiles[i].transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                tiles[i].transform.localScale = Vector3.Lerp(startScales[i], Vector3.one * tileSizeScaleInHolder, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            Vector3 targetPosition = targetPoint.transform.position + new Vector3(0, 0, -tileWidth * i);
            tiles[i].transform.position = targetPosition;
            tiles[i].transform.rotation = Quaternion.identity;
            tiles[i].transform.localScale = Vector3.one * tileSizeScaleInHolder;
        }

        isTileMoving = false;
        rearrangeCoroutine = null;
    }

    private void StartRearrangeCoroutine()
    {
        if (rearrangeCoroutine != null)
        {
            StopCoroutine(rearrangeCoroutine);
        }
        rearrangeCoroutine = StartCoroutine(RearrangeTiles());
    }


    public void AddTileToList(Tile tile)
    {
        tiles.Add(tile);
        tiles.Sort((tile1, tile2) => tile.Compare(tile1, tile2));

        StartRearrangeCoroutine();


        StartCoroutine(WaitUntil(() => !isTileMoving, () =>
        {
            int firstTileIndex = HasTripleTiles();
            if (firstTileIndex >= 0)
            {
                // Debug.Log("Matched!");
                audioSource.PlayOneShot(matchedTileAudio);
                RemoveMatchedTilesFromList(firstTileIndex);
                PlayMatchedEffect(firstTileIndex);
                StartRearrangeCoroutine();

                OnMatchTiles.Invoke();
            }
            else if (tiles.Count >= tilesMaxLength)
            {
                OnLost?.Invoke();
                isLost = true;
            }

        }));
    }

    IEnumerator WaitUntil(Func<bool> condition, Action action)
    {
        yield return new WaitUntil(condition);
        action?.Invoke();
    }

    public void PlayMatchedEffect(int firstTileIndex)
    {
        for (int i = firstTileIndex; i < firstTileIndex + 3; i++)
        {
            Vector3 targetPosition = targetPoint.transform.position + new Vector3(0, 0, -tileWidth * i);
            Instantiate(clearTileEffect, targetPosition, Quaternion.identity);
        }
    }

    public void RemoveMatchedTilesFromList(int firstTileIndex)
    {
        for (int i = 0; i < 3; i++)
        {

            tiles[firstTileIndex].clearTile();
            tiles.Remove(tiles[firstTileIndex]);
        }
    }

    /// <summary>
    /// Get index of first tile that form triple tiles
    /// </summary>
    /// <returns></returns>
    private int HasTripleTiles()
    {
        if (tiles.Count < 3) return -1;
        int tilesCount = 1;

        for (int i = 1; i < tiles.Count; i++)
        {
            if (tiles[i].Compare(tiles[i - 1]) == 0)
            {
                tilesCount++;
                if (tilesCount == 3) return i - 2;
            }
            else tilesCount = 1;
        }

        return -1;
    }

    public void SetTilesMaxLength(int length)
    {
        tilesMaxLength = length;
    }

    public int GetTilesMaxLength()
    {
        return tilesMaxLength;
    }

    public void SetGameLost(bool isLost)
    {
        this.isLost = isLost;
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        if (!isLost && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject selectedObject = hit.collider.gameObject;
                if (selectedObject.TryGetComponent(out Tile selectedTile))
                {
                    // if not in the list yet
                    if (!tiles.Contains(selectedTile))
                    {
                        audioSource.PlayOneShot(pickUpTileAudio);
                        selectedTile.DisplayOutline();
                        selectedTile.clearPhysics();
                        AddTileToList(selectedTile);
                    }

                }

            }
        }
    }
}
