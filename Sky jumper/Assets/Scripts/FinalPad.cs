using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPad : MonoBehaviour
{
    GameObject player;

    [SerializeField] GameObject collectablePlane;
    
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

    public void CreatePlaneForCollectables()
    {
        Instantiate(collectablePlane, new Vector3(0,0 , transform.position.z+17), Quaternion.identity);
    }
}
