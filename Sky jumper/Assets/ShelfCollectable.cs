using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfCollectable : MonoBehaviour
{
    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void SetLevelEndPosition(float zPos, float time)
    {
        LeanTween.moveLocal(gameObject, new Vector3(0, 0, zPos), time);
        LeanTween.rotateLocal(gameObject, new Vector3(0, 0, 0), time);
    }
    
}
