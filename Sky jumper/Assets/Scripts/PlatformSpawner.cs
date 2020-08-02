using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] int platformAmount;
    [SerializeField] GameObject[] platformPrefabs;
    [SerializeField] GameObject[] finalPadPrefabs;
    public GameObject[] platformsOfLevel;
    public GameObject finalPad;

    [SerializeField] float maxScaleX;
    [SerializeField] float minScaleX;

    [SerializeField] float maxSpeed;
    [SerializeField] float minSpeed;

    private void Start()
    {
        platformsOfLevel = RandomPlatforms();
        Invoke("PlatformInstantiater",0.1f);

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
 
    private void PlatformInstantiater()
    {
        int zPosDetecter = 0;
        int xPosDetecter = 1;
        for (int i = 0; i < platformsOfLevel.Length; i++)
        {
            Instantiate(platformsOfLevel[i],
                new Vector3(10 * xPosDetecter, -10, zPosDetecter + DistanceDetecter(platformsOfLevel[i])),Quaternion.identity);
            platformsOfLevel[i].GetComponent<PlatformMoveHandler>().SetMoveSpeed(PlatformSpeedRandomizer());
            platformsOfLevel[i].GetComponent<PlatformMoveHandler>().SetScaleX(PlatformScaleXRandomizer());
            zPosDetecter += DistanceDetecter(platformsOfLevel[i]);
            xPosDetecter *= -1;
        }
        finalPad = finalPadPrefabs[FinalPadRandomizer()];
        Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
    }

    private int DistanceDetecter(GameObject platform)
    {
        int value=0;
        if (platform.gameObject.CompareTag("BluePlatform"))
        {
            value= 4;
        }
        
        if (platform.gameObject.CompareTag("GreenPlatform"))
        {
            value =3;
        }
        
        if (platform.gameObject.CompareTag("YellowPlatform"))
        {
            value= 2;
        }

        return value;
    }
    private int PlatformRandomizer()
    {
        return Random.Range(0, platformPrefabs.Length);
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
