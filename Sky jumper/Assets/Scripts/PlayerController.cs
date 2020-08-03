using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    public Slider jumpSlider;

    public float maxInputTime;
    // Start is called before the first frame update
    void Start()
    {
        jumpSlider.maxValue = maxInputTime;
        jumpSlider.value = 0;
        jumpSlider.fillRect.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
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
            anim.SetBool("jumped",false);
            anim.SetBool("readyForJump",true);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            jumpSlider.value += Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("readyForJump",false);
            anim.SetBool("jumped",true);
            jumpSlider.fillRect.gameObject.SetActive(false);
        }
    }
}
