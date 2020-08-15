using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestarter : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("Restarter",2f);
        }
    }

    private void Restarter()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
