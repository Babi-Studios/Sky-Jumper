using System;
using System.Collections;
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

    private bool isOnPlatform;
    private float moveXSpeed;

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
                SetParabolaRoots(0.5f,1,0);
            }
            else if (jumpSlider.value <= maxInputTime*(grayFillAreaMultiplier+colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.yellow; 
                badTiming = false;
                SetParabolaRoots(1,2,0);
            }
            else if (jumpSlider.value <= maxInputTime*(2*grayFillAreaMultiplier+colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.gray;
                badTiming = true;
                SetParabolaRoots(0.5f,1,0);
            }
            else if (jumpSlider.value <= maxInputTime*(2*grayFillAreaMultiplier+2*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.green;
                badTiming = false;
                SetParabolaRoots(1.5f,3,0);
            }
            else if (jumpSlider.value <= maxInputTime*(3*grayFillAreaMultiplier+2*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.gray;
                badTiming = true;
                SetParabolaRoots(0.5f,1,0);
            }
            else if (jumpSlider.value <= maxInputTime*(3*grayFillAreaMultiplier+3*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.blue;
                badTiming = false;
                SetParabolaRoots(2,4,0);
            }
            else if (jumpSlider.value<= maxInputTime*(4*grayFillAreaMultiplier+3*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.gray;
                badTiming = true;
                SetParabolaRoots(0.5f,1,0);
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
                SetParabolaRoots(0.5f,1,0);
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
            isOnPlatform = false;
        }

        if (isOnPlatform)
        {
         MoveWithPlatform();   
        }
    }

    private void SetParabolaRoots(float height, float forwardEndPoint , float verticalEndPoint)
    {
        parabolaRoots[1].transform.localPosition=new Vector3(0,height,forwardEndPoint*0.6f);
        parabolaRoots[2].transform.localPosition=new Vector3(0,verticalEndPoint,forwardEndPoint);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BluePlatform") || other.gameObject.CompareTag("GreenPlatform") ||
            other.gameObject.CompareTag("YellowPlatform"))
        {
            anim.SetBool("toIdle",true);
            if (other.gameObject.GetComponent<PlatformMoveHandler>().isLeft)
            {
                moveXSpeed = other.gameObject.GetComponent<PlatformMoveHandler>().moveSpeed;    
            }
            else
            {
                moveXSpeed = - other.gameObject.GetComponent<PlatformMoveHandler>().moveSpeed;
            }
            isOnPlatform = true;
        }
        
        if (other.gameObject.CompareTag("BlueFinalPad") || other.gameObject.CompareTag("GreenFinalPad") ||
            other.gameObject.CompareTag("YellowFinalPad"))
        {
            anim.SetTrigger("dancing");
        }
    }

    private void MoveWithPlatform()
    {
        transform.Translate(Vector3.right * moveXSpeed*Time.deltaTime);
    }

    public void FollowParabolaOnFalling()
    {
        isOnPlatform = false;
        SetParabolaRoots(0.5f,1,0);
        parabolaControllerScript.Speed = 5;
        parabolaControllerScript.FollowParabola();
    }

}
