using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public static void AddScore() { score++; } 
    
    public static void RemoveScore(int levelScore)
    {
        score = score - levelScore;
    }

}