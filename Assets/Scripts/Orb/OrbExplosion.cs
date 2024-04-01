using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    private float expUpForce;
    [SerializeField]
    private float expForwardForce;
    [SerializeField]
    private float expRadius;
    private float staticExpRadius;
    private Vector2 expForceVec;
    private Vector3 noBouncePoint;
    [SerializeField]
    private float noBounceOffset;
    private void OnEnable()
    {
        staticExpRadius = expRadius;
        ////expForceVec = new Vector2(1.5f, expForce);
        //OrbData parentData = this.GetComponentInParent<Orb>().Data;
        //if (parentData != null)
        //{
        //    expRadius = (parentData.CollideRad * parentData.RenderScale) + staticExpRadius;
        //    noBounceOffset += expRadius;
        //    noBouncePoint = new Vector3(transform.position.x, transform.position.y - noBounceOffset, 0.0f);
        //}
    }
    public void IsEvolved()
    {
        //staticExpRadius = expRadius;
        OrbData parentData = this.GetComponentInParent<Orb>().Data;
        if (parentData != null)
        {
            expRadius = (parentData.CollideRad * parentData.RenderScale) + staticExpRadius;
            noBounceOffset += expRadius;
            noBouncePoint = new Vector3(transform.position.x,transform.position.y - noBounceOffset, 0.0f );
        }
        Explode();
    }

    private void Explode()
    {
        LayerMask orbsLayer = LayerMask.GetMask("Orb");
        Vector3 pos = this.gameObject.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, expRadius);
        Debug.Log("Checking for colliders!");
        foreach (Collider2D hit in colliders)
        {
            if (hit.gameObject == this.gameObject.transform.parent.gameObject)
                continue;
            Debug.Log("Collider Hit!");
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector3 rbPos = rb.gameObject.transform.position;
                if (rbPos.y <= (transform.position.y - noBounceOffset))
                    continue;
                if (!rb.gameObject.CompareTag("Orb"))
                    continue;
                Debug.Log("Explosion for a gameobject!!!");
                OrbData orbDataHit = rb.gameObject.GetComponent<Orb>().Data;
                Debug.Log("Collided with Orb!");
                if (rbPos.x - pos.x >= 0.0f)
                    expForceVec = new Vector2(expForwardForce * orbDataHit.OrbWeight, expUpForce * orbDataHit.OrbWeight);
                else
                    expForceVec = new Vector2(-expForwardForce * orbDataHit.OrbWeight, expUpForce * orbDataHit.OrbWeight);
                Vector2 forcePos = new Vector2(rbPos.x, rbPos.y - (orbDataHit.CollideRad * orbDataHit.RenderScale));
                rb.AddForceAtPosition(expForceVec, forcePos, ForceMode2D.Impulse);
                //Instantiate(explosionEffect, forcePos, Quaternion.identity);
            }
        }
    }
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, expRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(transform.position.x, (transform.position.y - noBounceOffset), 0.0f), new Vector3(6.0f, 0.06f, 1.0f));
    }
}
