using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public FixedJoystick Joystick;
    Rigidbody2D rb;
    private Vector2 move;
    public float moveSpeed;
    public Animator animator;
    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask Enemylayers;

    public int branchDamage;
    public int swordDamage;
    public int flamingSwordDamage;

    public int SpecialAtackDamage;
    public int MaxHealth;
    private int currentHealth;
    public Healthbar healthbar;
    public float AttackRate = 2f;
    private Vector2 AttackPosition;
    public float invincabilityframes = 2f;
    private float nextHurtTime = 0f;
    public bool IsDead = false;
    private float delay = 0;

    public Button specialAttackButton;
    public Button attackButton;

    public GameObject gameOverPanel;

    private bool hasPlayedSpecialAttack = false;
    public float gameOverDelay = 4f;

    //public Transform interactor;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //ResetMaxHealth();
        //ResetMaxSpeed();
        //ResetMaxAttack();

        currentHealth = MaxHealth;
        healthbar.SetMaxhealth(MaxHealth);
        float savedMaxHealth = PlayerPrefs.GetFloat("MaxHealth");
        if (savedMaxHealth != 0f)
        {
            MaxHealth = Mathf.FloorToInt(savedMaxHealth);
            healthbar.SetMaxhealth(MaxHealth);
            currentHealth = MaxHealth;
        }

        float speed = PlayerPrefs.GetFloat("Speed", moveSpeed);
        moveSpeed = speed;

        int damage = PlayerPrefs.GetInt("SpecialAtackDamage", SpecialAtackDamage);
        SpecialAtackDamage = damage;

    }

    public void ResetMaxHealth()
    {
        PlayerPrefs.DeleteKey("MaxHealth");
        MaxHealth = 100; // default maximum health value
        healthbar.SetMaxhealth(MaxHealth);
    }

    public void ResetMaxSpeed()
    {
        PlayerPrefs.DeleteKey("Speed");
        moveSpeed = 5; // default maximum health value
    }

    public void ResetMaxAttack()
    {
        PlayerPrefs.DeleteKey("SpecialAtackDamage");
        SpecialAtackDamage = 30; // default maximum health value
    }


    // Update is called once per frame
    void Update()
    {
        move.x = Joystick.Horizontal;
        move.y = Joystick.Vertical;
        Vector2 Rightposition = new Vector2(AttackPosition.x, AttackPosition.y);
        Vector2 Leftposition = new Vector2(-AttackPosition.x, AttackPosition.y);
        Vector2 Upposition = new Vector2(0f, AttackPosition.x + 0.01f);
        Vector2 Downposition = new Vector2(0f, -AttackPosition.x - 0.01f);
        bool usingSpeacialAttack = animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack");
        if (move.x > 0 && !usingSpeacialAttack)
        {
            AttackPoint.transform.localPosition = Rightposition;
        }
        if (move.x < 0 && !usingSpeacialAttack)
        {
            AttackPoint.transform.localPosition = Leftposition;
        }
        if (move.y > 0.7 && !usingSpeacialAttack)
        {
            AttackPoint.transform.localPosition = Upposition;
        }
        if (move.y < -0.7 && !usingSpeacialAttack)
        {
            AttackPoint.transform.localPosition = Downposition;
        }
        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.y);
        animator.SetFloat("Speed", move.sqrMagnitude);

        if (move.x != 0 || move.y != 0)
        {
            animator.SetFloat("LastHorizontal", Joystick.Horizontal);
            animator.SetFloat("LastVertical", Joystick.Vertical);
        }
        if (specialAttackButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Pressed") && !hasPlayedSpecialAttack)
        {
            hasPlayedSpecialAttack = true;
            animator.Play("SpecialAttack");
        }

        //if (move.x > 0)
        //{
        //    interactor.localRotation = Quaternion.Euler(0, 0, 90);
        //}
        //if (move.x < 0)
        //{
        //    interactor.localRotation = Quaternion.Euler(0, 0, -90);
        //}
        //if (move.y > 0)
        //{
        //    interactor.localRotation = Quaternion.Euler(0, 0, 180);
        //}
        //if (move.y < 0)
        //{
        //    interactor.localRotation = Quaternion.Euler(0, 0, 0);
        //}
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);
        if(animatorState.IsName("SpecialAttack") && Time.time > delay)
        {
            StartCoroutine(SpecialAttack());
            nextHurtTime = Time.time + 1;
            specialAttackButton.enabled = false;
            attackButton.enabled = false;
            delay = Time.time + 5;
        }
        else if (animatorState.IsName("PlayerDeath") || animatorState.IsName("PlayerDeathGhost") || animatorState.IsName("SpecialAttack"))
        {
            specialAttackButton.enabled = false;
            attackButton.enabled = false;
        }
        else if (animatorState.IsName("BranchAttack") || animatorState.IsName("SwordAttack") || animatorState.IsName("FlamingSwordAttack"))
        {
            Attack();
            attackButton.enabled = false;
            specialAttackButton.enabled = false;
        }
        else if(!IsDead)
        {
            attackButton.enabled = true;
            specialAttackButton.enabled = true;

            rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
        }

        if (specialAttackButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Pressed"))
        {
            specialAttackButton.enabled = false;
        }
        else
        {
            hasPlayedSpecialAttack = false;
        }
        //if (IsAttacking)
        //{
        //        if (Time.time > NextAttackTime)
        //        {
        //            animator.SetTrigger("Attack");
        //            Attack();
        //            NextAttackTime = Time.time + 1f / AttackRate;
        //        }
        //    IsAttacking = false;
        //}



/*        //VAIVOS KODAS
        if (attackButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Pressed")){
            DetermineAttackType();
        }
        //VAIVOS KODAS*/

    }


