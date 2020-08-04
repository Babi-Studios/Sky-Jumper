using UnityEngine;

public class PlatformMoveHandler : MonoBehaviour
{
    public bool isLeft;
    GameObject player;
    Vector3 initialPos;

    public float visibilityDistance = 4.5f;
    
    private bool isPlayerOnPlatform;
    public bool isPlayerExactlyOnThisPlatform;

    Renderer platformRenderer;

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        platformRenderer = GetComponent<Renderer>();
        initialPos = transform.position;
        
        if (transform.position.x < 0)
        {
            isLeft = true;
        }
        else
        {
            isLeft = false;
        }
        Invoke("Instantiater",InvokeTimeDetecter());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPositionFollower();
        
        if (!isLeft)
        {
            if (isPlayerOnPlatform)
            {
                transform.Translate(Vector3.right * (-1) * Time.deltaTime * moveSpeed/2);
            }
            else
            {
                transform.Translate(Vector3.right * (-1) * Time.deltaTime * moveSpeed);
            }
           
        }
        else
        {
            if (isPlayerOnPlatform)
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed/2);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            }
            
        }
        GameObjectDestoyer();
    }

    public void SetMoveSpeed(float platformSpeed)
    {
        moveSpeed = platformSpeed;
    }
    
    public void SetScaleX(float platformScaleX)
    {
        transform.localScale = new Vector3(platformScaleX,transform.localScale.y,transform.localScale.z);
    }

    private void Instantiater()
    {
        Instantiate(gameObject, initialPos, Quaternion.identity);
    }

    private void GameObjectDestoyer()
    {
        if (transform.position.x >= 20 || transform.position.x <= -20)
        {
            Destroy(gameObject);
        }
    }

    private float InvokeTimeDetecter()
    {
       return Random.Range(2f, 3f);
    }

    private void PlayerPositionFollower()
    {
        if (transform.position.z - player.transform.position.z < -0.5)
        {
            isPlayerExactlyOnThisPlatform = false;
        }
        
        if (transform.position.z-player.transform.position.z>visibilityDistance)
        {
            platformRenderer.enabled = false;
        }
        else 
        {
            platformRenderer.enabled = true;
        }

        if (transform.position.z - player.transform.position.z <= Mathf.Epsilon)
        {
            isPlayerOnPlatform = true;
            
            if (!isPlayerExactlyOnThisPlatform)
            {
                platformRenderer.enabled = false;
            }
        }
        else
        {
            isPlayerOnPlatform = false;
        }

       
    }
}
