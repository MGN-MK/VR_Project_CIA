using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Already Created SpawnPoints")]
    public bool spawnCreated;
    public string[] spawnTag;
    public GameObject[] spawnPointsCreated;

    [Header("Create Random Spawns Already Made")]
    public bool spawnRandomMade;
    public int spawnCount;
    public Vector3 spawnArea;
    public GameObject[] prfbsSpawn;
    public string seedSpawnsMaded = null;
    public int currentSeedSpawnsMaded = 0;
    public GameObject[] spawnPointsRandom;

    [Header("Create Random SpawnAreas")]
    public bool spawnRandom;
    public int spawnRandomCount;
    public int objSpawnMax;
    public int objSpawnMin;
    public float spawnTimeMax;
    public float spawnTimeMin;
    public float offSizeMax;
    public float offSizeMin;
    public Vector3 spawnRandomArea;
    public Vector3 spawnRandomPos;
    public GameObject[] spawnersBase;
    public string seedRandomSpawns = null;
    public int currentSeedRandomSpawns = 0;
    public GameObject[] spawnPointsGenerated;
    
    public int spawners
    {
        get { return b; }
    }
    public GameObject[] allSpawnsGet
    {
        get => allSpawns;
    }

    private int a = 0;
    private int b = 0;
    private GameObject spawn;
    private GameObject[] allSpawns;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomSeed();
        SetSpawners();
        SetAllSpawners();
    }

    private void SetSpawners()
    {
        if (spawnCreated)
        {
            foreach (var spawnTag in spawnTag)
            {
                var tagged = GameObject.FindGameObjectsWithTag(spawnTag);
                spawnPointsCreated = new GameObject[tagged.Length];

                for (int i = 0; i < tagged.Length; i++)
                {
                    spawnPointsCreated[a] = tagged[i];
                    tagged[i].transform.SetParent(transform);
                    a++;
                }
            }
        }

        if (spawnRandomMade)
        {
            Random.InitState(currentSeedSpawnsMaded);
            spawnPointsRandom = new GameObject[spawnCount];
            for (int i = 0; i < spawnCount; i++)
            {
                spawn = prfbsSpawn[Random.Range(0, prfbsSpawn.Length)];
                var pos = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), Random.Range(-spawnArea.z, spawnArea.z));
                var spawner = Instantiate(spawn, pos, Quaternion.identity);
                spawner.transform.SetParent(transform);
                spawnPointsRandom[i] = spawner;
            }
        }

        if (spawnRandom)
        {
            Random.InitState(currentSeedRandomSpawns);
            spawnPointsGenerated = new GameObject[spawnRandomCount];

            for (int i = 0; i < spawnRandomCount; i++)
            {
                var pos = new Vector3(Random.Range(-spawnRandomPos.x, spawnRandomPos.x), Random.Range(-spawnRandomPos.y, spawnRandomPos.y), Random.Range(-spawnRandomPos.z, spawnRandomPos.z));
                var spawner = Instantiate(spawnersBase[Random.Range(0, spawnersBase.Length)], pos, Quaternion.identity);
                spawner.GetComponent<SpawnPointsBase>().RangeSpawn = spawnRandomArea;
                spawner.GetComponent<SpawnPointsBase>().objects = Mathf.CeilToInt(Random.Range(objSpawnMin, objSpawnMax));
                spawner.GetComponent<SpawnPointsBase>().timeMin = Mathf.CeilToInt(Random.Range(spawnTimeMin, spawnTimeMax));
                spawner.GetComponent<SpawnPointsBase>().timeMax = Mathf.CeilToInt(Random.Range(spawnTimeMin, spawnTimeMax));
                spawner.GetComponent<SpawnPointsBase>().sizeOffsetPercentage = Random.Range(offSizeMin, offSizeMax);
                while (spawner.GetComponent<SpawnPointsBase>().timeMax <= spawner.GetComponent<SpawnPointsBase>().timeMin)
                {
                    spawner.GetComponent<SpawnPointsBase>().timeMax += 1f;
                }
                if(spawner.GetComponent<SpawnPointsBase>().timeMax > spawnTimeMax)
                {
                    spawner.GetComponent<SpawnPointsBase>().timeMax = spawnTimeMax;
                }
                spawner.transform.SetParent(transform);
                spawnPointsGenerated[i] = spawner;
            }
        }
    }

    private void SetAllSpawners()
    {
        allSpawns = new GameObject[spawnPointsCreated.Length + spawnPointsRandom.Length + spawnPointsGenerated.Length];

        foreach (var spawnCreated in spawnPointsCreated)
        {
            allSpawns[b] = spawnCreated;
            b++;
        }

        foreach (var spawnRandom in spawnPointsRandom)
        {
            allSpawns[b] = spawnRandom;
            b++;
        }
        foreach(var spawnGenerated in spawnPointsGenerated)
        {
            allSpawns[b] = spawnGenerated;
            b++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea.x * 2, spawnArea.y * 2, spawnArea.z * 2));
    }

    private void GenerateRandomSeed()
    {
        if(seedSpawnsMaded == null || seedSpawnsMaded == "0" && spawnRandomMade)
        {
            int tempSeed = Random.Range(0, 999999999);
            seedSpawnsMaded = tempSeed.ToString();
        }

        currentSeedSpawnsMaded = seedSpawnsMaded.GetHashCode();
        

        if (seedRandomSpawns == null || seedRandomSpawns == "0" && spawnRandom)
        {
            int tempSeed = Random.Range(0, 999999999);
            seedRandomSpawns = tempSeed.ToString();
        }

        currentSeedRandomSpawns = seedRandomSpawns.GetHashCode();
        
    }
}
