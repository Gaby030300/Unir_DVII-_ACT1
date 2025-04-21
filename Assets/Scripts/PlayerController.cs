using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private variables
    [SerializeField] private float speed = 10.0f;
    public float speedRotation = 45.0f;
    [SerializeField] private float jumpSpeed = 10.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rigidBody;
    public bool onGround = true;
    public LayerMask layerMask;

    //Variables for double Jump
    private const int MAX_JUMP = 2;
    private int currentJump = 0;

    //Projectile
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float coolDown = 1;

    //Range player position
    public float xRange;

    //Animator
    public Animator animator;

    //Audio Effects
    AudioSource audioSource;
    public AudioClip shootEffect;
    public AudioClip jumpEffect;
    public AudioClip collectEffect;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Keep the player in bounds.
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        //This is where we get player horizontal input        
        horizontalInput = Input.GetAxis("Horizontal");
        // Move the vehicle forward on the horizontal input
        transform.Rotate(Vector3.up, Time.deltaTime * speedRotation * horizontalInput);

        //This is where we get player vertical input 
        verticalInput = Input.GetAxis("Vertical");
        // Move the vehicle forward on the vertical input
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        //playeranimstate
        animator.SetFloat("VelX", horizontalInput);
        animator.SetFloat("VelY", verticalInput);
        animator.SetBool("ItsGround", onGround);

        //This is where the player can jump
        if (Input.GetKeyDown(KeyCode.Space) && (onGround || MAX_JUMP > currentJump))
        {
            audioSource.PlayOneShot(jumpEffect, 0.3f);
            rigidBody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            onGround = false;
            currentJump++;
        }

        //Launch projectiles
        if (Input.GetMouseButtonDown(0) && coolDown <= 0)
        {
            // Launch a projectile from the player
            audioSource.PlayOneShot(shootEffect, 0.3f);
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.transform.rotation);
            coolDown = 1;
        }
        if (coolDown >= 0)
        {
            coolDown -= Time.deltaTime;
        }
    }
    public void PlaySoundEffect()
    {        
       audioSource.PlayOneShot(collectEffect, 0.3f);
    }

    //Stop player jump
    private void OnCollisionEnter(Collision collision)
    {        
        if (Physics.Raycast(transform.position, Vector3.down, 4f))
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                onGround = true;
                currentJump = 0;
            }
        }
    }

    
}