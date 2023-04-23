using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public CoinManager coinManager;
    public int coins;
    public TMP_Text coinUI;
    public StoreItemSO[] storeItem;
    public StoreTemplate[] storePanels;
    public Button[] purchaseButton;

    private void Start()
    {
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
            storePanels[i].image.sprite = storeItem[i].image;
        }
    }

    public void BuyItem(int itemIndex)
    {
        if (coins < storeItem[itemIndex].baseCost)
        {
            // If the player does not have enough coins, do nothing
            return;
        }

        // Subtract the cost of the item from the coins
        coins -= storeItem[itemIndex].baseCost;

        // Update the coin value in all scenes where it is visible
        CoinManager.instance.ChangeScore(-storeItem[itemIndex].baseCost);
        CheckPurchase();

        // TODO: Add code to give the player the item they purchased
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
                }
                else
                {
                    purchaseButton[i].interactable = false;
                }
            }
        }
    }


}
