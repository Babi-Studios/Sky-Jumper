﻿using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlatformSpawner : MonoBehaviour
{
    public bool spawnerType1;
    public bool spawnerType2;
    public bool spawnerType3;
    public bool spawnerType4;
    public bool spawnerType5;
 
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
    public GameObject[] movingBreakPadsOfLevel;
    [SerializeField] int obstacleAmount;
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] int movingObstacleAmount;
    [SerializeField] GameObject[] movingObstaclePrefabs;

    [SerializeField] float maxScaleX;
    [SerializeField] float minScaleX;

    [SerializeField] float maxSpeed;
    [SerializeField] float minSpeed;
    
    [SerializeField] float maxPeriodOfMBP;
    [SerializeField] float minPeriodOfMBP;
    [SerializeField] float maxMoveRangeOfMBP;
    [SerializeField] float minMoveRangeOfMBP;

    private void Start()
    {
        SpawnerTypeDecider();
        platformsOfLevel = RandomPlatforms();
        breakPadsOfLevel = RandomBreakPads();
        movingBreakPadsOfLevel = RandomMovingBreakPads();

        if (spawnerType1)
        {
            Invoke("SpawnerType1Execute",0);
        }
        else if (spawnerType2)
        {
            Invoke("SpawnerType2Execute", 0);    
        }
        else if (spawnerType3)
        {
            Invoke("SpawnerType3Execute", 0); 
        }
        else if (spawnerType4)
        {
            Invoke("SpawnerType4Execute", 0); 
        }
        else if (spawnerType5)
        {
            Invoke("SpawnerType5Execute", 0);
        }
    }

    private GameObject[] RandomPlatforms()
    {
        GameObject[] tempPlatforms = new GameObject[platformAmount];
        for (int i = 0; i < platformAmount; i++)
        {
            tempPlatforms[i] = platformPrefabs[Randomizer(platformPrefabs)];
        }

        return tempPlatforms;
    }
    
    private GameObject[] RandomBreakPads()
    {
        GameObject[] tempBreakPads = new GameObject[breakPadAmount];
        for (int i = 0; i < breakPadAmount; i++)
        {
            tempBreakPads[i] = breakPadPrefabs[Randomizer(breakPadPrefabs)];
        }

        return tempBreakPads;
    }
    
    private GameObject[] RandomMovingBreakPads()
    {
        GameObject[] tempMovingBreakPads = new GameObject[movingBreakPadAmount];
        for (int i = 0; i < movingBreakPadAmount; i++)
        {
            tempMovingBreakPads[i] = movingBreakPadPrefabs[Randomizer(movingBreakPadPrefabs)];
        }

        return tempMovingBreakPads;
    }

    private void SpawnerType1Execute()
    {
        if (breakPadAmount > 0)
        {
            int zPosDetecter = 0;
            for (int i = 0; i < breakPadsOfLevel.Length; i++)
            {
                Instantiate(breakPadsOfLevel[i],
                    new Vector3(0, 0, zPosDetecter + DistanceDetecter(breakPadsOfLevel[i])),
                    Quaternion.identity);
                zPosDetecter += DistanceDetecter(breakPadsOfLevel[i]);
            }

            finalPad = finalPadPrefabs[Randomizer(finalPadPrefabs)];
            Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
        }
        else if (movingBreakPadAmount > 0)
        {
            int zPosDetecter = 0;
          for (int i = 0; i < movingBreakPadsOfLevel.Length; i++)
          {
                movingBreakPadsOfLevel[i]
                    .GetComponent<MovingBreakPad>().SetPeriod(MBPPeriodRandomizer());
                movingBreakPadsOfLevel[i]
                    .GetComponent<MovingBreakPad>().SetMoveRange(MBPRangeRandomizer());
                Instantiate(movingBreakPadsOfLevel[i],
                    new Vector3(0, 0, zPosDetecter + DistanceDetecter(movingBreakPadsOfLevel[i])),
                    Quaternion.identity);
                zPosDetecter += DistanceDetecter(movingBreakPadsOfLevel[i]);
          }
            finalPad = finalPadPrefabs[Randomizer(finalPadPrefabs)];
            Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
        }
        else
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

            finalPad = finalPadPrefabs[Randomizer(finalPadPrefabs)];
            Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
        }
    }
    private void SpawnerType2Execute()
    {
         if (breakPadAmount > movingBreakPadAmount)
         {
            int zPosDetecter = 0;
            int tempa = Mathf.FloorToInt(breakPadAmount / movingBreakPadAmount);
            int movingBreakPadIndex = 0;
            for (int i = 0; i < breakPadsOfLevel.Length; i++)
            {
                tempa--;
                
                Instantiate(breakPadsOfLevel[i],
                    new Vector3(0, 0, zPosDetecter + DistanceDetecter(breakPadsOfLevel[i])),
                    Quaternion.identity);
                zPosDetecter += DistanceDetecter(breakPadsOfLevel[i]);
                if (tempa == 0 && movingBreakPadIndex<movingBreakPadsOfLevel.Length)
                {
                    movingBreakPadsOfLevel[movingBreakPadIndex]
                        .GetComponent<MovingBreakPad>().SetPeriod(MBPPeriodRandomizer());
                    movingBreakPadsOfLevel[movingBreakPadIndex]
                        .GetComponent<MovingBreakPad>().SetMoveRange(MBPRangeRandomizer());
                    Instantiate(movingBreakPadsOfLevel[movingBreakPadIndex],
                        new Vector3(0, 0, zPosDetecter + DistanceDetecter(movingBreakPadsOfLevel[movingBreakPadIndex])),
                        Quaternion.identity);
                    zPosDetecter += DistanceDetecter(movingBreakPadsOfLevel[movingBreakPadIndex]);
                    movingBreakPadIndex++;
                    tempa = Mathf.FloorToInt(breakPadAmount / movingBreakPadAmount);
                }
            }
            finalPad = finalPadPrefabs[Randomizer(finalPadPrefabs)];
            Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
        }
        else if (breakPadAmount < movingBreakPadAmount)
        {
            int zPosDetecter = 0;
            int tempa = Mathf.FloorToInt(movingBreakPadAmount / breakPadAmount);
            int breakPadIndex = 0;
            for (int i = 0; i < movingBreakPadsOfLevel.Length; i++)
            {
                if (i < breakPadAmount)
                {
                    tempa--;
                }
                movingBreakPadsOfLevel[i]
                    .GetComponent<MovingBreakPad>().SetPeriod(MBPPeriodRandomizer());
                movingBreakPadsOfLevel[i]
                    .GetComponent<MovingBreakPad>().SetMoveRange(MBPRangeRandomizer());
                Instantiate(movingBreakPadsOfLevel[i],
                    new Vector3(0, 0, zPosDetecter + DistanceDetecter(movingBreakPadsOfLevel[i])),
                    Quaternion.identity);
                zPosDetecter += DistanceDetecter(movingBreakPadsOfLevel[i]);
                if (tempa == 0 && breakPadIndex<breakPadsOfLevel.Length)
                {
                    Instantiate(breakPadsOfLevel[breakPadIndex],
                        new Vector3(0, 0, zPosDetecter + DistanceDetecter(breakPadsOfLevel[breakPadIndex])),
                        Quaternion.identity);
                    zPosDetecter += DistanceDetecter(breakPadsOfLevel[breakPadIndex]);
                    breakPadIndex++;
                    tempa = Mathf.FloorToInt(movingBreakPadAmount / breakPadAmount);
                }
            }
            finalPad = finalPadPrefabs[Randomizer(finalPadPrefabs)];
            Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
        }
         else
         {
             int zPosDetecter = 0;
             int breakPadIndex = 0;
             for (int i = 0; i < movingBreakPadsOfLevel.Length; i++)
             {
                 movingBreakPadsOfLevel[i]
                     .GetComponent<MovingBreakPad>().SetPeriod(MBPPeriodRandomizer());
                 movingBreakPadsOfLevel[i]
                     .GetComponent<MovingBreakPad>().SetMoveRange(MBPRangeRandomizer());
                 Instantiate(movingBreakPadsOfLevel[i],
                     new Vector3(0, 0, zPosDetecter + DistanceDetecter(movingBreakPadsOfLevel[i])),
                     Quaternion.identity);
                 zPosDetecter += DistanceDetecter(movingBreakPadsOfLevel[i]);

                 Instantiate(breakPadsOfLevel[i],
                     new Vector3(0, 0, zPosDetecter + DistanceDetecter(breakPadsOfLevel[i])),
                     Quaternion.identity);
                 zPosDetecter += DistanceDetecter(breakPadsOfLevel[i]);
             }

             finalPad = finalPadPrefabs[Randomizer(finalPadPrefabs)];
             Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
         }
    }
    private void SpawnerType3Execute()
    {
        if(platformAmount>breakPadAmount)
        {
            
            int zPosDetecter = 0;
            int xPosDetecter = 1;
            int tempa = Mathf.FloorToInt(platformAmount / breakPadAmount);
            int breakPadIndex = 0;
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
                    Instantiate(breakPadsOfLevel[breakPadIndex],
                        new Vector3(3 * xPosDetecter, 0, zPosDetecter + DistanceDetecter(breakPadsOfLevel[breakPadIndex])),
                        Quaternion.identity);
                    zPosDetecter += DistanceDetecter(breakPadsOfLevel[breakPadIndex]);
                    breakPadIndex++;
                    tempa = Mathf.FloorToInt(platformAmount / breakPadAmount);
                } 
            }
            finalPad = finalPadPrefabs[Randomizer(finalPadPrefabs)];
            Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
        } 
        else if(platformAmount<breakPadAmount)
        {
            int zPosDetecter = 0;
            int xPosDetecter = 0;
            int tempa = Mathf.FloorToInt(breakPadAmount / platformAmount);
            int platformIndex = 0;
            for (int i = 0; i < breakPadsOfLevel.Length; i++)
            {
                tempa--;
                Instantiate(breakPadsOfLevel[i],
                    new Vector3(3 * xPosDetecter, 0, zPosDetecter + DistanceDetecter(breakPadsOfLevel[i])),
                    Quaternion.identity);
                zPosDetecter += DistanceDetecter(breakPadsOfLevel[i]);
                if (xPosDetecter == 0)
                {
                    xPosDetecter = 1;
                    tempa = 0;
                }
                if (tempa == 0 && platformIndex<platformsOfLevel.Length) // if in içindeki kısmı bütün spanwnlara ekleyelim
                {
                    Instantiate(platformsOfLevel[platformIndex],
                        new Vector3(10 * xPosDetecter, -10, zPosDetecter + DistanceDetecter(platformsOfLevel[platformIndex])),
                        Quaternion.identity);
                    platformsOfLevel[platformIndex].GetComponent<PlatformMoveHandler>().SetMoveSpeed(PlatformSpeedRandomizer());
                    platformsOfLevel[platformIndex].GetComponent<PlatformMoveHandler>().SetScaleX(PlatformScaleXRandomizer());
                    zPosDetecter += DistanceDetecter(platformsOfLevel[platformIndex]);
                    xPosDetecter *= -1;
                    tempa = Mathf.FloorToInt(breakPadAmount / platformAmount);
                    platformIndex++;
                } 
            }
            finalPad = finalPadPrefabs[Randomizer(finalPadPrefabs)];
            Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
        }
        else
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
               
                Instantiate(breakPadsOfLevel[i], new Vector3(3 * xPosDetecter, 0, zPosDetecter + DistanceDetecter(breakPadsOfLevel[i])),
                    Quaternion.identity);
                zPosDetecter += DistanceDetecter(breakPadsOfLevel[i]); 
            }
            finalPad = finalPadPrefabs[Randomizer(finalPadPrefabs)];
            Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
        }

        
    }
    private void SpawnerType4Execute() // burda yapılacaklar var 
    {
        int zPosDetecter = 0;
        int xPosDetecter = 1;
        int tempa = Mathf.FloorToInt(platformAmount / movingBreakPadAmount);
        int movingBreakPadIndex = 0;
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
                movingBreakPadsOfLevel[movingBreakPadIndex].GetComponent<MovingBreakPad>().SetPeriod(MBPPeriodRandomizer());
                movingBreakPadsOfLevel[movingBreakPadIndex].GetComponent<MovingBreakPad>().SetMoveRange(MBPRangeRandomizer());
                Instantiate(movingBreakPadsOfLevel[movingBreakPadIndex],
                    new Vector3(3*xPosDetecter, 0, zPosDetecter + DistanceDetecter(movingBreakPadsOfLevel[movingBreakPadIndex])),
                    Quaternion.identity);
                zPosDetecter += DistanceDetecter(movingBreakPadsOfLevel[movingBreakPadIndex]);
                movingBreakPadIndex++;
                tempa=Mathf.FloorToInt(platformAmount / movingBreakPadAmount);
            }
        }

        finalPad = finalPadPrefabs[Randomizer(finalPadPrefabs)];
        Instantiate(finalPad, new Vector3(0, 0, zPosDetecter + DistanceDetecter(finalPad)), Quaternion.identity);
    }
    private void SpawnerType5Execute()
    {
       // do sth
    }
    
    

    private int DistanceDetecter(GameObject platform)
    {
        int value = 0;
        if (platform.gameObject.CompareTag("BluePlatform") || platform.gameObject.CompareTag("BlueFinalPad")|| 
            platform.gameObject.CompareTag("BlueBreakPad") || platform.gameObject.CompareTag("BlueMovingBreakPad"))
        {
            value = 4;
        }

        if (platform.gameObject.CompareTag("GreenPlatform") || platform.gameObject.CompareTag("GreenFinalPad")|| 
            platform.gameObject.CompareTag("GreenBreakPad") || platform.gameObject.CompareTag("GreenMovingBreakPad"))
        {
            value = 3;
        }

        if (platform.gameObject.CompareTag("YellowPlatform") || platform.gameObject.CompareTag("YellowFinalPad")|| 
            platform.gameObject.CompareTag("YellowBreakPad") || platform.gameObject.CompareTag("YellowMovingBreakPad"))
        {
            value = 2;
        }

        return value;
    }
    
    private int Randomizer(GameObject[] prefabs)
    {
        return Random.Range(0, prefabs.Length);
    }
   
    private float PlatformSpeedRandomizer()
    {
        return Random.Range(minSpeed, maxSpeed);
    }
    
    private float MBPPeriodRandomizer()
    {
        return Random.Range(minPeriodOfMBP, maxPeriodOfMBP);
    }

    private float PlatformScaleXRandomizer()
    {
        return Random.Range(minScaleX, maxScaleX);
    }

    private float MBPRangeRandomizer()
    {
        return Random.Range(minMoveRangeOfMBP, maxMoveRangeOfMBP);
    }

    private void SpawnerTypeDecider()
    {
        if ((breakPadAmount > 0 && platformAmount <=0 && movingBreakPadAmount <= 0) || 
        (movingBreakPadAmount > 0&&breakPadAmount <= 0 && platformAmount <= 0 ) || 
        (platformAmount > 0 && breakPadAmount <= 0 && movingBreakPadAmount <=0)) 
        {
            spawnerType1 = true;
        }
        else if (breakPadAmount > 0 && movingBreakPadAmount > 0 && platformAmount <=0) 
        {
            spawnerType2 = true;
        }
        else if (platformAmount > 0 && breakPadAmount > 0 && movingBreakPadAmount <= 0)
        {
            spawnerType3 = true;
        }
        else if (platformAmount > 0 && movingBreakPadAmount > 0 && breakPadAmount <= 0) // eksik ne varsa yapalım
        {
            spawnerType4 = true;
        }
        else if (platformAmount > 0 && movingBreakPadAmount > 0 && breakPadAmount > 0)
        {
            spawnerType5 = true;
        }
    }
}
