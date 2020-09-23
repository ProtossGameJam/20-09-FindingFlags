using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNicknameManager : MonoBehaviourPun {
    [SerializeField] private TMP_Text nicknameText;

    [ReadOnly, SerializeField] private string nickname;

    private void Start() {
        SetFloatingNick(photonView.Controller.NickName);
    }

    public void SetFloatingNick(string name) {
        nickname = name;
        nicknameText.text = name;
    }
}