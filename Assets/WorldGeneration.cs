using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

    private float chunkSpawnZ;
    private Queue<Chunk> activeChunks = new Queue<Chunk>();
    private List<Chunk> chunkPool = new List<Chunk>();

    [SerializeField] private int firstChunkSpawnPosition = 10;
    [SerializeField] private int chunkOnScreen = 5;
    [SerializeField] private float despawnDistance = 5.0f;

    [SerializeField] private List<GameObject> chunkPrefab;
    [SerializeField] private Transform cameraTransform;



    void Start()
    {
        // Check if we have an empty chunkPrefab list
        if(chunkPrefab.Count == 0)
        {
            Debug.LogError("No Chunk prefab found on the world generator, please assign some chunks!");
            return;
        }


        //Try to assign the cameraTransform

        if (!cameraTransform)
        {
            cameraTransform = Camera.main.transform;
            Debug.Log("We've assigned cameraTransform automatically to the Camera.main");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        float cameraZ = cameraTransform.transform.position.z;
        Chunk lastChunk = activeChunks.Peek();
        if (cameraZ >= lastChunk.transform.position.z + lastChunk.chunkLength + despawnDistance)
        {
        ScanPosition();
        DeleteLastChunk();
        }
    }

    private void ScanPosition()
    {

    }


    private void SpawnNewChunk()
    {
        // Get a random index for which prefab to spawn

        int randomindex = Random.Range(0, chunkPrefab.Count);

        // Does it already exist within our pool
        Chunk chunk = null;


        // Place the object
        if (!chunk)
        {
            GameObject go = Instantiate(chunkPrefab[randomindex], transform );
            chunk = go.GetComponent<Chunk>();
        }

        //Place the object and show it
        chunk.transform.position = new Vector3(0, 0, chunkSpawnZ);
        chunkSpawnZ += chunk.chunkLength;

        //Store the value, to reuse in our pool
        activeChunks.Enqueue(chunk);
        chunk.ShowChunks();

    }


    private void DeleteLastChunk()
    {
        Chunk chunk = activeChunks.Dequeue();
        chunk.HideChunk();
        chunkPool.Add(chunk);
    }

}
