using System.Collections;
using UnityEngine;
using static EnemySpawner;

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
    private EnemySpawner enemySpawner; // Add reference to EnemySpawner script here
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
        currentHealth = Maxhealth;

        // Find EnemySpawner script in the scene and assign it to the enemySpawner variable
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(anim.GetBool("IsDead"))
        {
            StartCoroutine(waitForAnimation());
        }
        else
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
            else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
            {
                Attack();
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
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged") && !anim.GetBool("IsDead"))
        {
            anim.SetTrigger("Hurt");
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                anim.SetBool("IsDead", true);
            }
        }

        }
    IEnumerator waitForAnimation()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(1.3f);
        int coinValue = Random.Range(1, 4);
        enemySpawner.DropCoins(transform.position, coinValue);
        Destroy(gameObject);
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

