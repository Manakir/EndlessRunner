using System.Collections;
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
    public List<GameObject> disabled;
    public List<GameObject> active;
    List<int> toMove;
    public GameObject[] tilePrefabs;

    public int numberOfCopies, zSpawn, tileLength, numberOfStartTiles;

    public Transform player;
    Vector3 position = Vector3.zero;
    void Start()
    {
        for (int i = 0; i < tilePrefabs.Length; i++)
            {
                for (int j = 0; j < numberOfCopies; j++)
                {
                    GameObject obj = Instantiate(tilePrefabs[i], transform.forward * zSpawn, transform.rotation);
                    disabled.Add(obj);
                    obj.SetActive(false);
                     //zSpawn += tileLength;
                }
        }
        for (int i = 0; i < numberOfStartTiles; i++)
        {
            position.z = zSpawn;
            Spawn(position);
            zSpawn += tileLength;
        }
    }
    void Spawn(Vector3 v3)
    {
        int i = Random.Range(0, disabled.Count - 1);
        if (disabled[i].activeSelf)
        {
            Check();
            i = Random.Range(0, disabled.Count - 1);
        }
        disabled[i].SetActive(true); //включить
        disabled[i].transform.position = v3; //переместить
        active.Add(disabled[i]);
        disabled.Remove(disabled[i]); //убрать с листа
    }
    void Check()
    {
        for (int i = 0; i < disabled.Count; i++){
            if (disabled[i].activeSelf)
            {
                toMove.Add(i);
            }
        }
        foreach(int i in toMove)
        {
            active.Add(disabled[i]);
            disabled.RemoveAt(i);
        }

    }
    void Update()
    {
        if (player.position.z -10 > zSpawn - (numberOfCopies * tileLength))//delete old tiles and add new
        {
            position.z = zSpawn;
            Spawn(position);
            zSpawn += tileLength;
            disabled.Add(active[0]);
            active[0].SetActive(false);
            active.Remove(active[0]);
        }
    }
}