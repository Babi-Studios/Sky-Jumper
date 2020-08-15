using UnityEngine;

public class BreakPad : MonoBehaviour
{
    GameObject player;
    
    public float visibilityDistance = 4.5f;
    private Vector3 initialPos;

    public float collectablePeriod;
    public float collectableRange;
    public float obstaclePeriod;
    public float obstacleRange;

    public float collectableCreatePerc;
    

    [SerializeField] GameObject collectable;
    
    void Start()
    {
        CollectableOrObstacleDecider();
        player = GameObject.Find("Player");
        initialPos = transform.position;
        if (transform.Find("Collectable") != null)
        {
            transform.Find("Collectable").GetComponent<BreakPadCollectable>().SetVisibilityDistance(visibilityDistance);
            transform.Find("Collectable").GetComponent<BreakPadCollectable>().SetCollectablePeriod(collectablePeriod);
            transform.Find("Collectable").GetComponent<BreakPadCollectable>().SetCollectableRange(collectableRange);
            
        }

        if (transform.Find("Obstacle")!=null)
        {
            transform.Find("Obstacle").GetComponent<BreakPadObstacle>().SetVisibilityDistance(visibilityDistance);
            transform.Find("Obstacle").GetComponent<BreakPadObstacle>().SetObstaclePeriod(obstaclePeriod);
            transform.Find("Obstacle").GetComponent<BreakPadObstacle>().SetObstacleRange(obstacleRange);
            
        }

    }

    void Update()
    {
        PlayerPositionFollower();
        
        GameObjectDestoyer();
    }
  
    private void GameObjectDestoyer()
    {
        if (transform.position.z-player.transform.position.z<=-0.5)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().enabled = false;
            } 
        }
    }

    private void PlayerPositionFollower()
    {
        if (transform.position.z-player.transform.position.z>visibilityDistance)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().enabled = false;
            }
        }
        else 
        { 
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().enabled = true;
            }
        }
       
    }

    private void CollectableOrObstacleDecider()
    {

        if (transform.Find("Obstacle") != null && transform.Find("Collectable") != null)
        {
            int a = Random.Range(0, 101);
            if (a < collectableCreatePerc)
            {
                if (transform.Find("Obstacle") != null)
                {
                    Destroy(transform.Find("Obstacle").gameObject);
                }
            }
            else
            {
                if (transform.Find("Collectable") != null)
                {
                    Destroy(transform.Find("Collectable").gameObject);
                }
            }
        }
    }

    public void SetCollectablePeriod(float colPeriod)
    {
        collectablePeriod = colPeriod;
    }
    public void SetCollectableRange(float colRange)
    {
        collectableRange = colRange;
    }
    public void SetObstaclePeriod(float colPeriod)
    {
        obstaclePeriod = colPeriod;
    }
    public void SetObstacleRange(float colRange)
    {
        obstacleRange = colRange;
    }

    public void SetCollectableCreatePercentage(float perc)
    {
        collectableCreatePerc = perc;
    }
    
    
    
}
