using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public bool isPicked = false;
    public Rigidbody rb;
    public Transform pickSpot;
    void Start()
    {
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
}
