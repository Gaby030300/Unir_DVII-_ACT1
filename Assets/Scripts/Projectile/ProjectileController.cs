using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goose"))
        {
            other.GetComponent<GooseController>().die();            
            Destroy(gameObject);
        }
    }
}
