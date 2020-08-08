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
        if (SceneManager.GetActiveScene().buildIndex == 30)
        {
            SceneManager.LoadScene(31);
        }
        else
        {
            SceneManager.LoadScene(32);
        }
    }

   
    
}
