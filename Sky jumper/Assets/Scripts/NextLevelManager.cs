using UnityEngine.SceneManagement;
using UnityEngine;

public class NextLevelManager : MonoBehaviour
{
    int totalScore;
    int level;
    
    private const string START_SCORE_KEY = "StartScore";
    private const string LEVEL_KEY = "Level";
  
    private void Awake()
    {
        level = PlayerPrefs.GetInt(LEVEL_KEY);
        totalScore = PlayerPrefs.GetInt(START_SCORE_KEY);
      
        UpdatePageData();
        
    }
    
    private void UpdatePageData()
    {
      
    }
    
    public void LoadNextScene()
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.GetComponent<ScoreManager>().SaveLevelNumber();
        level = PlayerPrefs.GetInt(LEVEL_KEY);
        SceneManager.LoadScene(level +1);
    }
}
