using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BlueFinalPad") || other.gameObject.CompareTag("GreenFinalPad") ||
            other.gameObject.CompareTag("YellowFinalPad"))
        {
           
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.GetComponent<ScoreManager>().SaveFinalScoreOfLevel();
            scoreManager.GetComponent<ScoreManager>().SaveLevelNumber();
            scoreManager.GetComponent<ScoreManager>().SaveLevelScore();
            
            
            Invoke("LoadNextScene", 5f);
            
        }
    }

    private void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings-3)
        {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings-2);
            
        }
        else
        {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings-1);
        }
    }

   
    
}
