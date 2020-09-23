using Photon.Pun;
using TMPro;
using UnityEngine;

public class UINameInputField : MonoBehaviour {
    private void Awake() {
        if (nameInputField == null) nameInputField = GetComponent<TMP_InputField>();
        if (placeholderText == null) {
            placeholderText = transform.Find("Text Area").Find("Placeholder").GetComponent<TMP_Text>();
        }
    }

    private void Start() {
        nameInputField.text = Constants.DEFAULT_PLAYER_NAME;
    }

    /// <summary>
    /// Set network nickname
    /// </summary>
    /// <param name="name"> used by nickname </param>
    public void SetPlayerName(string name) {
        setName = name;
        PhotonNetwork.NickName = name;
    }

    /// <summary>
    /// Check if name is Empty
    /// </summary>
    /// <param name="name"> used by nickname </param>
    public void CheckNameIsEmpty(string name) {
        if (string.IsNullOrWhiteSpace(name)) {
            print("[WARNING] No name has typed in field. Use default name or previous name.");
            name = Constants.DEFAULT_PLAYER_NAME;
        }

        SetPlayerName(name);
    }

#region Private Serialized Variables

    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text placeholderText;

    [ReadOnly, SerializeField] private string setName;

#endregion
}