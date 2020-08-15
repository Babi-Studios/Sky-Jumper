using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public Text comboText;
    private float comboTimer;
    private bool comboTime;
    
    private int colorindex = 0;
    GameObject target;
    float offsetToMBP;
    
    public Animator anim;

    public GameObject fireworkParticle;

    public Slider jumpSlider;
    private float jumpSliderMultiplier = 1;

    public GameObject[] parabolaRoots;
    private ParabolaController parabolaControllerScript;

    public float maxInputTime;
    
    private bool enableForJumping;
    
    private bool isOnPlatform = true;
    private bool isOnMovingBreakPad = false;
    private bool isOnFinalPad = false;
    private float moveXSpeed;

    public float sliderDirectionChanger;
    public float grayFillAreaMultiplier;
    public float colorsFillAreaMultiplier;
    
    private float oneUnitForColorChange;

   
    void Start()
    {
        SetParabolaRoots(0.5f, 1);
        
        comboText.GetComponent<Text>().enabled = false;
        parabolaControllerScript = GetComponent<ParabolaController>();
       
        jumpSlider.maxValue = maxInputTime;
        jumpSlider.value = 0;
        jumpSlider.fillRect.gameObject.SetActive(false);
        jumpSlider.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        oneUnitForColorChange = 1 / (2 * grayFillAreaMultiplier + 3 * colorsFillAreaMultiplier+2*sliderDirectionChanger);
    }

    void Update()
    {
       
        if (isOnPlatform && !isOnFinalPad)
        {
            JumpHandler();
            MoveWithPlatform();
        }

        if (isOnMovingBreakPad && !isOnFinalPad)
        {
            JumpHandler();
            MoveWithMovingBreakPad();
        }

        if (comboTime)
        {
            ComboRandomizer();
        }
        
    }

    private void JumpHandler()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            jumpSlider.gameObject.SetActive(true);
            jumpSlider.fillRect.gameObject.SetActive(true);
            anim.SetBool("toIdle", false);
            anim.SetBool("readyForJump", true);
        }
        else if (Input.GetKey(KeyCode.Space)||Input.GetMouseButton(0))
        {
            jumpSlider.value += Time.deltaTime*jumpSliderMultiplier;
            
            if (jumpSlider.value <= maxInputTime * oneUnitForColorChange*sliderDirectionChanger)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.gray;
                enableForJumping = false;
                SetParabolaRoots(0.5f, 1);
                colorindex = 0;
                if(jumpSliderMultiplier==-1)
                {jumpSliderMultiplier = 1;}
            } 
            else if (jumpSlider.value <= maxInputTime * (sliderDirectionChanger+grayFillAreaMultiplier)*oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.gray;
                enableForJumping = false;
                SetParabolaRoots(0.5f, 1);
                colorindex = 0;
            }
            else if (jumpSlider.value <= maxInputTime * (sliderDirectionChanger+grayFillAreaMultiplier + colorsFillAreaMultiplier) *
                oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red;
                enableForJumping = true;
                SetParabolaRoots(1, 2);
                colorindex = 1;
            }
            else if (jumpSlider.value <= maxInputTime * (sliderDirectionChanger+grayFillAreaMultiplier + 2 * colorsFillAreaMultiplier) *
                oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color =
                    Color.green;
                enableForJumping = true;
                SetParabolaRoots(1.5f, 3);
                colorindex = 2;
            }
            else if (jumpSlider.value <= maxInputTime * (sliderDirectionChanger+grayFillAreaMultiplier + 3 * colorsFillAreaMultiplier) *
                oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.blue;
                enableForJumping = true;
                SetParabolaRoots(2, 4);
                colorindex = 3;
            }
            else if (jumpSlider.value <= maxInputTime * (sliderDirectionChanger+2*grayFillAreaMultiplier + 3 * colorsFillAreaMultiplier) *
                oneUnitForColorChange)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.gray;
                enableForJumping = false;
                SetParabolaRoots(0.5f, 1);
                colorindex = 0;
                jumpSliderMultiplier *= -1;
            }
            else if(jumpSlider.value - maxInputTime * (2*sliderDirectionChanger+2 * grayFillAreaMultiplier + 3 * colorsFillAreaMultiplier) *
                oneUnitForColorChange <= Mathf.Epsilon)
            {
                jumpSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.gray;
                enableForJumping = false;
                SetParabolaRoots(0.5f, 1);
                colorindex = 0;
                jumpSliderMultiplier = -1;
                
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space)||Input.GetMouseButtonUp(0))
        {
            //ColorTeller(colorindex);
            anim.SetBool("readyForJump", false);
            anim.SetBool("jumped", true);
            parabolaControllerScript.FollowParabola();
            jumpSlider.fillRect.gameObject.SetActive(false);
            jumpSlider.gameObject.SetActive(false);
            jumpSlider.value = 0;
            isOnPlatform = false;
            isOnMovingBreakPad = false;
        }
    }


