using System;
using TMPro;
using UnityEngine;

public class UIPlayerNameInput : MonoBehaviour
{
    #region Private Serialized Variables

    [SerializeField] private TMP_InputField nameInputField;

    #endregion

    #region Private Variables

    // Store the PlayerPref Key to avoid typos
    private const string PrefKeyPlayerName = "PlayerName";

    #endregion
    
    private void Awake()
    {
        if (nameInputField == null) {
            nameInputField = GetComponent<TMP_InputField>();
        }
    }

    private void Start()
    {
        
    }

    private void InitializeInputField()
    {
        if (PlayerPrefs.HasKey(PrefKeyPlayerName)) {
            
        }
    }
}