using System;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    #region Private Serialized Variables

    [Tooltip("유저 이름 입력과 입장할 수 있는 버튼이 있는 Panel")]
    [SerializeField] private GameObject controlPanel;
    [Tooltip("유저의 Connection 상태를 나타내는 Label")]
    [SerializeField] private GameObject progressLabel;

    #endregion

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        controlPanel.SetActive(true);
        progressLabel.SetActive(false);
    }

    public void OnConnectUI()
    {
        controlPanel.SetActive(false);
        progressLabel.SetActive(true);
    }

    public void OnDisconnectUI()
    {
        controlPanel.SetActive(true);
        progressLabel.SetActive(false);
    }
}