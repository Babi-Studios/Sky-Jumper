using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    public GameObject fireworkParticle;

    public Slider jumpSlider;

    public GameObject[] parabolaRoots;
    private ParabolaController parabolaControllerScript;

    public float maxInputTime;
    
    public bool enableForJumping;
    
    private bool isOnPlatform = true;
    private bool isOnFinalPad = false;
    private float moveXSpeed;

    public float grayFillAreaMultiplier;

    public float colorsFillAreaMultiplier;

    //public float redFillAreaMultiplier;
    private float oneUnitForColorChange;

    // Start is called before the first frame update
    void Start()
    {
        parabolaControllerScript = GetComponent<ParabolaController>();
       
        jumpSlider.maxValue = maxInputTime;
        jumpSlider.value = 0;
        jumpSlider.fillRect.gameObject.SetActive(false);
        jumpSlider.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        oneUnitForColorChange = 1 / (4 * grayFillAreaMultiplier + 3 * colorsFillAreaMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnPlatform && !isOnFinalPad)
        {
            JumpHandler();
            MoveWithPlatform();
        }
    }

    private void JumpHandler()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpSlider.gameObject.SetActive(true);
            jumpSlider.fillRect.gameObject.SetActive(true);
            anim.SetBool("toIdle", false);
            anim.SetBool("readyForJump", true);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            jumpSlider.value += Time.deltaTime;
            if (jumpSlider.value <= maxInputTime * oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.gray;
                enableForJumping = false;
                SetParabolaRoots(0.5f, 1);
            }
            else if (jumpSlider.value <= maxInputTime * (grayFillAreaMultiplier + colorsFillAreaMultiplier) *
                oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow;
                enableForJumping = true;
                SetParabolaRoots(1, 2);
            }
            else if (jumpSlider.value <= maxInputTime * (2 * grayFillAreaMultiplier + colorsFillAreaMultiplier) *
                oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.gray;
                enableForJumping = false;
                SetParabolaRoots(1.25f, 2.5f);
            }
            else if (jumpSlider.value <= maxInputTime * (2 * grayFillAreaMultiplier + 2 * colorsFillAreaMultiplier) *
                oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color =
                    Color.green;
                enableForJumping = true;
                SetParabolaRoots(1.5f, 3);
            }
            else if (jumpSlider.value <= maxInputTime * (3 * grayFillAreaMultiplier + 2 * colorsFillAreaMultiplier) *
                oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.gray;
                enableForJumping = false;
                SetParabolaRoots(1.5f, 3.5f);
            }
            else if (jumpSlider.value <= maxInputTime * (3 * grayFillAreaMultiplier + 3 * colorsFillAreaMultiplier) *
                oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.blue;
                enableForJumping = true;
                SetParabolaRoots(2, 4);
            }
            else if (jumpSlider.value - maxInputTime * (4 * grayFillAreaMultiplier + 3 * colorsFillAreaMultiplier) *
                oneUnitForColorChange <= Mathf.Epsilon)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.gray;
                enableForJumping = false;
                SetParabolaRoots(2.25f, 4.5f);
            }
            else
            {
                enableForJumping = false;
                jumpSlider.fillRect.gameObject.SetActive(false);
                SetParabolaRoots(0.5f, 1);
                parabolaControllerScript.FollowParabola();
                anim.SetBool("jumped", true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("readyForJump", false);
            anim.SetBool("jumped", true);
            parabolaControllerScript.FollowParabola();
            jumpSlider.fillRect.gameObject.SetActive(false);
            jumpSlider.gameObject.SetActive(false);
            jumpSlider.value = 0;
            isOnPlatform = false;
        }
    }


private void SetParabolaRoots(float height, float forwardEndPoint)
    {
        parabolaRoots[1].transform.localPosition=new Vector3(0,height,forwardEndPoint*0.6f);
        parabolaRoots[2].transform.localPosition=new Vector3(0,0,forwardEndPoint);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BluePlatform") || other.gameObject.CompareTag("GreenPlatform") ||
            other.gameObject.CompareTag("YellowPlatform"))
        {
            anim.SetBool("jumped",false);
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
            other.gameObject.GetComponent<PlatformMoveHandler>().isPlayerExactlyOnThisPlatform = true;
        }
        
        if (other.gameObject.CompareTag("BlueFinalPad") || other.gameObject.CompareTag("GreenFinalPad") ||
            other.gameObject.CompareTag("YellowFinalPad"))
        {
            isOnFinalPad = true;
            isOnPlatform = true;
            anim.SetTrigger("dancing");
            
            GameObject firework = Instantiate(fireworkParticle, new Vector3(transform.position.x+1,transform.position.y,transform.position.z), Quaternion.identity);
            GameObject firework2 = Instantiate(fireworkParticle, new Vector3(transform.position.x-1,transform.position.y,transform.position.z), Quaternion.identity);
            firework.GetComponent<ParticleSystem>().Play();
            firework2.GetComponent<ParticleSystem>().Play();
            
            
        }
    }

    private void MoveWithPlatform()
    {
        transform.Translate(Vector3.right * moveXSpeed*Time.deltaTime/2);
    }

    public void FollowParabolaOnFalling()
    {
        isOnPlatform = false;
        SetParabolaRoots(0.5f,1);
        parabolaControllerScript.FollowParabola();
    }

    
}
