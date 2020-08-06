using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPad : MonoBehaviour
{
    GameObject player;
    
    public float visibilityDistance = 4.5f;
    
    void Start()
    {
        player = GameObject.Find("Player");
      
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
}
