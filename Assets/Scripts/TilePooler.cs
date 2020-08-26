using System.Collections.Generic;
using UnityEngine;

public class TilePooler : MonoBehaviour
{
    #region Singleton
    public static TilePooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    [HideInInspector]
    public List<GameObject> disabledTiles, activeTiles;//disabled tiles
    [HideInInspector]
    public List<GameObject> disabledCoins, activeCoins;
    [HideInInspector]
    public List<GameObject> disabledPowerUps, activePowerUps;

    [Header("Prefabs")]
    public GameObject[] tilePrefabs;//array of tiles
    public GameObject CoinPrefab;
    public GameObject[] PowerUps;
    [Header("Values")]
    public int numberOfCopies;

    public int zSpawn, tileLength, numberOfStartTiles, numberOfCoins, playerRangeOfVision;
    public Transform player;
    Vector3 position = Vector3.zero;
    void Start()
    {
        for (int i = 0; i < tilePrefabs.Length; i++)//creating tiles
        {
            for (int j = 0; j < numberOfCopies; j++)//and few copies of each tile
            {
                GameObject obj = Instantiate(tilePrefabs[i], transform.forward * zSpawn, transform.rotation);
                disabledTiles.Add(obj);//add created tile to list of disabled
                obj.SetActive(false);//disable tile
            }
        }
        for (int j = 0; j < numberOfCopies; j++)
        {
            for (int i = 0; i < PowerUps.Length; i++)
            {
                GameObject powerUp = Instantiate(PowerUps[i], transform.forward, transform.rotation);
                disabledPowerUps.Add(powerUp);
                powerUp.SetActive(false);
            }
        }
        for (int i = 0; i < numberOfCoins; i++)
        {
            GameObject coin = Instantiate(CoinPrefab, transform.forward, transform.rotation);
            disabledCoins.Add(coin);//add created coin to list of disabled
            coin.SetActive(false);//disable coin
        }
        for (int i = 0; i < numberOfStartTiles; i++)//create some tiles for start, cause player is falling down
        {
            position.z = zSpawn;
            Spawn(position);
        }
    }
    void Spawn(Vector3 v3)
    {
        int i = Random.Range(0, disabledTiles.Count - 1);//take random tile from disabled list
        disabledTiles[i].transform.position = v3; //change position
        disabledTiles[i].SetActive(true); //activate 
        activeTiles.Add(disabledTiles[i]);//add to list of activated tiles
        disabledTiles.Remove(disabledTiles[i]);
        zSpawn += tileLength;
    }
    void Update()
    {
        if (player.position.z + playerRangeOfVision > zSpawn )//delete old tiles and add new
        {
            position.z = zSpawn;
            Spawn(position);
            disabledTiles.Add(activeTiles[0]);
            activeTiles[0].SetActive(false);
            activeTiles.Remove(activeTiles[0]);
        }
    }
    public GameObject EnableObject(Transform transform, List<GameObject> active, List<GameObject> disabled)
    {
        GameObject coin = disabled[0];
        active.Add(coin);
        disabled.Remove(coin);
        coin.transform.position = transform.position;
        coin.SetActive(true);
        return coin;
    }
    
    public void DisableObject(GameObject coin, List<GameObject> active, List<GameObject> disabled)
    {
        active.Remove(coin);
        disabled.Add(coin);
        coin.SetActive(false);
    }
}   