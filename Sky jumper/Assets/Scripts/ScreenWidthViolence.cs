using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWidthViolence : MonoBehaviour
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
            player.GetComponent<Animator>().SetBool("readyForJump", false);
            player.GetComponent<Animator>().SetBool("jumped", true);
            player.GetComponent<PlayerController>().FollowParabolaOnFalling();
        }
    }
}
