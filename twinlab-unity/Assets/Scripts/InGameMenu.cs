using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject backPanel;
    public int mainMenuSceneIndex;

    bool inMenu;
    public static InGameMenu instance;

    void Start()
    {
        inMenu = false;
        backPanel.SetActive(inMenu);
        Time.timeScale = 1;
        //Cursor.visible = inMenu;
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }

    public void Toggle()
    {
        inMenu = !inMenu;
        backPanel.SetActive(inMenu);
        Time.timeScale = (inMenu) ? 0f : 1f;
        //Cursor.visible = inMenu;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneIndex);
        Time.timeScale = 1f;
    }

    public  void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
