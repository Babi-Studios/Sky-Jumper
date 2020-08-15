using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private const string START_SCORE_KEY = "StartScore";
    private const string LEVEL_SCORE_KEY = "LevelScore";
    private const string LEVEL_KEY = "Level";
    public int currentScore;
    public int startScore;
    int levelScore;
    int level;
    public Text scoreText;
    [SerializeField] GameObject scoreTextGameObject;

    public int collectableScore;
   
    private bool ableToMultiply = true;

    private void Awake()
    {
        collectableScore = 0;
        currentScore = 0;
        startScore = PlayerPrefs.GetInt(START_SCORE_KEY);
        UpdateScoreText();
    }
    
    public void SaveFinalScoreOfLevel()
    {
        startScore += currentScore;
        PlayerPrefs.SetInt(START_SCORE_KEY, startScore); 
    }
    
    public void SaveLevelScore()
    {
        levelScore = currentScore;
        PlayerPrefs.SetInt(LEVEL_SCORE_KEY, levelScore); 
    }
    public void SaveLevelNumber()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(LEVEL_KEY, level);
    }
    private void UpdateScoreTextWithMultiplier(int multiplier)
    {
        scoreText.text = currentScore.ToString("0") + "x" + multiplier.ToString("0");
    }
    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString("0");
    }

    public void IncreaseCollectableScore()
    {
        collectableScore += 1;
    }
    
    public void DecreaseCollectableScore()
    {
        collectableScore -= 1;
    }

    public void LevelEndScoreAnimation()
    { 
        LeanTween.moveLocal(scoreTextGameObject, new Vector3(0, 375, 0), 4).setEaseInOutBack();
        Invoke("UpdateScoreText",4f);
    }
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("childPlatform"))
        {
            currentScore += 3;
            UpdateScoreText();
        }
        if (other.gameObject.CompareTag("childMBP"))
        {
            currentScore += 2;
            UpdateScoreText();
        }*/
        
        if (other.gameObject.CompareTag("Collectable"))
        {
            currentScore ++;
            UpdateScoreText();
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (currentScore > 0)
            {
                currentScore--;
                UpdateScoreText();
            }
        }
        
        

        if (ableToMultiply)
        {
            
            if (other.gameObject.CompareTag("Times1"))
            {
                UpdateScoreTextWithMultiplier(1);
                currentScore *= 1;
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times2"))
            {
                UpdateScoreTextWithMultiplier(2);
                currentScore *= 2;
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times3"))
            {
                UpdateScoreTextWithMultiplier(3);
                currentScore *= 3;
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times4"))
            {
                UpdateScoreTextWithMultiplier(4);
                currentScore *= 4;
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times5"))
            {
                UpdateScoreTextWithMultiplier(5);
                currentScore *= 5;
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times6"))
            {
                UpdateScoreTextWithMultiplier(6);
                currentScore *= 6;
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times7"))
            {
                UpdateScoreTextWithMultiplier(7);
                currentScore *= 7;
                ableToMultiply = false;
            } 
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        /*if (other.gameObject.CompareTag("BlueBreakPad") || other.gameObject.CompareTag("GreenBreakPad") ||
            other.gameObject.CompareTag("YellowBreakPad"))
        {
            currentScore += 1;
            UpdateScoreText();
        }
        
        if (other.gameObject.CompareTag("BlueMovingBreakPad") || other.gameObject.CompareTag("GreenMovingBreakPad") ||
            other.gameObject.CompareTag("YellowMovingBreakPad"))
        {
            currentScore += 2;
            UpdateScoreText();
        }
        
        if (other.gameObject.CompareTag("BluePlatform") || other.gameObject.CompareTag("GreenPlatform") ||
            other.gameObject.CompareTag("YellowPlatform"))
        {
            currentScore += 3;
            UpdateScoreText();
        }*/
        
        if (other.gameObject.CompareTag("BlueFinalPad") || other.gameObject.CompareTag("GreenFinalPad") ||
            other.gameObject.CompareTag("YellowFinalPad"))
        {
           LevelEndScoreAnimation();
        }
    }
}
