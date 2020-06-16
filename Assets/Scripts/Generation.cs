using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public GameObject[] tilePrefabs;//massive of tiles that we'll use
    public float zSpawn = 0f;
    public float tileLength;
    public int numberOfTiles;
    public Transform player;
    private List<GameObject> activeTiles = new List<GameObject>();//list of instansiated tiles
    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)//spawn tiles at start
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
       if(player.position.z - 35> zSpawn - (numberOfTiles * tileLength))//delete old tiles and add new
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }
    public void SpawnTile(int tileIndex)//add tiles at z axis
    {
       GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);//instantiate new tile
        activeTiles.Add(go);// add new tile to a list
        zSpawn += tileLength;//defines when the player had passed tile
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
