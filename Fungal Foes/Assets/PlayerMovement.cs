using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick Joystick;
    Rigidbody2D rb;
    Vector2 move;
    public float moveSpeed;
    public Animator animator;
    public Button specialAttackButton;
    public Button attackButton;

    private bool hasPlayedSpecialAttack = false;


    //public Transform interactor;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Joystick.Horizontal;
        move.y = Joystick.Vertical;

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
    }
}
