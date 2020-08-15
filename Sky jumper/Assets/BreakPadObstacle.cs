using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakPadObstacle : MonoBehaviour
{
    
    private GameObject player;
    
    private float tau = Mathf.PI * 2f;

    private float movementFactor;
    public float period;
    public Vector3 startingPos;
    public float visibilityDistance;

    private bool isPlayerOnPlatform;

    private MeshRenderer mRenderer;

    private float minPeriod = 10;
    private float maxPeriod=5;
    private float minMoveRange=-3;
    private float maxMoveRange=3;

    private float moveRange;
   
    private void Start()
    {
        
        player = GameObject.Find("Player");
        startingPos = transform.position;
        mRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(!isPlayerOnPlatform)
        {MoveCycle();}
        
        PlayerPositionFollower();
    }

    private void MoveCycle()
    {
        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(tau * cycles);
        movementFactor = rawSinWave;
        Vector3 offset = movementFactor * Vector3.right * moveRange;
        transform.position = startingPos + offset;
    }
    
    private void PlayerPositionFollower()
    {
        if (transform.position.z-player.transform.position.z>visibilityDistance)
        {
            mRenderer.enabled = false;
        }
        else if(transform.position.z-player.transform.position.z<visibilityDistance && transform.position.z-player.transform.position.z >= -0.1)
        {
            mRenderer.enabled = true;
        }
        else
        {
            mRenderer.enabled = false;
        }

        if (transform.position.z - player.transform.position.z <= Mathf.Epsilon)
        {
            isPlayerOnPlatform = true;
        }
        else
        {
            isPlayerOnPlatform = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CollectableHandler collectableHandler = FindObjectOfType<CollectableHandler>();
            if (collectableHandler.GetComponent<CollectableHandler>().childDetecter < 0)// buraya bakman lazım
            {
                other.GetComponent<PlayerController>().FollowParabolaOnFalling();
            }
            else
            {
                float xPosForAnimation = FindObjectOfType<CollectableHandler>().GetComponent<CollectableHandler>().xPos;
                LeanTween.move(this.gameObject, new Vector3(xPosForAnimation, 9, other.transform.position.z), 1f);
                Invoke("ObstacleDestroyer",1f);
            }
        }
    }

    private void ObstacleDestroyer()
    {
        Destroy(gameObject);
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.GetComponent<ScoreManager>().DecreaseCollectableScore();
        CollectableHandler collectableHandler = FindObjectOfType<CollectableHandler>();
        collectableHandler.GetComponent<CollectableHandler>().DestroyLastCollectable();
    }

    public void SetVisibilityDistance(float visDistance)
    {
        visibilityDistance = visDistance;
    }
    
    public void SetObstaclePeriod(float colPeriod)
    {
        period = colPeriod;
    }
    
    public void SetObstacleRange(float range)
    {
        moveRange = range;
    }
    
}
