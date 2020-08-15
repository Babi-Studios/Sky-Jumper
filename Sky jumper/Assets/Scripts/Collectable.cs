using System;
using System.Security.Cryptography;
using UnityEngine;

public class Collectable : MonoBehaviour
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
            float xPosForAnimation = FindObjectOfType<CollectableHandler>().GetComponent<CollectableHandler>().xPos + 1;
            LeanTween.move(this.gameObject, new Vector3(xPosForAnimation, 9, other.transform.position.z), 1f);
            Invoke("CollectableDestroyer",1f);
        }
    }

    private void CollectableDestroyer()
    {
        Destroy(gameObject);
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.GetComponent<ScoreManager>().IncreaseCollectableScore();
        CollectableHandler collectableHandler = FindObjectOfType<CollectableHandler>();
        collectableHandler.GetComponent<CollectableHandler>().AddCollectableToShelf();
    }

    
}
