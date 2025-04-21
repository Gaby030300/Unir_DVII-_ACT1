using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseMakeDmg : MonoBehaviour
{
    public float damage;

    //Audio Effects
    AudioSource audioSource;
    public AudioClip hitEffect;

    private void Start()
    {        
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audioSource.PlayOneShot(hitEffect, 0.3f);
            other.GetComponent<PlayerHealth>().TakeDamage(20.0f);
        }
    }
}
