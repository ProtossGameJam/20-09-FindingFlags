using DG.Tweening;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinManager : MonoBehaviourPun, IPunObservable {
    [ReadOnly] public string winnerNickname;

    [SerializeField] private GameObject winObject;
    [SerializeField] private Image[] winObjImages;
    [SerializeField] private TMP_Text winnerText;

    [SerializeField] private UnityEvent winEvent;

    [SerializeField] private float fadeInTime;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            if (!string.IsNullOrEmpty(winnerNickname)) {
                //stream.SendNext(winnerNickname);
            }
        }
        else {
            if (string.IsNullOrEmpty(winnerNickname)) {
                //winnerNickname = (string) stream.ReceiveNext();
            }
        }
    }

    public void WinnerPresent() {
        winnerNickname = PhotonNetwork.NickName;
        photonView.RPC("WinnerPresentRPC", RpcTarget.AllBufferedViaServer, winnerNickname);
    }

    [PunRPC]
    public void WinnerPresentRPC(string nick) {
        winEvent.Invoke();
        winnerNickname = nick;
        winnerText.text = nick;
        winnerText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        foreach (var image in winObjImages) {
            image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        winObject.SetActive(true);
        winnerText.DOColor(Color.white, fadeInTime);
        foreach (var image in winObjImages) {
            image.DOColor(Color.white, fadeInTime);
        }
    }
}