using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void Rules()
    {
        SceneManager.LoadScene(5);
    }

    public void Credit()
    {
        SceneManager.LoadScene(6);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
