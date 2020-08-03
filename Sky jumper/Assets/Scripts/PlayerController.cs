using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    public Slider jumpSlider;

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
            }
            else if (jumpSlider.value <= maxInputTime*(grayFillAreaMultiplier+colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.yellow; 
                badTiming = false;
            }
            else if (jumpSlider.value <= maxInputTime*(2*grayFillAreaMultiplier+colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.gray;
                badTiming = true;
            }
            else if (jumpSlider.value <= maxInputTime*(2*grayFillAreaMultiplier+2*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.green;
                badTiming = false;
            }
            else if (jumpSlider.value <= maxInputTime*(3*grayFillAreaMultiplier+2*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.gray;
                badTiming = true;
            }
            else if (jumpSlider.value <= maxInputTime*(3*grayFillAreaMultiplier+3*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.blue;
                badTiming = false;
            }
            else if (jumpSlider.value<= maxInputTime*(4*grayFillAreaMultiplier+3*colorsFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color=Color.gray;
                badTiming = true;
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
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (badTiming)
            {
                anim.SetBool("readyForJump",false);
                anim.SetTrigger("badtiming");
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
            }
            
            jumpSlider.fillRect.gameObject.SetActive(false);
            jumpSlider.value = 0;
            holdTimer += 0;
        }
    }
}
