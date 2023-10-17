using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    // public EventHandler TileAddEvent;
    [SerializeField] private string tileName;
    private Image tileSprite;
    /// <summary>
    /// Appearance of this tile
    /// </summary>
    private int tileChance;

    private Outline _outline;
    private Rigidbody _rigidbody;
    private BoxCollider _boxcollider;

    private void Start()
    {
        // Get the Outline component attached to the GameObject
        _outline = GetComponent<Outline>();
        _rigidbody = GetComponent<Rigidbody>();
        _boxcollider = GetComponent<BoxCollider>();

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
        // Destroy(gameObject);
    }
    public void clearPhysics()
    {
        _rigidbody.isKinematic = true;
        _boxcollider.enabled = false;
    }
    public void DisplayOutline()
    {
        // Enable the Outline component
        _outline.enabled = true;

        // Start the coroutine to disable the Outline after 1 second
        StartCoroutine(DisableOutlineAfterDelay(.2f));
    }

    private IEnumerator DisableOutlineAfterDelay(float delay)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(delay);

        // Disable the Outline component
        _outline.enabled = false;
    }
}
