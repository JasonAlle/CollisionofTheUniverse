using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatternizerController : MonoBehaviour
{
    [SerializeField]
    private MatternizerListener matterListener;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private StateListner stateListner;
    private bool isBlending = false;
    private void OnEnable()
    {
        slider.value = 0;
        slider.maxValue = matterListener.MaxMatter;
        matterListener.MatterIncreaseEvent += HandleMatterIncrease;
        stateListner.BlendStateEvent += HandleBlend;
        stateListner.GameplayStateEvent += HandleGame;
    }
    private void OnDisable()
    {
        matterListener.MatterIncreaseEvent -= HandleMatterIncrease;
        stateListner.BlendStateEvent -= HandleBlend;
        stateListner.GameplayStateEvent -= HandleGame;
    }

    private void HandleGame()
    {
        matterListener.Matter = 0;
        matterListener.MaxMatter += 100;
        SetMatterValues();
        isBlending = false;
    }

    private void HandleBlend()
    {
        isBlending = true;
    }
    private void SetMatterValues()
    {
        slider.value = matterListener.Matter;
        slider.maxValue = matterListener.MaxMatter;
    }
    private void HandleMatterIncrease(int amount)
    {
        if (isBlending)
            return;
        //Change Amount
        slider.value = amount;
        if (slider.value >= slider.maxValue )
        {
            Debug.Log("Matternizer is trying to activate blender!");
            if (EvolutionHandler.IsEvolving)
            {
                Debug.Log("Matternizer is telling evolution to do it");
                EvolutionHandler.StateIsWaiting = true;
                return;
            }
            Debug.Log("Matternizer entering Blend Mode!");
        stateListner.OnBlendState();
        }
    }
}
