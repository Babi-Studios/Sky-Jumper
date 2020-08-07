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
            GetComponentInChildren<Canvas>().enabled = false;
            foreach (Transform child in transform)
            {
                if(!child.CompareTag("FinalPadCanvas"))
                {child.GetComponent<Renderer>().enabled = false;}
            }
        }
        else 
        { 
            GetComponentInChildren<Canvas>().enabled = true;
            foreach (Transform child in transform)
            {
                if(!child.CompareTag("FinalPadCanvas"))
                {child.GetComponent<Renderer>().enabled = true;}
            }
        }
       
    }
}
