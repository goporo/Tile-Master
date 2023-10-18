using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private string tileName;
    [SerializeField] private Sprite tileSprite;


    // [SerializeField] private int tileChance;
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
        topLocation.GetComponent<SpriteRenderer>().sprite = tileSprite;
    }

    /// <summary>
    /// Check if tileName are equal
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Compare(Tile y)
    {
        return this.tileName.CompareTo(y.tileName);
    }
    public int Compare(Tile x, Tile y)
    {
        return x.tileName.CompareTo(y.tileName);
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
