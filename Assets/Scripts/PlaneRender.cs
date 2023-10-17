using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRender : MonoBehaviour
{
    public Color startColor = new Color(0.227f, 0.58f, 0.78f, 1f); // #3a94c8 in RGBA
    public Color endColor = new Color(0.16f, 0.31f, 0.6f, 1f);   // #284f9a in RGBA
    public float lerpFactor = 0.58f; // Interpolation factor between 0 and 1

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            Material material = meshRenderer.material;
            if (material != null)
            {
                // Set the material color gradient based on the lerpFactor
                material.color = Color.red;
            }
        }
    }
}
