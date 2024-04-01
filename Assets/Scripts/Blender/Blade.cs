using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField]
    private ScoreListener scoreListener;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Checking Collision with: " + collision.gameObject.name);
        if (!collision.gameObject.CompareTag("HighOrb") || !collision.gameObject.CompareTag("Orb"))
        {
            return;
        }
        Orb orbcollision = collision.gameObject.GetComponent<Orb>();
        if (orbcollision.IsBlended)
            return;
        if (orbcollision.IsHighTier)
        {
            orbcollision.IsBlended = true;
            int score = orbcollision.Data.Score;
            Debug.Log("Score From Data Added: " + score);
            scoreListener.OnScoreIncrease(score);
            Debug.Log("Orb score taken!");
            OrbManager.RemoveOrb(orbcollision);
            Destroy(orbcollision.gameObject);
           // HandleHighOrb(orbcollision);
        }
        else
        {
            Vector3 orbPos = orbcollision.transform.position;
            Vector2 forcePos = new Vector2(orbPos.x, orbPos.y - (orbcollision.Data.CollideRad * orbcollision.Data.RenderScale));
            orbcollision.OrbBody.AddExplosionForce(5.0f, forcePos, 2.0f);
            //HandleOrb(orbcollision);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Checking Trigger Collision with: " + collision.gameObject.name);
        if (!collision.gameObject.CompareTag("HighOrb") && !collision.gameObject.CompareTag("Orb"))
        {
            return;
        }
        Orb orbcollision = collision.gameObject.GetComponent<Orb>();
        if (orbcollision.IsBlended)
            return;
        if (orbcollision.IsHighTier)
        {
            orbcollision.IsBlended = true;
            int score = orbcollision.Data.Score;
            Debug.Log("Score From Data Added: " + score);
            scoreListener.OnScoreIncrease(score);
            Debug.Log("Orb score taken!");
            OrbManager.RemoveOrb(orbcollision);
            Destroy(orbcollision.gameObject);
           // HandleHighOrb(orbcollision);
        }
        else
        {
            Vector3 orbPos = orbcollision.transform.position;
            Vector2 forcePos = new Vector2(orbPos.x, orbPos.y - (orbcollision.Data.CollideRad * orbcollision.Data.RenderScale));
            orbcollision.OrbBody.AddExplosionForce(5.0f, forcePos, 2.0f);
           // HandleOrb(orbcollision);
        }
    }
    private void HandleHighOrb(Orb orb)
    {
        orb.IsBlended = true;
        int score = orb.Data.Score;
        Debug.Log("Score From Data Added: " + score);
        scoreListener.OnScoreIncrease(score);
        Debug.Log("Orb score taken!");
        OrbManager.RemoveOrb(orb);
        Destroy(orb.gameObject);
    }
    private void HandleOrb(Orb orb)
    {
        Vector3 orbPos = orb.transform.position;
        Vector2 forcePos = new Vector2(orbPos.x, orbPos.y - (orb.Data.CollideRad * orb.Data.RenderScale));
        orb.OrbBody.AddExplosionForce(5.0f, forcePos, 2.0f);
    }
}
