using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public float chunkLength;

    public Chunk ShowChunks()
    {
        gameObject.SetActive(true);
        return this;
    }

    public Chunk HideChink()
    {
        gameObject.SetActive(false);
        return this;
    }
}
