using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

//플레이어의 데이터를 주로 관리하는 녀석인듯하다. 죽었을때나 부활하거나 하는둥. PlayerController를 스폰하며 플레이어마다 한개씩 스폰된다.
public class PlayerManager : MonoBehaviour
{
    PhotonView pv;

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
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }
}
