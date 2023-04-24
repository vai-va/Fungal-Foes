using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManagerUP : MonoBehaviour
{
    public CoinManager coinManager;
    public PlayerMovement player;
    public int coins;
    public TMP_Text coinUI;
    public StoreItemSOUP[] storeItem;
    public StoreTemplateUP[] storePanels;
    public Button[] purchaseButton;
    int oldMaxHealth = 100;
    int oldSpeed = 5;
    int oldDamage = 30;

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

            // Update the purchase button based on the saved value
            if (PlayerPrefs.HasKey(storeItem[i].title) && PlayerPrefs.GetInt(storeItem[i].title) == 1)
            {
                purchaseButton[i].GetComponentInChildren<TMP_Text>().text = "PURCHASED";
                purchaseButton[i].interactable = false;
            }
            else
            {
                purchaseButton[i].GetComponentInChildren<TMP_Text>().text = "BUY";
                purchaseButton[i].interactable = coins >= storeItem[i].baseCost;
            }
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
                    purchaseButton[i].interactable = true;

                    if (PlayerPrefs.HasKey(storeItem[i].title))
                    {
                        if (PlayerPrefs.GetInt(storeItem[i].title) == 1)
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
                        // Enable the button and set the text to "BUY" for items that have not been bought yet
                        purchaseButton[i].GetComponentInChildren<TMP_Text>().text = "BUY";
                        purchaseButton[i].interactable = true;
                    }
                }
                else
                {
                    purchaseButton[i].interactable = false;
                }
            }
        }
    }


    //public void BuyItem(int itemIndex)
    //{
    //    if (coins < storeItem[itemIndex].baseCost)
    //    {
    //        // If the player does not have enough coins, do nothing
    //        return;
    //    }

    //    // Check if the item is already purchased
    //    if (PlayerPrefs.HasKey(storeItem[itemIndex].title) && PlayerPrefs.GetInt(storeItem[itemIndex].title) == 1)
    //    {
    //        // If the item is already purchased, set it as the current item and update the UI
    //        CheckPurchase();
    //    }

    //    // Use switch statement to determine what to do for each item
    //    switch (itemIndex)
    //    {
    //        case 0:
    //            // Subtract the cost of the item from the coins
    //            coins -= storeItem[itemIndex].baseCost;

    //            // Update the coin value in all scenes where it is visible
    //            CoinManager.instance.ChangeScore(-storeItem[itemIndex].baseCost);

    //            // Save the purchased item
    //            PlayerPrefs.SetInt(storeItem[itemIndex].title, 1);
    //            PlayerPrefs.Save();

    //            CheckPurchase();

    //            float newMaxHealth = oldMaxHealth * 2f; // double the old max health
    //            PlayerPrefs.SetFloat("MaxHealth", newMaxHealth);

    //            break;

    //        case 1:
    //            // Subtract the cost of the item from the coins
    //            coins -= storeItem[itemIndex].baseCost;

    //            // Update the coin value in all scenes where it is visible
    //            CoinManager.instance.ChangeScore(-storeItem[itemIndex].baseCost);

    //            // Save the purchased item
    //            PlayerPrefs.SetInt(storeItem[itemIndex].title, 1);
    //            PlayerPrefs.Save();

    //            CheckPurchase();

    //            float newSpeed = oldSpeed + 2; // increase speed by 2
    //            PlayerPrefs.SetFloat("Speed", newSpeed);

    //            break;

    //        default:
    //            // Subtract the cost of the item from the coins
    //            coins -= storeItem[itemIndex].baseCost;

    //            // Update the coin value in all scenes where it is visible
    //            CoinManager.instance.ChangeScore(-storeItem[itemIndex].baseCost);

    //            // Save the purchased item
    //            PlayerPrefs.SetInt(storeItem[itemIndex].title, 1);
    //            PlayerPrefs.Save();

    //            CheckPurchase();

    //            break;
    //    }
    //}


    //public void BuyItemHealth()
    //{
    //    int itemIndex = 0;
    //    if (coins < storeItem[itemIndex].baseCost)
    //    {
    //        // If the player does not have enough coins, do nothing
    //        return;
    //    }

    //    // Check if the item is already purchased
    //    if (PlayerPrefs.HasKey(storeItem[itemIndex].title) && PlayerPrefs.GetInt(storeItem[itemIndex].title) == 1)
    //    {
    //        // If the item is already purchased, set it as the current item and update the UI
    //        CheckPurchase();
    //    }

    //    // Subtract the cost of the item from the coins
    //    coins -= storeItem[itemIndex].baseCost;

    //    // Update the coin value in all scenes where it is visible
    //    CoinManager.instance.ChangeScore(-storeItem[itemIndex].baseCost);

    //    // Save the purchased item
    //    PlayerPrefs.SetInt(storeItem[itemIndex].title, 1);
    //    PlayerPrefs.Save();

    //    CheckPurchase();

    //    float newMaxHealth = oldMaxHealth * 2f; // double the old max health
    //    PlayerPrefs.SetFloat("MaxHealth", newMaxHealth);
    //}


    public void BuyItems(int itemIndex)
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
            CheckPurchase();
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

    public void SetHealth()
    {
        float newMaxHealth = oldMaxHealth * 2f; // double the old max health
        PlayerPrefs.SetFloat("MaxHealth", newMaxHealth);
    }

    public void SetSpeed()
    {
        float newSpeed = oldSpeed + 2; // increase speed by 2
        PlayerPrefs.SetFloat("Speed", newSpeed);
    }

    public void SetAttack()
    {
        int newDamage = oldDamage + 20; // increase speed by 2
        PlayerPrefs.SetInt("SpecialAtackDamage", newDamage);
    }

    public void SetThings(int index)
    {
        if (index == 0)
        {
            float newMaxHealth = oldMaxHealth * 2f; // double the old max health
            PlayerPrefs.SetFloat("MaxHealth", newMaxHealth);
        }

        else if (index == 2)
        {
            float newSpeed = oldSpeed + 2; // increase speed by 2
            PlayerPrefs.SetFloat("Speed", newSpeed);
        }
    }
}
