using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicTest : MonoBehaviour
{
    [SerializeField] private MultiPlayerHandler multiHandler;
    
    private void Start()
    {
        for (var i = 0; i < 4; i++) {
            if (i != 0) {
                multiHandler.EnterPlayer();
            }
            else {
                multiHandler.EnterPlayer(true);
            }
        }
    }
}
