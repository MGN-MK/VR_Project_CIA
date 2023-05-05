using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnedStats : MonoBehaviour
{
    public GameObject arrow;
    public string PlayerTag;

    [Header("Buttons")]
    public KeyCode ShowAndHide = KeyCode.F;

    public KeyCode NextSpawner = KeyCode.I;
    public KeyCode PreviousSpawner = KeyCode.K;

    public KeyCode NextSpawnPoint = KeyCode.U;
    public KeyCode PreviousSpawnPoint = KeyCode.O;

    public KeyCode NextObj = KeyCode.J;
    public KeyCode PreviousObj = KeyCode.L;

    [Header("Texts")]
    public TextMeshProUGUI randomMadeSeed;
    public TextMeshProUGUI randomGeneratedSeed;
    public TextMeshProUGUI spawnersNum;

    public TextMeshProUGUI idSpawn;
    public TextMeshProUGUI idNumberSpawn;
    public TextMeshProUGUI spawnType;
    public TextMeshProUGUI spawningTime;
    public TextMeshProUGUI spawnedObjNumber;
    public TextMeshProUGUI spawnerSize;
    public TextMeshProUGUI spawnerPointsNumber;
    public TextMeshProUGUI spawnerSeed;

    public TextMeshProUGUI spawnerPointName;
    public TextMeshProUGUI spawnerPointID;
    public TextMeshProUGUI spawnerPointsPos;

    public TextMeshProUGUI idObj;
    public TextMeshProUGUI idNumberObj;
    public TextMeshProUGUI objSpawnPos;
    public TextMeshProUGUI objPos;
    public TextMeshProUGUI objSize;

    //Strings que almacenan el contenido inicial de los textMeshPro
    private string randomMadeSeedtext;
    private string randomGeneratedSeedtext;
    private string spawnersNumtext;

    private string idSpawntext;
    private string idNumberSpawntext;
    private string spawnTypetext;
    private string spawningTimetext;
    private string spawnedObjNumbertext;
    private string spawnerSizetext;
    private string spawnerPointsNumbertext;
    private string spawnerSeedtext;

    private string spawnerPointNametext;
    private string spawnerPointIDtext;
    private string objSpawnPostext;
    private string spawnerPointsPostext;

    private string idObjtext;
    private string idNumberObjtext;
    private string objPostext;
    private string objSizetext;

    private int idNumSpawn = 0;
    private int idNumPoint = 1;
    private int idNumObj = 0;
    private bool isActive = false;
    private GameObject activeGreenArrow;
    private GameObject spawner;
    private GameObject spawnerPoint;
    private GameObject obj;
    private Vector3 objSpawn;
    private TextMeshProUGUI[] texts;
    private SpawnManager spawnManager;


    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindAnyObjectByType<SpawnManager>();
        texts = GetComponentsInChildren<TextMeshProUGUI>();

        randomMadeSeedtext = randomMadeSeed.text;
        randomGeneratedSeedtext = randomGeneratedSeed.text;
        spawnersNumtext = spawnersNum.text;

        idSpawntext = idSpawn.text;
        idNumberSpawntext = idNumberSpawn.text;
        spawnTypetext = spawnType.text;
        spawningTimetext = spawningTime.text;
        spawnedObjNumbertext = spawnedObjNumber.text;
        spawnerPointsNumbertext = spawnerPointsNumber.text;
        spawnerSizetext = spawnerSize.text;
        spawnerPointsNumbertext = spawnerPointsNumber.text;
        spawnerSeedtext = spawnerSeed.text;

        spawnerPointNametext = spawnerPointName.text;
        spawnerPointIDtext = spawnerPointID.text;
        spawnerPointsPostext = spawnerPointsPos.text;
        objSizetext = objSize.text;

        idObjtext = idObj.text;
        idNumberObjtext = idNumberObj.text;
        objSpawnPostext = objSpawnPos.text;
        objPostext = objPos.text;

        foreach (var text in texts)
        {
            text.gameObject.SetActive(isActive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(ShowAndHide))
        {
            isActive = !isActive;

            foreach(var text in texts)
            {
                text.gameObject.SetActive(isActive);
            }

            if(activeGreenArrow != null)
            {
                activeGreenArrow.SetActive(isActive);
            }
        }

        if (isActive)
        {
            SetStats();

            if (Input.GetKeyDown(NextSpawner))
            {
                if (idNumSpawn == spawnManager.spawners - 1)
                {
                    idNumSpawn = 0;
                }
                else
                {
                    idNumSpawn++;
                }
            }
            if (Input.GetKeyDown(PreviousSpawner))
            {
                if(idNumSpawn == 0)
                {
                    idNumSpawn = spawnManager.spawners - 1;
                }
                else
                {
                    idNumSpawn--;
                }
            }

            if (spawner.GetComponent<SpawnPointsBase>().spawnType == SpawnType.POINTS)
            {
                if (Input.GetKeyDown(NextSpawnPoint))
                {
                    if (idNumPoint == spawnManager.allSpawnsGet[idNumSpawn].GetComponent<SpawnPointsBase>().spawnPoints.Length - 1)
                    {
                        idNumPoint = 1;
                    }
                    else
                    {
                        idNumPoint++;
                    }
                }
                if (Input.GetKeyDown(PreviousSpawnPoint))
                {
                    if (idNumPoint == 1)
                    {
                        idNumPoint = spawnManager.allSpawnsGet[idNumSpawn].GetComponent<SpawnPointsBase>().spawnPoints.Length - 1;
                    }
                    else
                    {
                        idNumPoint--;
                    }
                }
            }

            if (Input.GetKeyDown(NextObj))
            {
                if (idNumObj == spawnManager.allSpawnsGet[idNumSpawn].GetComponent<SpawnPointsBase>().objNumber - 1)
                {
                    idNumObj = 0;
                }
                else
                {
                    idNumObj++;
                }
            }
            if (Input.GetKeyDown(PreviousObj))
            {
                if (idNumObj == 0)
                {
                    idNumObj = spawnManager.allSpawnsGet[idNumSpawn].GetComponent<SpawnPointsBase>().objNumber - 1;
                }
                else
                {
                    idNumObj--;
                }
            }
        }
    }

    private void SetStats()
    {
        spawner = spawnManager.allSpawnsGet[idNumSpawn];
        obj = spawner.GetComponent<SpawnPointsBase>().spawned[idNumObj];

        if (activeGreenArrow != null)
        {
            activeGreenArrow.transform.position = obj.transform.position + new Vector3(0f, 2f, 0f);
            activeGreenArrow.transform.LookAt(GameObject.FindGameObjectWithTag(PlayerTag).transform);
        }
        else
        {
            activeGreenArrow = Instantiate(arrow, obj.transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
            activeGreenArrow.transform.LookAt(GameObject.FindGameObjectWithTag(PlayerTag).transform);
        }

        randomMadeSeed.text = randomMadeSeedtext + spawnManager.currentSeedSpawnsMaded;
        randomGeneratedSeed.text = randomGeneratedSeedtext + spawnManager.currentSeedRandomSpawns;
        spawnersNum.text = spawnersNumtext + spawnManager.allSpawnsGet.Length;

        idSpawn.text = idSpawntext + spawner.name;
        idNumberSpawn.text = idNumberSpawntext + idNumSpawn;
        spawnType.text = spawnTypetext + spawner.GetComponent<SpawnPointsBase>().spawnType;
        spawningTime.text = spawningTimetext + spawner.GetComponent<SpawnPointsBase>().timeMin + "s - " + spawner.GetComponent<SpawnPointsBase>().timeMax + "s (" + spawner.GetComponent<SpawnPointsBase>().timer + "s)";
        spawnedObjNumber.text = spawnedObjNumbertext + spawner.GetComponent<SpawnPointsBase>().objNumber;
        spawnerSeed.text = spawnerSeedtext + spawner.GetComponent<SpawnPointsBase>().currentSeed;
        if (spawner.GetComponent<SpawnPointsBase>().spawnType == SpawnType.AREA)
        {
            spawnerSize.text = spawnerSizetext + spawner.GetComponent<SpawnPointsBase>().RangeSpawn * 2;
            spawnerPointsNumber.text = spawnerPointsNumbertext + "Doesn't apply.";

            spawnerPointName.text = spawnerPointNametext + "Doesn't apply.";
            spawnerPointID.text = spawnerPointIDtext + "Doesn't apply.";
            spawnerPointsPos.text = spawnerPointsPostext + "Doesn't apply.";
        }
        else if (spawner.GetComponent<SpawnPointsBase>().spawnType == SpawnType.POINTS)
        {
            Debug.Log(idNumPoint);
            spawnerPoint = spawner.GetComponent<SpawnPointsBase>().spawnPoints[idNumPoint].gameObject;
            spawnerSize.text = spawnerSizetext + "Doesn't apply.";
            spawnerPointsNumber.text = spawnerPointsNumbertext + spawner.GetComponent<SpawnPointsBase>().spawnPoints.Length;

            spawnerPointName.text = spawnerPointNametext + spawnerPoint.name;
            spawnerPointID.text = spawnerPointIDtext + idNumPoint;
            spawnerPointsPos.text = spawnerPointsPostext + spawnerPoint.transform.position.y.ToString("F2") + ", " + spawnerPoint.transform.position.z.ToString("F2");
        }

        idObj.text = idObjtext + obj.name;
        idNumberObj.text = idNumberObjtext + idNumObj;
        objSpawnPos.text = objSpawnPostext + objSpawn.x.ToString("F2") + ", " + objSpawn.y.ToString("F2") + ", " + objSpawn.z.ToString("F2");
        objPos.text = objPostext + obj.transform.position.x.ToString("F2") + ", " + obj.transform.position.y.ToString("F2") + ", " + obj.transform.position.z.ToString("F2");
        objSize.text = objSizetext + obj.transform.localScale.x.ToString("F2") + ", " + obj.transform.localScale.y.ToString("F2") + ", " + obj.transform.localScale.z.ToString("F2");
    }
}
