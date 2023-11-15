using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour, IDangerZone
{
    IDangerZone IdangerZone;
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        
    }
    void Start()
    {
        IdangerZone=GetComponent<IDangerZone>();
    }

    void Update()
    {
        
    }

    public void HandleDamage()
    {
        GameManager.instance.DamagePlayer(10);
    }

}
