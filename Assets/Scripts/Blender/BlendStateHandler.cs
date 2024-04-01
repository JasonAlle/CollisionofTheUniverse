using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlendStateHandler : MonoBehaviour
{
    [SerializeField]
    private StateListner stateListner;
    [SerializeField]
    private Blender blenderObject;
    private Blender blender;
    //[SerializeField]
    //private Lid lidObject;
    //private Lid lid;
    [SerializeField]
    private float blendTime;
    private float blenderTimer;
    private bool isBlend = false;
    [SerializeField]
    private TextMeshProUGUI timerVisualizer;
    private string cleanTimerVisualizer;
    //[SerializeField]
    //private Container container;
    //[SerializeField]
    //private float lidLeaveTime;
    //private float lidLeaveTimer = 0.0f;
    private void OnEnable()
    {
        cleanTimerVisualizer = "Blender Timer:\n{blender}";
        blenderTimer = blendTime;
        stateListner.BlendStateEvent += HandleBlend;
        SetCleanTimerVisual();
        blenderTimer = 0.0f;
    }
    private void OnDisable()
    {
        stateListner.BlendStateEvent -= HandleBlend;

    }
    private void SetCleanTimerVisual()
    {
        timerVisualizer.text = cleanTimerVisualizer;
    }
    private void UpdateTimerVisual()
    {
        Debug.Log("Updating visualizer!");
        timerVisualizer.text = timerVisualizer.text.Replace("{blender}", blenderTimer.ToString());
    }
    private void HandleBlend()
    {
        Debug.Log("Blender started!");
        isBlend = true;
       // lid = Instantiate(lidObject);
        blender = Instantiate(blenderObject);
       // container.Shake();
    }
    private void FixedUpdate()
    {
        //if (lidLeaveTimer > 0.0f)
        //{
        //    lidLeaveTime -= Time.fixedDeltaTime;
        //    if (lidLeaveTime <= 0.0f)
        //    {
        //    Destroy(lid.gameObject);
        //    }
        //}
        if (!isBlend)
            return;
        if (blenderTimer >= blendTime)
        {
            isBlend = false;
            blenderTimer = 0.0f;
            Destroy(blender.gameObject);
            stateListner.OnGameplayState();
            Debug.Log("Blender is finished and returning to gameplay!");
            SetCleanTimerVisual();
            //lidLeaveTimer = lidLeaveTime;
        }
        else
        {
            blenderTimer += Time.fixedDeltaTime;
            UpdateTimerVisual();
        }
    }
}
