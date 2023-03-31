using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    private GameObject Player;
    public float speed;
    private Animator anim;
    public float Maxhealth;
    public float currentHealth;
    public float distanceFromPlayer;
    public Transform AttackPoint;
    public float AttackRange = 2f;
    public LayerMask PlayerLayer;
    public int Damage;
    private float Invicability_Time = 0f;
    public float invincabilty_frames = 2f;
    public float stuntime = 2f;
    private bool BeingDamaged = false;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
        currentHealth = Maxhealth;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        if (distance > distanceFromPlayer && !anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            Vector2 direction = Player.transform.position - transform.position;
            direction.Normalize();
            anim.SetFloat("x", direction.x);
            anim.SetFloat("y", direction.y);
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            if(Time.time > Invicability_Time)
            {
                Attack();
                Invicability_Time = Time.time + invincabilty_frames;
            }
        }
    }
    void Attack()
    {
        Collider2D[] HitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayer);
        foreach (Collider2D player in HitPlayer)
        {
            player.GetComponent<PlayerMovement>().TakeDamage(Damage);
        }
    }
    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Hurt");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
