using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius = 1f;
    public float chunkOffset = 20f;
    public LayerMask terrainMask;

    private Vector3 noTerrainPosition;
    private Movement pm;

    private void Start()
    {
        pm = FindObjectOfType<Movement>();
    }

    private void Update()
    {
        ChunkChecker();
    }

    void ChunkChecker()
    {
        Vector3 offset = Vector3.zero;

        if (pm.moveDir.x > 0 && pm.moveDir.y == 0) offset = new Vector3(chunkOffset, 0, 0);     // right
        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0) offset = new Vector3(-chunkOffset, 0, 0);    // left
        else if (pm.moveDir.y > 0 && pm.moveDir.x == 0) offset = new Vector3(0, chunkOffset, 0);     // up
        else if (pm.moveDir.y < 0 && pm.moveDir.x == 0) offset = new Vector3(0, -chunkOffset, 0);    // down
        else return; // не движется строго по одной оси — ничего не делать

        Vector3 checkPos = player.transform.position + offset;

        if (!Physics2D.OverlapCircle(checkPos, checkerRadius, terrainMask))
        {
            noTerrainPosition = checkPos;
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
    }
}
