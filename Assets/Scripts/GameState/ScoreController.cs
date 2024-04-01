using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private ScoreListener scoreListener;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private void OnEnable()
    {
        scoreText.text = "0";
        scoreListener.ScoreIncreaseEvent += HandleScoreIncrease;
    }
    private void OnDisable()
    {
        scoreListener.ScoreIncreaseEvent -= HandleScoreIncrease;
    }
    
    private void HandleScoreIncrease(int amount)
    {
        //Change Text
        Debug.Log("Score: " + amount.ToString());
        scoreText.text = amount.ToString();
    }
}
