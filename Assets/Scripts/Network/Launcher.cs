using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] TMP_InputField roomNameInputField; //방 이름 입력란
    [SerializeField] TMP_Text errorText; //에러났을때 UI
    [SerializeField] TMP_Text roomNameText; //방 이름
    [SerializeField] Transform roomListContent; //방 리스트 컨테이너
    [SerializeField] Transform playerListContent; //플레이어 리스트 컨테이너
    [SerializeField] GameObject roomListItemPrefabs; //방 리스트 프리팹
    [SerializeField] GameObject playerListItemPrefabs; //플레이어 리스트 프리팹
    [SerializeField] GameObject startGameButton; //방 내의 게임 시작 버튼.

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();   //Asset/Photon/PhotonUnityNetworking/Resources 에 있는 세팅으로 연결.
    }

    public override void OnConnectedToMaster() //포톤 마스터 서버에 연결될 시.
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();  //로비로 연결. 로비는 방을 찾거나 연결하기 위한 공간.
        PhotonNetwork.AutomaticallySyncScene = true; //자동으로 씬을 싱크해줌. 이는 플레이어가 준비됬단걸 확인하기 위함.(게임시작을 위해)
    }

    public override void OnJoinedLobby() //로비에 연결될 시.
    {
        MenuManager.Instance.OpenMenu("Title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000"); //내 닉네임 설정.
    }

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text); //방 이름을 받고 방을 만듬
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnJoinedRoom() //방에 참여됬을때 (방을 만들게 되면 OnCreateRoom 과 이 함수가 호출된다)
    {
        MenuManager.Instance.OpenMenu("Room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name; //방 이름

        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject); //방을 나갔다가 들어갔을때 더미 플레이어가 오류로 남아있는 것을 방지하기 위해 한번 싹 클리어 해줌.
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListItemPrefabs, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]); //방에 입장했을때 나 말고 다른 유저도 리스트에 보여야 하니까
        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient); //게임 시작 버튼은 방 안의 방장만 사용할 수 있도록.
    }

    public override void OnMasterClientSwitched(Player newMasterClient) //방장이 바뀌었을때(방장이 나가거나 등)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient); //방장이 바뀌게 되면 시작버튼도 다시 출력해야 할것이기에.
    }

    public override void OnCreateRoomFailed(short returnCode, string message) //방 만들기 실패시
    {
        errorText.text = "Room Creation Failed : " + message;
        MenuManager.Instance.OpenMenu("Error");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1); //게임 시작하는데, 이때 인자는 빌드 세팅에 있던 씬 번호.
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(); //방 나가기
        MenuManager.Instance.OpenMenu("Loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnLeftRoom() //방 나갔을 때
    {
        MenuManager.Instance.OpenMenu("Title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) //방 리스트가 업데이트 될 때
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject); //매번 업데이트 될 때마다 쌓일테니 정리를 위한 Destroy
        }

        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList) //만약 방이 지워진 방이라면 무시하기.
                continue;
            Instantiate(roomListItemPrefabs, roomListContent). GetComponent<RoomListItem>().SetUp(roomList[i]); //리스트에 계속해서 추가
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //새 플레이어가 방으로 입장했을 때 
    {
        Instantiate(playerListItemPrefabs, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
