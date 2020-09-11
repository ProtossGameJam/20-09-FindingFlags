using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

//플레이어의 데이터를 주로 관리하는 녀석인듯하다. 죽었을때나 부활하거나 하는둥. PlayerController를 스폰하며 플레이어마다 한개씩 스폰된다.
public class PlayerManager : MonoBehaviour
{
    PhotonView pv;
    GameObject player;  //이 playerManager가 관리하는 플레이어.

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(pv.IsMine) //이 PlayerManager가 내꺼(로컬)인가?
        {
            CreateController();
        }
    }

    void CreateController()
    {
        // Player에 int로 index를 부여해 PlayerCreate 하도록 변경 할 
        CharacterSpawner cs = FindObjectOfType<CharacterSpawner>();
        int temp = pv.ViewID / 1000;
        switch(temp)
        {
            case 1:
                player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController_RED"),
                    cs.GetSpawnInfo(CharacterColor.RED).point.position, Quaternion.identity);
                break;
            case 2:
                player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController_BLUE"),
                                    cs.GetSpawnInfo(CharacterColor.BLUE).point.position, Quaternion.identity); 
                break;
            case 3:
                player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController_GREEN"),
                                    cs.GetSpawnInfo(CharacterColor.GREEN).point.position, Quaternion.identity); 
                break;
            case 4:
                player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController_PURPLE"),
                                    cs.GetSpawnInfo(CharacterColor.YELLOW).point.position, Quaternion.identity); 
                break;
            case 5:
                Debug.Log("player 5 instanciated");
                break;
            case 6:
                Debug.Log("player 6 instanciated");
                break;
        }
    }
}
