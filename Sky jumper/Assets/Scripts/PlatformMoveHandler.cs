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
    Renderer childRenderer;

    public float moveSpeed;

    public float collectableCreatePerc;
    // Start is called before the first frame update
    void Start()
    {
        CollectableOrObstacleDecider();
        
        player = GameObject.Find("Player");
        platformRenderer = GetComponent<Renderer>();
        childRenderer = GetComponentInChildren<Renderer>();
        childRenderer.enabled = false;
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
        
        SpeedHandler();
        
        GameObjectDestoyer();
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

    private void SpeedHandler()
    {
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
    }

    public void SetCollectableCreatePercentage(float perc)
    {
        collectableCreatePerc = perc;
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
            transform.Find("Cube").GetComponent<Renderer>().enabled = false;
            if (transform.Find("Collectable") != null)
            {
                transform.Find("Collectable").GetComponent<MeshRenderer>().enabled = false;
            }

            if (transform.Find("Obstacle") != null)
            {
                transform.Find("Obstacle").GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            platformRenderer.enabled = true;
            transform.Find("Cube").GetComponent<Renderer>().enabled = true;
            if (transform.Find("Collectable") != null)
            {
                transform.Find("Collectable").GetComponent<MeshRenderer>().enabled = true;
            } 
            if (transform.Find("Obstacle") != null)
            {
                transform.Find("Obstacle").GetComponent<MeshRenderer>().enabled = true;
            }// bunları eger null degılse diye değiştir.
        }

        if (transform.position.z - player.transform.position.z <= Mathf.Epsilon)
        {
            isPlayerOnPlatform = true;
            
            if (!isPlayerExactlyOnThisPlatform)
            { 
                 platformRenderer.enabled = false;
                 transform.Find("Cube").GetComponent<Renderer>().enabled = false;
                 if (transform.Find("Collectable") != null)
                 {
                     transform.Find("Collectable").GetComponent<MeshRenderer>().enabled = false;
                 }
                 if (transform.Find("Obstacle") != null)
                 {
                     transform.Find("Obstacle").GetComponent<MeshRenderer>().enabled = false;
                 }
            }
        }
        else
        {
            isPlayerOnPlatform = false;
        }
    }
}
