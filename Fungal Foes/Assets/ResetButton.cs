using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public GameObject confirmationPanel;
    public Button confirmButton;
    public Button cancelButton;

    private void Start()
    {
        // Hide the confirmation panel when the game starts
        confirmationPanel.SetActive(false);

        // Attach the onClick events to the confirm and cancel buttons
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    public void ShowConfirmationPanel()
    {
        // Show the confirmation panel
        confirmationPanel.SetActive(true);
    }

    public void HideConfirmationPanel()
    {
        // Hide the confirmation panel
        confirmationPanel.SetActive(false);
    }

    public void OnConfirmButtonClicked()
    {
        // Reset the player data
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // Hide the confirmation panel
        HideConfirmationPanel();
    }

    public void OnCancelButtonClicked()
    {
        // Hide the confirmation panel
        HideConfirmationPanel();
    }
}