private void SetParabolaRoots(float height, float forwardEndPoint)
    {
        parabolaRoots[1].transform.localPosition=new Vector3(0,height,forwardEndPoint*0.6f);
        parabolaRoots[2].transform.localPosition=new Vector3(0,0.14f,forwardEndPoint);
    }

private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("childPlatform") || other.gameObject.CompareTag("childMBP"))
    {
        string[] comboTexts= new string[5];
        comboTexts[0] = "Nice Jump";
        comboTexts[1] = "Yes, One More!";
        comboTexts[2] = "Keep Going!";
        comboTexts[3] = "Good Timing!";
        comboTexts[4] = "Here, You Go!";
        int textRandomizer = Random.Range(0, comboTexts.Length);
        string selectedComboText = comboTexts[textRandomizer];
        comboText.GetComponent<Text>().text = selectedComboText;
        comboTime = true;
    }
}

private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BluePlatform") || other.gameObject.CompareTag("GreenPlatform") ||
            other.gameObject.CompareTag("YellowPlatform"))
        {
            transform.position=new Vector3(transform.position.x,transform.position.y,other.gameObject.transform.position.z);
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
        
        if (other.gameObject.CompareTag("BlueBreakPad") || other.gameObject.CompareTag("GreenBreakPad") ||
            other.gameObject.CompareTag("YellowBreakPad"))
        {
            transform.position=new Vector3(transform.position.x,transform.position.y,other.gameObject.transform.position.z);
            anim.SetBool("jumped",false);
            anim.SetBool("toIdle",true);
            moveXSpeed = 0;
            isOnPlatform = true;
        }
        
        if (other.gameObject.CompareTag("BlueMovingBreakPad") || other.gameObject.CompareTag("GreenMovingBreakPad") ||
            other.gameObject.CompareTag("YellowMovingBreakPad"))
        {
            target = other.gameObject;
            transform.position=new Vector3(transform.position.x,transform.position.y,other.gameObject.transform.position.z);
            offsetToMBP = transform.position.x - other.gameObject.transform.position.x;
            anim.SetBool("jumped",false);
            anim.SetBool("toIdle",true);
            moveXSpeed = 0;
            isOnMovingBreakPad = true;
        }
        
        if (other.gameObject.CompareTag("BlueFinalPad") || other.gameObject.CompareTag("GreenFinalPad") ||
            other.gameObject.CompareTag("YellowFinalPad"))
        {
            transform.position=new Vector3(transform.position.x,transform.position.y,other.gameObject.transform.position.z);
            isOnFinalPad = true;
            isOnPlatform = true;
            anim.SetTrigger("dancing");
            
            GameObject firework = Instantiate(fireworkParticle, new Vector3(transform.position.x+1,transform.position.y,transform.position.z), Quaternion.identity);
            GameObject firework2 = Instantiate(fireworkParticle, new Vector3(transform.position.x-1,transform.position.y,transform.position.z), Quaternion.identity);
            firework.GetComponent<ParticleSystem>().Play();
            firework2.GetComponent<ParticleSystem>().Play();
            
            CollectableHandler collectableHandler = FindObjectOfType<CollectableHandler>();
            collectableHandler.GetComponent<CollectableHandler>().AllignCollectables();
            
            other.gameObject.GetComponent<FinalPad>().CreatePlaneForCollectables();
        }
    }

    private void MoveWithPlatform()
    {
        transform.Translate(Vector3.right * moveXSpeed*Time.deltaTime/2);
    }

    private void MoveWithMovingBreakPad()
    {
        transform.position = target.transform.position + new Vector3(offsetToMBP, transform.position.y, 0);
    }

    public void FollowParabolaOnFalling()
    {
        //isOnPlatform = false;
        SetParabolaRoots(0.5f,1f);
        parabolaControllerScript.FollowParabola();
    }

   /* private void ColorTeller(int color)
    {
        if (color == 1)
        {
            Debug.Log("Red");
        }
        else if (color == 2)
        {
            Debug.Log("green");
        }
        else if (color == 3)
        {
            Debug.Log("blue");
        }
        else
        {
            Debug.Log("gray");
        }
    }*/

    private void ComboRandomizer()
    {
        comboTimer += Time.deltaTime;
        comboText.GetComponent<Text>().enabled = true;
        if (comboTimer >= 1f)
        {
            comboText.GetComponent<Text>().enabled = false;
            comboTimer = 0;
            comboTime = false;
        }
    }
}
