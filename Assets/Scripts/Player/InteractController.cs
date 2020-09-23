using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractController : MonoBehaviour {
    public void SetInteractMine(bool enable) {
        var interactPlayers = FindObjectsOfType<InteractManager>();
        var players = new List<InteractManager>(interactPlayers.Length);
        players.AddRange(interactPlayers.Where(player => player.photonView.IsMine));

        if (players.Count == 0) {
            Debug.LogError("[디버그] 내가 소유하고 있는 플레이어를 찾을 수 없습니다. 상호작용 모듈을 활성화하지 못했습니다.");
            return;
        }

        if (players.Count > 1) {
            Debug.LogWarning("[디버그] 내가 소유하고 있는 플레이어의 수가 1명을 초과합니다. 정상적으로 상호작용 모듈이 작동하지 않을 수 도 있습니다.");
        }
        else {
            players[0].enableInteract = enable;
        }
    }

    public void SetInteractEntire(bool enable) {
        var interactPlayers = FindObjectsOfType<InteractManager>();
        foreach (var player in interactPlayers.Where(player => player.photonView.IsMine)) {
            player.enableInteract = enable;
            return;
        }

        Debug.LogError("[디버그] 내가 소유하고 있는 플레이어를 찾을 수 없습니다. 상호작용 모듈을 활성화하지 못했습니다.");
    }
}