using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject.transform);
        }
    }
    
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ShowOptions()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
