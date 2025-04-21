using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    //Player Health    
    public float maxHealth = 100.0f;
    public float currentHealth;
    public bool Death;
    public string levelName;

    //animator
    public Animator animator;

    [SerializeField] private PlayerController playerController;


    public void SetMaxHealth(float health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    public void SetHealth(float health)
    {
        healthBar.value = health;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
    }
   
    //Damage player
    
    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        SetHealth(currentHealth);
        if (currentHealth > 0)
        {
            Debug.Log("Hit");
            animator.SetBool("Hit", true);
            StartCoroutine(SetHitFalse());
        }
        
        else
        {
            if (!Death)
            {                
                Debug.Log("Game Over");
                animator.SetBool("Death", true);
                playerController.enabled = false;
                StartCoroutine(Deathdelay());
            }
        }
    }



    private IEnumerator SetHitFalse()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("Hit", false);
    }



    private IEnumerator Deathdelay()
    {
        yield return new WaitForSeconds(5);        
        SceneManager.LoadScene(levelName);

    }
}
