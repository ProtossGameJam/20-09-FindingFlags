using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

//방을 전체적으로 통제하는 녀석. PlayerManager를 스폰하며 게임이 끝났을때 PlayerManager를 삭제하고 다음 절차를 진행함.
//이 녀석은 포톤을 상속받은 오브젝트인데, 이 녀석을 프리펩해서 사용하려면 Resources 에 넣어야 하는 모양이다.
//포톤 프리펩은 Photon View 라는걸 상속받아야만 하는데 이는 네트워크 상에서 싱크를 맞춰줘야 하기 때문이라고 한다.
//특히 이녀석은 View ID를 최대(999)로 올려놓았는데 이는 최상위 매니저이기에 다른 매니저와 충돌을 야기하지 않기 위해서이다.
//실행하게 되면 PlayerManager가 플레이어 인원 수 만큼 생성될텐데, 한개는 자신, 나머지는 다른 플레이어들의 것이다.
//영상 참조 : https://youtu.be/6qRNBPPojMA
public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private void Awake()
    {
        //전형적인 싱글톤 패턴. 여러 씬을 오가면서 딱 한개만 존재할꺼임.
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1) //만약 인게임 씬에 있다면
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity); //Resources/PhotonPrefabs 에 있는 PlayerManager 스폰.
        }
    }
}
