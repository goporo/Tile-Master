using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    private string _tileType;
    private Sprite _tileSprite;

    private int _tileChance;
    [SerializeField] private GameObject topLocation;


    private Outline _outline;
    private Rigidbody _rigidbody;
    private BoxCollider _boxcollider;
    private float highlightDuration = 0.2f;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _rigidbody = GetComponent<Rigidbody>();
        _boxcollider = GetComponent<BoxCollider>();
        topLocation.GetComponent<SpriteRenderer>().sprite = _tileSprite;
    }

    /// <summary>
    /// This function get called right after Instantiate the prefab but before Start executes
    /// </summary>
    /// <param name="tileType"></param>
    /// <param name="tileSprite"></param>
    /// <param name="tileChance"></param>
    public void SetTileParameter(string tileType, Sprite tileSprite, int tileChance)
    {
        _tileType = tileType;
        _tileSprite = tileSprite;
        _tileChance = tileChance;
    }

    /// <summary>
    /// Check if tileName are equal
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Compare(Tile y)
    {
        return this._tileType.CompareTo(y._tileType);
    }
    public int Compare(Tile x, Tile y)
    {
        return x._tileType.CompareTo(y._tileType);
    }
    public void clearTile()
    {
        gameObject.SetActive(false);
    }
    public void clearPhysics()
    {
        _rigidbody.isKinematic = true;
        _boxcollider.enabled = false;
    }
    public void DisplayOutline()
    {
        _outline.enabled = true;
        StartCoroutine(DisableOutlineAfterDelay(highlightDuration));
    }

    private IEnumerator DisableOutlineAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _outline.enabled = false;
    }
}
