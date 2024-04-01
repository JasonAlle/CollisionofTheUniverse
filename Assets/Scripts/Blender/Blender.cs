using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BladeTypes
{
    NoSpin,
    Single,
    Double,
    Triple,
    None,
}
public class Blender : MonoBehaviour
{
    [SerializeField]
    private BladeTypes bladeType;
    [SerializeField]
    private GameObject singleBlade;
    [SerializeField]
    private GameObject doubleBlade;
    [SerializeField]
    private GameObject tripleBlade;
    [SerializeField]
    private float singleBladeSpeed;
    [SerializeField]     
    private float doubleBladeSpeed;
    [SerializeField]      
    private float tripleBladeSpeed;
    private bool isBlending = false;

    private void OnEnable()
    {
        isBlending = false;
        //Play animation
        //Turn on collision
        //Play sound
        //After animation
        isBlending = true;
    }
    private void OnDisable()
    {

    }
    private void SpinBlades()
    {
        switch (bladeType)
        {
            case BladeTypes.NoSpin:
                break;
            case BladeTypes.None:
                break;
            case BladeTypes.Single:
                singleBlade.transform.Rotate(Vector3.up, singleBladeSpeed * Time.fixedDeltaTime);
                break;
            case BladeTypes.Double:
                singleBlade.transform.Rotate(Vector3.up, singleBladeSpeed * Time.fixedDeltaTime);
                doubleBlade.transform.Rotate(Vector3.up, doubleBladeSpeed * Time.fixedDeltaTime);
                break;
            case BladeTypes.Triple:
                singleBlade.transform.Rotate(Vector3.up, singleBladeSpeed * Time.fixedDeltaTime);
                doubleBlade.transform.Rotate(Vector3.up, doubleBladeSpeed * Time.fixedDeltaTime);
                tripleBlade.transform.Rotate(Vector3.up, tripleBladeSpeed * Time.fixedDeltaTime);
                break;
            default:
                break;
        }
    }
    private void FixedUpdate()
    {
        if (isBlending == false)
            return;
        if (bladeType == BladeTypes.NoSpin || bladeType == BladeTypes.None)
            return;
        SpinBlades();

    }
}