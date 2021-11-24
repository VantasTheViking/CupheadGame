using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float backgroundSpeed;
    [SerializeField] private Renderer backgroundRend;

    // Update is called once per frame
    void Update()
    {
        backgroundRend.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0f);
    }
}
