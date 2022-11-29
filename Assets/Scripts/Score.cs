using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private int score = 0;
    
    public void AddScore()
    {
        score++;
        text.text = score.ToString();
    }
}
