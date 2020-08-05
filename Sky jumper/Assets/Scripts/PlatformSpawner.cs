using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlatformSpawner : MonoBehaviour
{
    public bool spawnerType1=true;
    public bool spawnerType2;
    public bool spawnerType3;

    [SerializeField] int platformAmount;
    [SerializeField] GameObject[] platformPrefabs;
    public GameObject[] platformsOfLevel;
    [SerializeField] GameObject[] finalPadPrefabs;
    GameObject finalPad;
    [SerializeField] int breakPadAmount;
    [SerializeField] GameObject[] breakPadPrefabs;
    public GameObject[] breakPadsOfLevel;
    [SerializeField] int movingBreakPadAmount;
    [SerializeField] GameObject[] movingBreakPadPrefabs;
    [SerializeField] int obstacleAmount;
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] int movingObstacleAmount;
    [SerializeField] GameObject[] movingObstaclePrefabs;

    [SerializeField] float maxScaleX;
    [SerializeField] float minScaleX;

    [SerializeField] float maxSpeed;
    [SerializeField] float minSpeed;

    private void Start()
    {
        platformsOfLevel = RandomPlatforms();
        breakPadsOfLevel = RandomBreakPads();
        if (spawnerType1)
        {
            Invoke("SpawnerType1Execute", 0.1f);    
        }
        else if (spawnerType2)
        {
            Invoke("SpawnerType2Execute", 0.1f); 
        }
    }

    private GameObject[] RandomPlatforms()
    {
        GameObject[] tempPlatforms = new GameObject[platformAmount];
        for (int i = 0; i < platformAmount; i++)
        {
            tempPlatforms[i] = platformPrefabs[PlatformRandomizer()];
        }

        return tempPlatforms;
    }
    
    private GameObject[] RandomBreakPads()
    {
        GameObject[] tempBreakPads = new GameObject[breakPadAmount];
        for (int i = 0; i < breakPadAmount; i++)
        {
            tempBreakPads[i] = breakPadPrefabs[PlatformRandomizer()];
        }

        return tempBreakPads;
    }

    private void SpawnerType1Execute()
    {
        int zPosDetecter = 0;
        int xPosDetecter = 1;
        for (int i = 0; i < platformsOfLevel.Length; i++)
        {
            Instantiate(platformsOfLevel[i],
                new Vector3(10 * xPosDetecter, -10, zPosDetecter + DistanceDetecter(platformsOfLevel[i])),
                Quaternion.identity);
            platformsOfLevel[i].GetComponent<PlatformMoveHandler>().SetMoveSpeed(PlatformSpeedRandomizer());
            platformsOfLevel[i].GetComponent<PlatformMoveHandler>().SetScaleX(PlatformScaleXRandomizer());
            zPosDetecter += DistanceDetecter(platformsOfLevel[i]);
            xPosDetecter *= -1;
        }

        finalPad = finalPadPrefabs[FinalPadRandomizer()];
        Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
    }
    private void SpawnerType2Execute()
    {
        int zPosDetecter = 0;
        int xPosDetecter = 1;
        int tempa = Mathf.FloorToInt(platformAmount / breakPadAmount)-1;
        int inca = 0;
        for (int i = 0; i < platformsOfLevel.Length; i++)
        {
            tempa--;
            Instantiate(platformsOfLevel[i],
                new Vector3(10 * xPosDetecter, -10, zPosDetecter + DistanceDetecter(platformsOfLevel[i])),
                Quaternion.identity);
            platformsOfLevel[i].GetComponent<PlatformMoveHandler>().SetMoveSpeed(PlatformSpeedRandomizer());
            platformsOfLevel[i].GetComponent<PlatformMoveHandler>().SetScaleX(PlatformScaleXRandomizer());
            zPosDetecter += DistanceDetecter(platformsOfLevel[i]);
            xPosDetecter *= -1;
            if (tempa == 0)
            {
                Instantiate(breakPadsOfLevel[inca],
                    new Vector3(0, 0, zPosDetecter + DistanceDetecter(breakPadsOfLevel[inca])),
                    Quaternion.identity);
                zPosDetecter += DistanceDetecter(breakPadsOfLevel[inca]);
                inca++;
            }
        }

        finalPad = finalPadPrefabs[FinalPadRandomizer()];
        Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
    }

    private int DistanceDetecter(GameObject platform)
    {
        int value = 0;
        if (platform.gameObject.CompareTag("BluePlatform") || platform.gameObject.CompareTag("BlueFinalPad")|| 
            platform.gameObject.CompareTag("BlueBreakPad"))
        {
            value = 4;
        }

        if (platform.gameObject.CompareTag("GreenPlatform") || platform.gameObject.CompareTag("GreenFinalPad")|| 
            platform.gameObject.CompareTag("GreenBreakPad"))
        {
            value = 3;
        }

        if (platform.gameObject.CompareTag("YellowPlatform") || platform.gameObject.CompareTag("YellowFinalPad")|| 
            platform.gameObject.CompareTag("YellowBreakPad"))
        {
            value = 2;
        }

        return value;
    }

    // Randomizer'ın hepsini GameObject[] variable alan bir method yap adı da ObjectRandomizer() falan olsun
    private int PlatformRandomizer()
    {
        return Random.Range(0, platformPrefabs.Length);
    }

    private int BreakPadRandomizer()
    {
        return Random.Range(0, breakPadPrefabs.Length);
    }

    private int FinalPadRandomizer()
    {
        return Random.Range(0, finalPadPrefabs.Length);
    }

    private float PlatformSpeedRandomizer()
    {
        return Random.Range(minSpeed, maxSpeed);
    }

    private float PlatformScaleXRandomizer()
    {
        return Random.Range(minScaleX, maxScaleX);
    }
}
