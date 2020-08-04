﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    public Slider jumpSlider;

    public GameObject[] parabolaRoots;
    private ParabolaController parabolaControllerScript;

    public float maxInputTime;
    public float holdTimer;

    public bool badTiming;
    public bool idleTiming;

    public float grayFillAreaMultiplier;
    public float colorsFillAreaMultiplier;
    public float redFillAreaMultiplier;
    private float oneUnitForColorChange;
    // Start is called before the first frame update
    void Start()
    {
        parabolaControllerScript = GetComponent<ParabolaController>();
        
        jumpSlider.maxValue = maxInputTime;
        jumpSlider.value = 0;
        jumpSlider.fillRect.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        oneUnitForColorChange = 1/(4*grayFillAreaMultiplier + 3*colorsFillAreaMultiplier + redFillAreaMultiplier);
        
    }

    // Update is called once per frame
    void Update()
    {
        JumpHandler();
    }

    private void JumpHandler()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpSlider.fillRect.gameObject.SetActive(true);
            anim.SetBool("toIdle",false);
            anim.SetBool("jumped",false);
            anim.SetBool("readyForJump",true);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            holdTimer += Time.deltaTime;
            jumpSlider.value += Time.deltaTime;
            if (jumpSlider.value <= maxInputTime*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.gray;
                badTiming = true;
                SetParabolaRoots(0.5f,1);
            }
            else if (jumpSlider.value <= maxInputTime*(grayFillAreaMultiplier+colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.yellow; 
                badTiming = false;
                SetParabolaRoots(1,2);
            }
            else if (jumpSlider.value <= maxInputTime*(2*grayFillAreaMultiplier+colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.gray;
                badTiming = true;
                SetParabolaRoots(0.5f,1);
            }
            else if (jumpSlider.value <= maxInputTime*(2*grayFillAreaMultiplier+2*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.green;
                badTiming = false;
                SetParabolaRoots(1.5f,3);
            }
            else if (jumpSlider.value <= maxInputTime*(3*grayFillAreaMultiplier+2*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.gray;
                badTiming = true;
                SetParabolaRoots(0.5f,1);
            }
            else if (jumpSlider.value <= maxInputTime*(3*grayFillAreaMultiplier+3*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.blue;
                badTiming = false;
                SetParabolaRoots(2,4);
            }
            else if (jumpSlider.value<= maxInputTime*(4*grayFillAreaMultiplier+3*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.gray;
                badTiming = true;
                SetParabolaRoots(0.5f,1);
            }
            else if (jumpSlider.value<= maxInputTime*(4*grayFillAreaMultiplier+3*colorsFillAreaMultiplier+redFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.red;
                idleTiming = true;
                badTiming = false;
                
            }
            else
            {
                idleTiming = false;
                badTiming = true;
                jumpSlider.fillRect.gameObject.SetActive(false);
                anim.SetTrigger("overtiming");
                SetParabolaRoots(0.5f,1);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (badTiming)
            {
                anim.SetBool("readyForJump",false);
                anim.SetTrigger("badtiming");
                parabolaControllerScript.FollowParabola();
            }
            else if (idleTiming)
            {
                anim.SetBool("readyForJump",false);
                anim.SetBool("toIdle",true);
            }
            else if (!idleTiming && !badTiming)
            {
                anim.SetBool("readyForJump",false);
                anim.SetBool("jumped",true);
                parabolaControllerScript.FollowParabola();
            }
            jumpSlider.fillRect.gameObject.SetActive(false);
            jumpSlider.value = 0;
            holdTimer += 0;
        }
    }

    private void SetParabolaRoots(float height, float forwardEndPoint)
    {
        parabolaRoots[1].transform.localPosition=new Vector3(0,height,forwardEndPoint*0.6f);
        parabolaRoots[2].transform.localPosition=new Vector3(0,parabolaRoots[2].transform.localPosition.y,forwardEndPoint);
    }
}
