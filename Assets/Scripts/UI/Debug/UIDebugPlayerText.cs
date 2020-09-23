using TMPro;
using UnityEngine;

public class UIDebugPlayerText : MonoBehaviour {
    [SerializeField] private TMP_Text debugText;

    public void SetDebugText(string player, bool isEnter) =>
            debugText.text = $"[{(isEnter ? "ENTER" : "EXIT")}] Player : {player}";
}