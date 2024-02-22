using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{

    
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI PlayerBalance;
    public GameObject PopupHolder;
    public Button closeUI;
    [Header("Change Name")]
    public GameObject ChangeNameHolder;
    public TMP_InputField NameInputField;
    public Button changeNameBtn;

    [Header("Trade screen")]
    public GameObject TradeHolder;
    public GameObject BuyHolder;

    public Button OpenSellMenu;
    public Button OpenBuyMenu;




    private void Start()
    {
        SetUI();
    }
    public void SetUI()
    {
        SetPlayerName(DataController.dataController.playerData.playerName);
        SetPlayerBalance();
    }

    public void SetPlayerName(string newName)
    {
        PlayerName.text = newName;
    }

    public void SetNewPlayerName()
    {
        PlayerName.text = NameInputField.text;
        DataController.dataController.SetPlayerName(NameInputField.text);
    }

    public void SetPlayerBalance()
    {
        PlayerBalance.text = "$ " + DataController.dataController.playerData.playerBalance.ToString();

    }


    public void OpenChangeNamePopup()
    {
        DataController.dataController.blockMoving = true;

        PopupHolder.SetActive(true);
        ChangeNameHolder.SetActive(true);
    }

    public void OpenTradeMenu()
    {
        DataController.dataController.blockMoving = true;

        ChangeNameHolder.SetActive(false);
        PopupHolder.SetActive(true);
        TradeHolder.SetActive(true);
        BuyHolder.SetActive(true);

    }



    public void CloseUI()
    {
        PopupHolder.SetActive(false);
        DataController.dataController.blockMoving = false;

    }


}
