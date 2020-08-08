using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }
}
