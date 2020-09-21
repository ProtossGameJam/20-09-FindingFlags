using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    public string var_flag; //NPC가 소유하고 있는 깃발 코드.
    public string var_correctQuizEventCode; //이 NPC가 내준 Quiz의 정답이 되는 이벤트 코드값.

    private void Start()
    {
        Set_randomFlag();
    }

    /// <summary>
    /// 깃발 코드를 랜덤으로 재설정.
    /// </summary>
    public void Set_randomFlag()
    {
        var_flag = "123";
    }

    /// <summary>
    /// NPC가 내준 퀴즈의 정답 이벤트 코드가 같은지 확인.
    /// </summary>
    /// <param name="code">이벤트 코드값. (#sN)</param>
    /// <returns></returns>
    public bool Check_correctQuizEventCode(string code)
    {
        if(code == var_correctQuizEventCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
