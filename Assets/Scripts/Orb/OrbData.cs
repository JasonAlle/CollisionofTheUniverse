using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrbTiers
{
    tier1,
    tier2,
    tier3,
    tier4,
    tier5,
    tier6,
    tier7,
    tier8,
}


[CreateAssetMenu(menuName = "Orb")]
public class OrbData : ScriptableObject
{
    [SerializeField]
    private OrbTiers tier;
    public OrbTiers Tier { get { return tier; } set { tier = value; } }
    [SerializeField]
    private Sprite tex;
    public Sprite Tex { get { return tex; } set { tex = value;} } 
    [SerializeField]
    private int score;
    public int Score { get { return score; } set { score = value; } }
    [SerializeField]
    private int essence;
    public int Essence { get { return essence; } set { essence = value; } }
    [SerializeField]
    private float renderScale;
    public float RenderScale { get { return renderScale; } set { renderScale = value; } }
    [SerializeField]
    private float collideRad;
    public float CollideRad { get { return collideRad; } set { collideRad = value; } }
    [SerializeField]
    private float orbWeight;
    public float OrbWeight { get { return orbWeight; } set { orbWeight = value; } }

}
