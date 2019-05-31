using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    public void NextScene()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        Debug.Log("Next Scene");
    }

    public void LastScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Debug.Log("Last Scene");
    }
    

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Load Start Scene");
    }

    public void Exit()
    {
        Debug.Log("Exit...");
        Application.Quit();
    }
}