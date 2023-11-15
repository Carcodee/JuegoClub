using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ObjectController>(out ObjectController obj))
        {
            GameManager.OnItemDropedCorrectly?.Invoke();
            Destroy(obj.gameObject);


        }
    }
}
