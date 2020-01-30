using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int sceneToLoad;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKey)
            SceneManager.LoadScene(sceneToLoad);
    }
}
