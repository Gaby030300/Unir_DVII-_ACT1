using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreOfGalas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;
    [SerializeField] private int finalScore;

    private void Start()
    {
        score = 0;
        scoreText.text = "Galas: " + score + "/" + finalScore;
    }

    public void aumentScore()
    {
        score++;
        scoreText.text = "Galas: " + score + "/" + finalScore;
    }
}
