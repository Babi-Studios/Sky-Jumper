using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CollectableHandler : MonoBehaviour
{
    public float xPos, yPos, zPos;
    public int childDetecter = -1;
    [SerializeField] GameObject collectable;
    
    private void Start()
    {
        xPos = -5;
        yPos = 9;
    }

    public void AddCollectableToShelf()
    {
        childDetecter += 1;
        xPos += 1;
        Instantiate(collectable, new Vector3(xPos, yPos, transform.position.z), Quaternion.Euler(-20,0,0), gameObject.transform);
        if (xPos == 4)
        {
            xPos =- 5;
            yPos++;
        }
    }

    public void DestroyLastCollectable()
    {
        transform.GetChild(childDetecter).GetComponent<ShelfCollectable>().DestroyMe();
        childDetecter -= 1;
        xPos -= 1;
    }

    public void AllignCollectables()
    {
        int a = 0;
        int childIndex = 0;
        float additionalTime = 0f;
        foreach (var collectable in transform)
        {
            transform.GetChild(childIndex).GetComponent<ShelfCollectable>().SetLevelEndPosition(4+a,2+additionalTime);
            childIndex += 1;
            a+=2;
            additionalTime += 0.2f;
        }
    }
 }
