using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    int points = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HandController>(out HandController hand)) return;
        if (other.transform.parent.transform.parent.TryGetComponent<ObjectController>(out ObjectController obj))
        {
            Destroy(obj);
            points++;
        }
    }
}
