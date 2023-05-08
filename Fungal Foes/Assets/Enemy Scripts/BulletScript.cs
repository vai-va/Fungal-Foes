using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    private GameObject target;
    Rigidbody2D BulletRB;
    CapsuleCollider2D BulletC;
    public LayerMask PlayerLayer;
    public int Damage;
    public Vector2 range;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        BulletC = GetComponent<CapsuleCollider2D>();
        BulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        BulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Vector2 range = new Vector2(BulletC.size.x, BulletC.size.y);
        Destroy(this.gameObject, 6);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            player.GetComponent<PlayerMovement>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }
    }

}

