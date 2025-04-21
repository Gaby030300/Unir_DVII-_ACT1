using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ScoreOfGalas score;

    void Start()
    {
        score = GameObject.FindWithTag("Gala").GetComponent<ScoreOfGalas>();    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().PlaySoundEffect();
            score.aumentScore();            
            Destroy(gameObject);
        }
    }
}
