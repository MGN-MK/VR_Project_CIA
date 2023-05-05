using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType
{
    AREA, POINTS
}

public class SpawnPointsBase : MonoBehaviour
{
    public SpawnType spawnType;
    public GameObject[] prfbs;
    public int objects;
    public float timeMin;
    public float timeMax;
    public float sizeOffsetPercentage;
    public GameObject[] spawned;
    public Transform[] spawnPoints;

    [Header("Range for AreaSpawn")]
    public Vector3 RangeSpawn;

    [Header("Seed Management")]
    public string seed = null;
    public int currentSeed = 0;

    public int objNumber
    {
        get { return obj; }
    }
    public float timer
    {
        get { return time; }
    }

    private int obj = 0;
    private float time;
    private bool spawning = true;
    private GameObject choosed;
    private GameObject spawnedObject;
    private Vector3 posSelected;
    private Vector3 scale = new(1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomSeed();

        spawned = new GameObject[objects];
        if(spawnType == SpawnType.POINTS)
        {
            spawnPoints = GetComponentsInChildren<Transform>();
        }

        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if(obj >= objects)
        {
            spawning = false;
        }
        else
        {
            spawning = true;
        }
    }

    public IEnumerator Spawn()
    {
        while (spawning)
        {
            switch (spawnType)
            {
                case SpawnType.AREA:
                    time = Random.Range(timeMin, timeMax);
                    choosed = prfbs[Random.Range(0, prfbs.Length)];

                    posSelected = new Vector3(Random.Range(-RangeSpawn.x, RangeSpawn.x), Random.Range(-RangeSpawn.y, RangeSpawn.y), Random.Range(-RangeSpawn.z, RangeSpawn.z)) + transform.position;

                    NewObject(posSelected);

                    yield return new WaitForSeconds(time);
                    break;

                case SpawnType.POINTS:
                    time = Random.Range(timeMin, timeMax);
                    choosed = prfbs[Random.Range(0, prfbs.Length)];
                    posSelected = spawnPoints[Random.Range(1, spawnPoints.Length)].position;

                    NewObject(posSelected);

                    yield return new WaitForSeconds(time);
                    break;
            }
        }
    }

    private void NewObject(Vector3 pos)
    {
        scale += scale * Random.Range(-sizeOffsetPercentage, sizeOffsetPercentage) / 100;
        spawnedObject = Instantiate(choosed, pos, Quaternion.identity);
        spawnedObject.transform.SetParent(transform);
        spawnedObject.transform.localScale = scale;
        spawned[obj] = spawnedObject;
        obj++;
        Debug.Log("New object spawned at " + pos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(spawnType == SpawnType.AREA)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(RangeSpawn.x * 2, RangeSpawn.y * 2, RangeSpawn.z * 2));
        }
    }

    private void GenerateRandomSeed()
    {
        if (seed == null || seed == "0")
        {
            int tempSeed = Random.Range(0, 999999999);
            seed = tempSeed.ToString();
        }
        currentSeed = seed.GetHashCode();
        Random.InitState(currentSeed);
    }
}