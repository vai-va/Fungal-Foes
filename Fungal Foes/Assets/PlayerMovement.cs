using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float NextAttackTime = 0f;
    private bool IsAttacking;
    private Vector2 AttackPosition;
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
        Vector2 Upposition = new Vector2(0f, AttackPosition.x);
        Vector2 Downposition = new Vector2(0f, -AttackPosition.x);
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
        if(IsAttacking)
        {
                if (Time.time > NextAttackTime)
                {
                    animator.SetTrigger("Attack");
                    Attack();
                    NextAttackTime = Time.time + 1f / AttackRate;
                }
            IsAttacking = false;
        }
        else
        {
            rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
        }
    }
    public void TakeDamage(int dmg)
    {
            animator.SetTrigger("Hurt");
            currentHealth -= dmg;
            healthbar.SetHealth(currentHealth);
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
    public void ClickToAttack(bool isattacking)
    {
        IsAttacking = isattacking;
    }
}
