using UnityEngine;

public class MovingBreakPad : MonoBehaviour
{
    GameObject player;
    public float visibilityDistance = 4.5f;
    private bool isPlayerOnPlatform;

    private float tau = Mathf.PI * 2f;

    public float movementFactor;
    public float period;
    public Vector3 startingPos;

    public float moveRange;

    public float collectableCreatePerc;

    // Start is called before the first frame update
    void Start()
    {
        CollectableOrObstacleDecider();
        player = GameObject.Find("Player");
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
     
        MoveCycle();

        PlayerPositionFollower();

        GameObjectDestoyer();

    }

    private void MoveCycle()
    {
        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(tau * cycles);
        movementFactor = rawSinWave;
        Vector3 offset = movementFactor * Vector3.right * moveRange;
        transform.position = startingPos + offset;
    }

    private void GameObjectDestoyer()
    {
        if (transform.position.z - player.transform.position.z <= -0.5)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().enabled = false;
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

    
    public void SetCollectableCreatePercentage(float perc)
    {
        collectableCreatePerc = perc;
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

        if (transform.position.z - player.transform.position.z <= Mathf.Epsilon)
        {
            isPlayerOnPlatform = true;
        }
        else
        {
            isPlayerOnPlatform = false;
        }
    }
    
    public void SetPeriod(float periodofMBP)
    {
        period = periodofMBP;
    }

    public void SetMoveRange(float range)
    {
        moveRange = range;
    }
}
