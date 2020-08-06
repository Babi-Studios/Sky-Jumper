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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isPlayerOnPlatform = false;
        }
        
        MoveCycle();

        PlayerPositionFollower();

        GameObjectDestoyer();

        if (isPlayerOnPlatform)
        {
            
        }
    }

    private void MoveCycle()
    {
        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(tau * cycles);
        movementFactor = rawSinWave;
        Vector3 offset = movementFactor * Vector3.right * moveRange;
        transform.position = startingPos + offset;
        if (isPlayerOnPlatform)
        {
            player.transform.position = transform.position +
                                        new Vector3(player.GetComponent<PlayerController>().offsetToMBP, player.transform.position.y, 0);
        }
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
