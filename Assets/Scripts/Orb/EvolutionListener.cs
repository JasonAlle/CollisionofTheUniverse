//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//[CreateAssetMenu(menuName = "EvolutionListner")]
//public class EvolutionListener : ScriptableObject
//{
//    private Orb orb;
//    public event System.Action<Vector2, OrbTiers> EvolutionEvent;
//    private void OnEnable()
//    {
//        if (orb == null)
//        {
//            orb = new Orb();
//        }
//        orb.EvolutionEvent += HandleEvolve;
//    }

//    private void HandleEvolve(Vector2 point, OrbTiers tier)
//    {
//        EvolutionEvent?.Invoke(point, tier);
//    }
//}
