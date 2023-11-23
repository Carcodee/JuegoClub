using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour, IPickableType<HoldeableData>
{
    public bool isPicked = false;
    public Rigidbody rb;
    public Transform pickSpot;

    [Header("PickStats")]
    IPickableType<HoldeableData> IPickType;
    public string pickTypeName;
    public float timeToRelease;
    public HoldeableData holdData;
    void Start()
    {
        holdData.pickTypeName = pickTypeName;
        holdData.timeToRelease = timeToRelease;



        IPickType = GetComponent<IPickableType<HoldeableData>>();

        rb.isKinematic = false;
        rb.useGravity = true;
    }

    void Update()
    {
        if (isPicked)
        {
            DeactivateRb();
        }
        else
        {
            ActivateRb();
        }
    }
    public void DeactivateRb()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
    }
    public void ActivateRb()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void HandleDamage()
    {
        GameManager.instance.DamagePlayer(10);
    }

    public HoldeableData PickableType()
    {
        return holdData;
    }

}
public struct HoldeableData
{
    public string pickTypeName;
    public float timeToRelease;
}