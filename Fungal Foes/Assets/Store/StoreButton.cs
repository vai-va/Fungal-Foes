using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    public GameObject armorPanel;
    public GameObject characterPanel;
    private GameObject activePanel; // new variable to keep track of active panel

    void Start()
    {
        activePanel = armorPanel; // set initial active panel to armor
        armorPanel.SetActive(true); // show the armor panel by default
        characterPanel.SetActive(false); // hide the character panel by default
    }

    public void ShowArmor()
    {
        activePanel.SetActive(false); // hide current active panel
        armorPanel.SetActive(true); // show armor panel
        activePanel = armorPanel; // set armor panel as active
    }

    public void ShowCharacter()
    {
        activePanel.SetActive(false); // hide current active panel
        characterPanel.SetActive(true); // show character panel
        activePanel = characterPanel; // set character panel as active
    }
}
