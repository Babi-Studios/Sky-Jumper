using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    int totalScore;
    int level;
    
    private const string START_SCORE_KEY = "StartScore";
    private const string LEVEL_KEY = "Level";

    public Text totalScoreText;
    
    private void Awake()
    {
        level = PlayerPrefs.GetInt(LEVEL_KEY);
        totalScore = PlayerPrefs.GetInt(START_SCORE_KEY);
      
        UpdatePageData();
        
    }
    
    private void UpdatePageData()
    {
        totalScoreText.text = totalScore.ToString("0");
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(level + 1);
    }
}
