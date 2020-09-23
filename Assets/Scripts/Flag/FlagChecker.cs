using Photon.Pun;
using UnityEngine;

public class FlagChecker : InteractModule {
    [SerializeField] private FlagManager flagManager;
    
    public override void Interact() {
        if (flagManager.IsAllFlagCollected) {
            PhotonNetwork.LoadLevel(4);
            Debug.Log("필요한 깃발을 모두 모음!");
        }
        else {
            Debug.Log("아직 다 안모음");
        }
    }
}