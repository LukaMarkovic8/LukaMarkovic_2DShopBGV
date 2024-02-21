using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI PlayerBalance;


    public void SetPlayerName(string newName)
    {
        PlayerName.text = newName;
    }

    public void SetPlayerBalance(int newBalance)
    {
        PlayerBalance.text = "$ " + newBalance.ToString();

    }
}
