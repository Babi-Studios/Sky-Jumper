using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPad : MonoBehaviour
{
    GameObject player;
    
    public float visibilityDistance = 4.5f;
    
    private bool isPlayerOnPlatform;
   
    void Start()
    {
        player = GameObject.Find("Player");
      
    }

    void Update()
    {
        PlayerPositionFollower();
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
}
