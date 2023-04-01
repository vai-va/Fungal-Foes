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
    public int Damage;
    public int MaxHealth;
    private int currentHealth;
    public Healthbar healthbar;
    public float AttackRate = 2f;
    private Vector2 AttackPosition;
    public float invincabilityframes = 2f;
    private float nextHurtTime = 0f;


    public Button specialAttackButton;
    public Button attackButton;

    private bool hasPlayedSpecialAttack = false;

    //public Transform interactor;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = MaxHealth;
        healthbar.SetMaxhealth(MaxHealth);
        AttackPosition = AttackPoint.localPosition;
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
        if (move.x > 0)
        {
            AttackPoint.transform.localPosition = Rightposition;
        }
        if (move.x < 0)
        {
            AttackPoint.transform.localPosition = Leftposition;
        }
        if (move.y > 0.7)
        {
            AttackPoint.transform.localPosition = Upposition;
        }
        if (move.y < -0.7)
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
        if (animatorState.IsName("BranchAttack"))
        {
            Attack();
            attackButton.enabled = false;
            specialAttackButton.enabled = false;
        }
        else if (animatorState.IsName("PlayerDeath") || animatorState.IsName("PlayerDeathGhost") || animatorState.IsName("SpecialAttack"))
        {
            specialAttackButton.enabled = false;
            attackButton.enabled = false;
        }
        else
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
    }
    public void TakeDamage(int dmg)
    {
        if(Time.time > nextHurtTime)
        {
            animator.SetTrigger("Hurt");
            currentHealth -= dmg;
            healthbar.SetHealth(currentHealth);
            nextHurtTime = Time.time + invincabilityframes;
        }
    }
    void Attack()
    {
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position,AttackRange,Enemylayers);
        foreach (Collider2D enemy in HitEnemies)
        {
            enemy.GetComponent<Enemy_AI>().TakeDamage(Damage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if(AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position,AttackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            Destroy(collision.gameObject);
        }
    }
}
