using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "ScoreListener")]
public class ScoreListener : ScriptableObject
{
    [SerializeField]
    private int score;

    private void OnEnable()
    {
        score = 0;
    }
    public event Action<int> ScoreIncreaseEvent;

    public void OnScoreIncrease(int amount)
    {
        score += amount;
        ScoreIncreaseEvent?.Invoke(score);
    }
}
