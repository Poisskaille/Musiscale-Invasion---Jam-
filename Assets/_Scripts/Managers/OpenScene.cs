using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainLevel(string name) 
    {
        SceneManager.LoadScene(name);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
