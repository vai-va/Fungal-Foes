using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Chase : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    private Animator anim;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position,Player.transform.position);
        Vector2 direction = Player.transform.position - transform.position;
        direction.Normalize();
        anim.SetFloat("x", direction.x);
        anim.SetFloat("y", direction.y);
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);


    }
}
