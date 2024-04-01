using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D containerBox;
    [SerializeField]
    private PhysicsMaterial2D bounceMat;
    [SerializeField]
    private PhysicsMaterial2D regMat;
    [SerializeField]
    private StateListner stateListner;
    private void OnEnable()
    {
        stateListner.GameplayStateEvent += HandleGamePlay;
        //containerBox.size = Screen.height
    }
    private void OnDisable()
    {
        stateListner.GameplayStateEvent -= HandleGamePlay;
    }

    private void HandleGamePlay()
    {
       // StopShake();
    }

    //public void Shake()
    //{
    //    foreach (BoxCollider2D box in containerColliders)
    //    {
    //        box.sharedMaterial = bounceMat;
    //    }
    //}
    //public void StopShake()
    //{
    //    foreach (BoxCollider2D box in containerColliders)
    //    {
    //        box.sharedMaterial = regMat;
    //    }
    //}
}
