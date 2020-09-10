using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class UINameInputField : MonoBehaviour
{
    #region Private Serialized Variables

    [SerializeField] private TMP_InputField nameInputField;

    [SerializeField] private PlayerDataObject playerData;

    #endregion
    
    private void Awake()
    {
        if (nameInputField == null) {
            nameInputField = GetComponent<TMP_InputField>();
        }
    }

    private void Start()
    {
        InitializeInputField();
    }

    /// <summary>
    /// Initialize InputField by stored player name
    /// </summary>
    private void InitializeInputField()
    {
        nameInputField.text = playerData.PlayerName;
    }

    /// <summary>
    /// Set network nickname
    /// </summary>
    /// <param name="name">used by nickname</param>
    public void SetPlayerName(string name)
    {
        playerData.PlayerName = name;
        if (string.IsNullOrWhiteSpace(name)) {
            Debug.LogError("[ERROR] No name has typed in field. But store into PlayerDataObject");
            return;
        }
        
        PhotonNetwork.NickName = name;
    }
}