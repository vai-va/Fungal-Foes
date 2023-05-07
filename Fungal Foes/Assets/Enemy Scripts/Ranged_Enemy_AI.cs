using System.Collections;
using UnityEngine;
using static EnemySpawner;

public class Ranged_Enemy_AI : MonoBehaviour
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
    public float DeathAnimationTime;
    public GameObject bullet;
    public float FireRate = 1f;
    private float nextFireTime;
    public bool animation_finished;

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
        Vector2 direction = Player.transform.position - transform.position;
        direction.Normalize();
        anim.SetFloat("x", direction.x);
        anim.SetFloat("y", direction.y);
        if (anim.GetBool("IsDead"))
        {
            StartCoroutine(waitForAnimation(DeathAnimationTime));
        }
        else
        {
            distance = Vector2.Distance(transform.position, Player.transform.position);
            if (distance > distanceFromPlayer && !anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            }
            else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged") && nextFireTime < Time.time && distance <= distanceFromPlayer)
            {
                anim.SetTrigger("Attack");
                StartCoroutine("Attack");
                nextFireTime = Time.time + FireRate;
            }
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.1f);
        Vector2 direction = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        Instantiate(bullet, AttackPoint.position, q);
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
    IEnumerator waitForAnimation(float time)
    {
        yield return new WaitForSeconds(time);
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