/*    private void DetermineAttackType()
    {
        if (animator.GetBool("BranchChosen") == true)
        {
            animator.PlayInFixedTime("BranchAttack");
        }
        else if (animator.GetBool("SwordChosen") == true)
        {
            animator.PlayInFixedTime("SwordAttack");
        }
        else if (animator.GetBool("FlamingSwordChosen") == true)
        {
            animator.PlayInFixedTime("FlamingSwordAttack");
        }
        animator.ResetTrigger("Attack");
    }*/
    public void TakeDamage(int dmg)
    {
        if(Time.time > nextHurtTime && !animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
        {
            animator.SetTrigger("Hurt");
            currentHealth -= dmg;
            healthbar.SetHealth(currentHealth);
            if(currentHealth <= 0 && !IsDead)
            {
                animator.SetTrigger("Death");
                IsDead = true;
                attackButton.enabled = false;

                // show game over panel after a delay
                Invoke("ShowGameOverPanel", gameOverDelay);
            }
            nextHurtTime = Time.time + invincabilityframes;
        }
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void Attack()
    {
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position,AttackRange,Enemylayers);
        AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);
        foreach (Collider2D enemy in HitEnemies)
        {
            if (animatorState.IsName("BranchAttack"))
            {
                if(enemy.gameObject.layer == 8)
                {
                    enemy.GetComponent<Ranged_Enemy_AI>().TakeDamage(branchDamage);
                }
                else if(enemy.gameObject.layer == 6)
                {
                    enemy.GetComponent<Enemy_AI>().TakeDamage(branchDamage);
                }
            }
            else if(animatorState.IsName("SwordAttack"))
            {
                if (enemy.gameObject.layer == 8)
                {
                    enemy.GetComponent<Ranged_Enemy_AI>().TakeDamage(swordDamage);
                }
                else if (enemy.gameObject.layer == 6)
                {
                    enemy.GetComponent<Enemy_AI>().TakeDamage(swordDamage);
                }
            }
            else
            {
                if (enemy.gameObject.layer == 8)
                {
                    enemy.GetComponent<Ranged_Enemy_AI>().TakeDamage(flamingSwordDamage);
                }
                else if (enemy.gameObject.layer == 6)
                {
                    enemy.GetComponent<Enemy_AI>().TakeDamage(flamingSwordDamage);
                }
            }
        }
    }
    IEnumerator SpecialAttack()
    {
        Vector2 center = new Vector2(0, 0);
        AttackPoint.localPosition = center;
        yield return new WaitForSeconds(1.5f);
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange*2, Enemylayers);
        foreach (Collider2D enemy in HitEnemies)
        {
            enemy.GetComponent<Enemy_AI>().TakeDamage(SpecialAtackDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            Destroy(collision.gameObject);
        }
    }

}
