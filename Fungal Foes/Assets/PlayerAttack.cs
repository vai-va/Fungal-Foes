using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Button button;
    public GameObject player;
    Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        button.onClick.AddListener(OnClick);
        animator = player.GetComponent<Animator>();
    }

    private void OnClick()
    {
        DetermineAttackType();
    }

    private void DetermineAttackType()
    {
        if (PlayerPrefs.GetString("CurrentItem") == "Sword")
        {
            animator.PlayInFixedTime("SwordAttack");
        }
        else if (PlayerPrefs.GetString("CurrentItem") == "Flaming sword")
        {
            animator.PlayInFixedTime("FlamingSwordAttack");
        }
        else
            animator.PlayInFixedTime("BranchAttack");
    }
}
