using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    private static OrbManager Instance = default;
    private List<Orb> orbs = new List<Orb>();
    private int orbCount = 0; //For keeping count of current orbs
    private int orbIterator = 0; //For setting orbID
    [SerializeField]
    private StateListner stateListner;
    bool isBlend = false;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void OnEnable()
    {
        Instance.isBlend = false;
        stateListner.BlendStateEvent += HandleBlend;
        stateListner.GameplayStateEvent += HandleGame;
    }
    private void OnDisable()
    {
        stateListner.BlendStateEvent -= HandleBlend;
        stateListner.GameplayStateEvent -= HandleGame;
    }
    private void HandleBlend()
    {
        Instance.isBlend = true;
        List<Orb> highOrbs = OrbManager.GetHighTierOrbs();
        for (int i = 0; i < highOrbs.Count; i++)
        {
            if (highOrbs[i].gameObject != null)
            {
                highOrbs[i].PrepareForBlend();
            }
        }
    }
    private void HandleGame()
    {
        Instance.isBlend = false;
        List<Orb> highOrbs = OrbManager.GetHighTierOrbs();
        for (int i = 0; i < highOrbs.Count; i++)
        {
            if (highOrbs[i].gameObject != null)
            {
                highOrbs[i].PrepareForGame();
            }
        }
    }
    public static void AddOrb(Orb orb)
    {
        Instance.orbCount++;
        Instance.orbIterator++;
        orb.OrbID = Instance.orbIterator;
        Instance.orbs.Add(orb);
        if (Instance.isBlend && orb.IsHighTier)
            orb.PrepareForBlend();
    }
    public static void RemoveOrb(Orb orbToRemove)
    {
        Instance.orbs.Remove(orbToRemove);
        Instance.orbCount--;
    }
    public static List<Orb> GetOrbs()
    {
        return Instance.orbs;
    }
    public static List<Orb> GetHighTierOrbs()
    {
        List<Orb> highOrbs = new List<Orb>();
        foreach (Orb orb in Instance.orbs)
        {
            if (orb.IsHighTier && orb != null)
            {
                highOrbs.Add(orb);
            }
        }
        return highOrbs;
    }
    public static List<Orb> GetLowTierOrbs()
    {
        List<Orb> lowOrbs = new List<Orb>();
        foreach (Orb orb in Instance.orbs)
        {
            if (!orb.IsHighTier)
            {
                lowOrbs.Add(orb);
            }
        }
        return lowOrbs;
    }

}
