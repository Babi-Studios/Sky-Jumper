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
    int currentScore;
    public int startScore;
    int levelScore;
    int level;
    public Text scoreText;
   
    private bool ableToMultiply = true;

    private void Awake()
    {
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
    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString("0");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("childPlatform"))
        {
            currentScore += 3;
            UpdateScoreText();
        }
        if (other.gameObject.CompareTag("childMBP"))
        {
            currentScore += 2;
            UpdateScoreText();
        }

        if (ableToMultiply)
        {
            
            if (other.gameObject.CompareTag("Times1"))
            {
                currentScore *= 1;
                UpdateScoreText();
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times2"))
            {
                currentScore *= 2;
                UpdateScoreText();
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times3"))
            {
                currentScore *= 3;
                UpdateScoreText();
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times4"))
            {
                currentScore *= 4;
                UpdateScoreText();
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times5"))
            {
                currentScore *= 5;
                UpdateScoreText();
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times6"))
            {
                currentScore *= 6;
                UpdateScoreText();
                ableToMultiply = false;
            }
            else if (other.gameObject.CompareTag("Times7"))
            {
                currentScore *= 7;
                UpdateScoreText();
                ableToMultiply = false;
            } 
            //SaveFinalScoreOfLevel();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BlueBreakPad") || other.gameObject.CompareTag("GreenBreakPad") ||
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
        }
        
        if (other.gameObject.CompareTag("BlueFinalPad") || other.gameObject.CompareTag("GreenFinalPad") ||
            other.gameObject.CompareTag("YellowFinalPad"))
        {
           
        }
    }
}
