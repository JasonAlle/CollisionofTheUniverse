using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionHandler : MonoBehaviour
{
    [SerializeField]
    OrbSpawner spawner;
    [SerializeField]
    private List<OrbData> orbs = new List<OrbData>();
    private static EvolutionHandler Instance = default;
    public static event System.Action<Vector2, OrbTiers> EvolutionEvent;
    private float pointResetTimer = 0.0f;
    private Vector2 pointHappenedAlready;
    private Vector2 pointResetPoint;
    [SerializeField]
    private float baseLiftAmount = .2f;
    [SerializeField]
    private StateListner stateListener;
    [SerializeField]
    private MatternizerListener matternizerListener;
    private bool isEvolving = false;
    public static bool IsEvolving { get { return Instance.isEvolving; } }
    private bool stateIsWaiting = false;
    public static bool StateIsWaiting { get { return Instance.stateIsWaiting; } set { Instance.stateIsWaiting = value; } }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        pointResetPoint = new Vector2(-10000.0f, -10000.0f);
    }
    public static void Evolve(Vector2 point, OrbTiers tier)
    {
        if (tier == OrbTiers.tier8)
            return;
        if (Instance.pointHappenedAlready == point)
            return;
        Instance.isEvolving = true;
        Debug.Log("EvolutionHandler is evolving!");
        Instance.pointHappenedAlready = point;
        Vector3 spawnPos = new Vector3(point.x, point.y, 0.0f);
        Orb orbSpawn = Instance.spawner.SpawnSpecificOrb(Instance.orbs[((int)tier + 1)], spawnPos, Quaternion.identity);
        //Tell Orb it lives through evolution
        orbSpawn.HasEvolved();
        EvolutionEvent?.Invoke(point, tier);
        if (orbSpawn.Data.Tier >= OrbTiers.tier7)
        {
            Instance.stateListener.OnLateGameState();
        }
        Instance.matternizerListener.OnMatterIncrease(orbSpawn.Data.Essence);
        Debug.Log("EvolutionHandler is finished evolving!");
        Instance.isEvolving = false;
        if (Instance.stateIsWaiting)
        {
            Debug.Log("EvolutionHandler is activating blender");
            Instance.stateIsWaiting = false;
            Instance.stateListener.OnBlendState();
        }
    }
    private void Update()
    {
        if (pointHappenedAlready.x != -10000.0f)
        {
            pointResetTimer += Time.deltaTime;
        }
        if (pointResetTimer >= 1.0f)
        {
            pointHappenedAlready = pointResetPoint;
            pointResetTimer = 0.0f;
        }
    }
}
