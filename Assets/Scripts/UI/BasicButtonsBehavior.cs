using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicButtonsBehavior : MonoBehaviour
{
    [Header("Play")]
    public Button playButton;
    public string levelNameToLoad = "InputExempleScene";

    [Header("Quit")]
    public Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            UIManager.Instance.LoadScene(levelNameToLoad);
        });

        quitButton.onClick.AddListener(() =>
        {
            UIManager.Instance.QuitGame();
        });
    }

}
