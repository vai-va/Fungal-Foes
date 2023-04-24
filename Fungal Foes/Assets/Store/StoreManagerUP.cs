using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManagerUP : MonoBehaviour
{
    public CoinManager coinManager;
    public int coins;
    public TMP_Text coinUI;
    public StoreItemSOUP[] storeItem;
    public StoreTemplateUP[] storePanels;
    public Button[] purchaseButton;

    //set max health
    // speed
    //spec attack
    private void Start()
    {
        //PlayerPrefs.DeleteKey("CurrentItem");
        //for (int i = 0; i < storeItem.Length; i++)
        //{
        //    PlayerPrefs.DeleteKey(storeItem[i].title);
        //}
        coinManager = CoinManager.instance;
        coins = coinManager.score;
        LoadPanels();
        CheckPurchase();
    }

    public void LoadPanels()
    {
        for (int i = 0; i < storeItem.Length; i++)
        {
            storePanels[i].titleTXT.text = storeItem[i].title;
            storePanels[i].costTXT.text = storeItem[i].baseCost.ToString() + " COINS";

            // Always set the button text to "BUY" and enable the button
            purchaseButton[i].GetComponentInChildren<TMP_Text>().text = "BUY";
            purchaseButton[i].interactable = true;
        }
    }

    public void CheckPurchase()
    {
        for (int i = 0; i < storeItem.Length; i++)
        {
            if (purchaseButton != null && purchaseButton.Length > i)
            {
                if (coins >= storeItem[i].baseCost)
                {
                    if (PlayerPrefs.HasKey(storeItem[i].title) && PlayerPrefs.GetInt(storeItem[i].title) == 1)
                    {
                        // Disable the button and set the text to "PURCHASED" for items that have already been bought
                        purchaseButton[i].GetComponentInChildren<TMP_Text>().text = "PURCHASED";
                        purchaseButton[i].interactable = false;
                    }
                    else
                    {
                        // Enable the button and set the text to "BUY" for items that have not been bought yet
                        purchaseButton[i].GetComponentInChildren<TMP_Text>().text = "BUY";
                        purchaseButton[i].interactable = true;
                    }
                }
                else
                {
                    // Disable the button and set the text to "INSUFFICIENT FUNDS" for items that the player can't afford
                    purchaseButton[i].GetComponentInChildren<TMP_Text>().text = "BUY";
                    purchaseButton[i].interactable = false;
                }
            }
        }
    }


    public void BuyItem(int itemIndex)
    {
        if (coins < storeItem[itemIndex].baseCost)
        {
            // If the player does not have enough coins, do nothing
            return;
        }

        // Check if the item is already purchased
        if (PlayerPrefs.HasKey(storeItem[itemIndex].title) && PlayerPrefs.GetInt(storeItem[itemIndex].title) == 1)
        {
            // If the item is already purchased, set it as the current item and update the UI
            UseItem(itemIndex);
        }
        else
        {
            // Subtract the cost of the item from the coins
            coins -= storeItem[itemIndex].baseCost;

            // Update the coin value in all scenes where it is visible
            CoinManager.instance.ChangeScore(-storeItem[itemIndex].baseCost);

            // Save the purchased item
            PlayerPrefs.SetInt(storeItem[itemIndex].title, 1);
            PlayerPrefs.Save();

            CheckPurchase();
        }
    }


    public void UseItem(int itemIndex)
    {
        // Set the player's current item
        PlayerPrefs.SetString("CurrentItem", storeItem[itemIndex].title);
        PlayerPrefs.Save();

        // Update the button text for all items
        for (int i = 0; i < purchaseButton.Length; i++)
        {
            if (i == itemIndex)
            {
                purchaseButton[i].GetComponentInChildren<TMP_Text>().text = "USING";
                purchaseButton[i].interactable = false;
            }
            else
            {
                if (PlayerPrefs.HasKey(storeItem[i].title) && PlayerPrefs.GetInt(storeItem[i].title) == 1)
                {
                    purchaseButton[i].GetComponentInChildren<TMP_Text>().text = "USE";
                    purchaseButton[i].interactable = true;
                }
                else
                {
                    purchaseButton[i].GetComponentInChildren<TMP_Text>().text = "BUY";
                    if (coins >= storeItem[i].baseCost)
                    {
                        purchaseButton[i].interactable = true;
                    }
                    else
                    {
                        purchaseButton[i].interactable = false;
                    }
                }
            }
        }
    }
}
