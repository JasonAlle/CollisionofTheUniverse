using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]
     OrbSpawner spawner;
    [SerializeField]
    private Controls_Listener input;
    [SerializeField]
    private StateListner stateListner;
    private bool isDropping;
    private Orb orbToDrop;
    private float spawnOrbTimer = 0.0f;
    bool isDisabled = false;

    private void OnEnable()
    {
        input.DropEvent += HandleDrop;
        stateListner.BlendStateEvent += HandleBlend;
        stateListner.GameplayStateEvent += HandleGame;
    }
    private void OnDisable()
    {
        input.DropEvent -= HandleDrop;
        stateListner.BlendStateEvent -= HandleBlend;
        stateListner.GameplayStateEvent -= HandleGame;

    }
    private void HandleBlend()
    {
        isDisabled = true;
    }
    private void HandleGame()
    {
        isDisabled = false;
    }
    private void Start()
    {
        spawner.PickOrb();
        FillDropper();
    }
    private void HandleDrop()
    {
        if (isDisabled || isDropping)
            return;
        isDropping = true;
        orbToDrop.transform.SetParent(null);
        orbToDrop.DropTheFucker(this.transform.up * 500.0f);
        spawner.PickOrb();
        
    }
    private void FillDropper()
    {
        orbToDrop = spawner.Spawn();
        orbToDrop.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
        orbToDrop.transform.SetParent(this.gameObject.transform);
        
    }
    private void Update()
    {
        if (isDropping == true)
        {
            if (spawnOrbTimer >= 1.0f)
            {
                spawnOrbTimer = 0.0f;
                isDropping = false;
                FillDropper();
            }
            else
            {
                spawnOrbTimer += Time.deltaTime;
            }
        }
    }
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;

        Gizmos.DrawLine(this.transform.position, this.transform.position + (this.transform.up * 50.0f));
        //Gizmos.color = Color.red;
        //Gizmos.DrawCube(new Vector3(transform.position.x, (transform.position.y - noBounceOffset), 0.0f), new Vector3(6.0f, 0.06f, 1.0f));
    }
}
