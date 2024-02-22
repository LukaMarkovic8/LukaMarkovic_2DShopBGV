using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public TextMeshProUGUI PlayerName;
    public Button openChangeNameBtn;


    public TextMeshProUGUI PlayerBalance;

    public GameObject PopupHolder;

    public Button closeUI;

    [Header("Change Name")]
    public GameObject ChangeNameHolder;
    public TMP_InputField NameInputField;
    public Button changeNameBtn;

    private void Start()
    {
        SetUI();
    }
    public void SetUI()
    {
        SetPlayerName(DataController.dataController.playerData.playerName);
        SetPlayerBalance(DataController.dataController.playerData.playerBalance);
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

    public void SetPlayerBalance(int newBalance)
    {
        PlayerBalance.text = "$ " + newBalance.ToString();

    }


    public void OpenChangeNamePopup()
    {
        PopupHolder.SetActive(true);
        ChangeNameHolder.SetActive(true);
    }

    public void OpenTradeMenu()
    {
        PopupHolder.SetActive(true);

    }

    public void CloseUI()
    {
        PopupHolder.SetActive(false);

    }


}
