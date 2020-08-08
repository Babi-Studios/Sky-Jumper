using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InterstitialScreenManager : MonoBehaviour
{
    int totalScore;
    int levelScore;
    int level;
    
    private const string START_SCORE_KEY = "StartScore";
    private const string LEVEL_SCORE_KEY = "LevelScore";
    private const string LEVEL_KEY = "Level";

    public Text totalScoreText;
    public Text levelText;
    public Text levelScoreText;
    private void Awake()
    {
        level = PlayerPrefs.GetInt(LEVEL_KEY);
        levelScore = PlayerPrefs.GetInt(LEVEL_SCORE_KEY);
        totalScore = PlayerPrefs.GetInt(START_SCORE_KEY);
        
        if (level == 0)
        {
            level = 1;
        }
        UpdatePageData();
    }
    
    private void UpdatePageData()
    {
        totalScoreText.text = totalScore.ToString("0");
        levelText.text = level.ToString("0");
        levelScoreText.text = levelScore.ToString("0");
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(level + 1);
    }
    
}
