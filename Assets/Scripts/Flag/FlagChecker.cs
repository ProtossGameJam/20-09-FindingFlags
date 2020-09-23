using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class FlagChecker : InteractModule {
    [SerializeField] private FlagManager flagManager;

    [SerializeField] private UnityEvent winEvent;
    
    public override void Interact() {
        if (flagManager.IsAllFlagCollected) {
            // RPC로 UI 출력 - 우승
            // 움직임 멈추기
            winEvent.Invoke();
            Debug.Log("필요한 깃발을 모두 모음!");
        }
        else {
            Debug.Log("아직 다 안모음");
        }
    }
}