using Photon.Pun;
using TMPro;
using UnityEngine;

public class UINameInputField : MonoBehaviour
{
    private void Awake() {
        if (nameInputField == null) nameInputField = GetComponent<TMP_InputField>();
        if (placeholderText == null)
            placeholderText = transform.Find("Text Area").Find("Placeholder").GetComponent<TMP_Text>();

        prevName = UserDataManager.GetNickname();
    }

    private void Start() {
        Initialize();
    }

    /// <summary>
    ///     Initialize InputField by stored player name
    /// </summary>
    private void Initialize() {
        if (string.Equals(prevName, Constants.DEFAULT_PLAYER_NAME)) return;

        nameInputField.text = prevName;
        placeholderText.text = prevName;
    }

    /// <summary>
    ///     Set network nickname
    /// </summary>
    /// <param name="name">used by nickname</param>
    public void SetPlayerName(string name) {
        UserDataManager.SetNickname(name);
        PhotonNetwork.NickName = UserDataManager.GetNickname(true);
    }

    /// <summary>
    ///     Check if name is Empty
    /// </summary>
    /// <param name="name">used by nickname</param>
    public void CheckNameIsEmpty(string name) {
        if (string.IsNullOrWhiteSpace(name)) {
            print("[WARNING] No name has typed in field. Use default name or previous name.");
            name = prevName;
        }

        SetPlayerName(name);
    }

    #region Private Serialized Variables

    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text placeholderText;

    private string prevName;

    #endregion
}