using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class UIDebugRoomList : MonoBehaviour
{
    [ReadOnly] [SerializeField] private List<UIDebugPlayerText> playerList;

    [SerializeField] private GameObject debugPlayerText;

    [SerializeField] private float deleteInterval;
    private WaitForSeconds waitInterval;
    private WaitUntil waitCondition;
    
    [ReadOnly] [SerializeField] private Queue<GameObject> playerTextQueue;

    private void Awake()
    {
        playerTextQueue = new Queue<GameObject>();
        
        waitInterval = new WaitForSeconds(deleteInterval);
        waitCondition = new WaitUntil(() => playerTextQueue.Count != 0);
    }

    private void Start()
    {
        StartCoroutine(DeleteTextInterval());
    }

    public void DebugEnterPlayer(Player player)
    {
        print($"[DEBUG] Execute : DebugEnterPlayer() - UI");
        var tempTextObj = Instantiate(debugPlayerText, transform);
        tempTextObj.GetComponent<UIDebugPlayerText>().SetDebugText($"{player.NickName}({player.UserId})", true);
        playerTextQueue.Enqueue(tempTextObj);
    }
    
    public void DebugLeftPlayer(Player player)
    {
        print($"[DEBUG] Execute : DebugLeftPlayer() - UI");
        var tempTextObj = Instantiate(debugPlayerText, transform);
        tempTextObj.GetComponent<UIDebugPlayerText>().SetDebugText($"{player.NickName}({player.UserId})", false);
        playerTextQueue.Enqueue(tempTextObj);
    }

    private IEnumerator DeleteTextInterval()
    {
        while (true)
        {
            yield return waitCondition;
            yield return waitInterval;
            
            print($"[DEBUG] Execute : DeleteTextInterval() - UI");
            DestroyImmediate(playerTextQueue.Dequeue());
        }
    }
}
