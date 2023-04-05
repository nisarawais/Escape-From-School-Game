using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI Message;
    // Start is called before the first frame update
    void Start()
    {
        Message.text =  "You collected "+ Score.score.ToString() + " books out of 9";
    }

    // Update is called once per frame
    public void Back()
    {
        Score.score = 0;
        SceneManager.LoadScene(0);
    }
}
