using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    private GameObject player;
    
   
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CollectableHandler collectableHandler = FindObjectOfType<CollectableHandler>();
            if (collectableHandler.GetComponent<CollectableHandler>().childDetecter < 0)
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
    
}
