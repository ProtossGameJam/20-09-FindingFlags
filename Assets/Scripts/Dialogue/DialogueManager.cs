using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using WebSocketSharp;

public class DialogueManager : MonoBehaviour, IInteractable
{
    [SerializeField] private TextBubble textBubble;

    [SerializeField] private DialogueObject dialogueObjs;

    [SerializeField] private GameObject coolDownObj;    //쿨타임 출력용 오브젝트.

    [ReadOnly] [SerializeField] private int currentGroupIndex;
    [ReadOnly] [SerializeField] private int currentIndex;

    [SerializeField] private float typeSpeed;
    [SerializeField] private float coolDownTime = 0; //쿨타임용 변수.
    public float CoolDownTime
    {
        set
        {
            if (coolDownTime + value < 0) coolDownTime = 0;
            else coolDownTime = value;
        }
        get { return coolDownTime; }
    }

    [SerializeField] private string curEventCode = "";

    private bool isCoroutine;   //쿨타임 코루틴 실행 여부 확인용

    private void Update()
    {
        CoolDownTime -= Time.deltaTime;
    }

    public void Interact()
    {
        if (CoolDownTime > 0)
        {
            if (!isCoroutine)
            {
                StartCoroutine(PrintCoolDown());
            }
            return;
        }

        PrintDialogue();
    }

    private void PrintDialogue()
    {
        var element = dialogueObjs.data[currentGroupIndex];
        if (textBubble.IsWriteOver)
        {
            if (curEventCode != "")
            {
                DoEventCode(curEventCode);
            }
            else if (curEventCode == "")
            {
                curEventCode = element.sentence[currentIndex].eventCode;
                textBubble.Write(element.sentence[currentIndex++].content, typeSpeed);
            }
            else
            {
                print("End Dialogue");
                // End group dialogue
            }
        }
        else
        {
            textBubble.writeElement.WriteAllAndDestroy();
        }
    }

    /// <summary>
    /// 새로운 다이얼로그를 세팅하고 실행하는 함수.
    /// </summary>
    /// <param name="_currentGroupIndex">다이얼로그 그룹 번호</param>
    public void SetNewDialogue(int _currentGroupIndex)
    {
        currentGroupIndex = _currentGroupIndex;
        currentIndex = 0;
        PrintDialogue();
    }



    /// <summary>
    /// 쿨타임일때 ... 을 출력하는 함수.
    /// 다이얼로그와 쿨타임이 동시 출력되는 버그가 존재. 개선이 필요함.
    /// </summary>
    IEnumerator PrintCoolDown()
    {
        isCoroutine = true;

        coolDownObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        coolDownObj.SetActive(false);

        isCoroutine = false;
    }


    /// <summary>
    /// 이벤트 코드를 판독하고 실행하는 함수.
    /// </summary>
    /// <param name="eventCode">이벤트 코드</param>
    public void DoEventCode(string eventCode)
    {
        switch (eventCode)
        {
            case "#q":
                textBubble.gameObject.SetActive(false); //SmokyOnion : TextBubble 에 대한 이해도가 부족하여 디버그용으로 넣은 코드.
                GetComponent<NPCData>().var_correctQuizEventCode = FindObjectOfType<QuizManager>().Quiz_StartQuiz(this, transform.GetChild(0).position, 0); //Smokyonion : 실행할 퀴즈에 대한 인덱스값을 어떻게 넘겨주는가?
                break;

            case "#s1":
            case "#s2":
            case "#s3":
                transform.GetChild(1).gameObject.SetActive(false); //여기서 다이얼로그가 사라져야함
                var npcData = GetComponent<NPCData>();
                if (npcData.Check_correctQuizEventCode(eventCode)) 
                {
                    //npcData.var_flag;
                    npcData.Set_randomFlag();
                    CoolDownTime = 30f;
                }
                else
                {
                    CoolDownTime = 5f;
                }
                currentIndex = 0;
                break;

            default: print("Error : 이벤트 코드가 이상합니다. (~DoEventCode())"); return;
        }
        curEventCode = "";
    }


    /// <summary>
    /// 퀴즈 대답 후에 사용하는 다이알로그 출력함수.
    /// </summary>
    /// <param name="answer"></param>
    public void printDialogueAfterQuizAnswer(string answer)
    {
        SetDialogueIndexAsAnswer(answer);
        PrintDialogue();
    }

    /// <summary>
    /// 퀴즈 대답으로 다음 다이알로그의 인덱스값을 정해주는 코드.
    /// </summary>
    /// <param name="answer"></param>
    private void SetDialogueIndexAsAnswer(string answer)
    {
        var element = dialogueObjs.data[currentGroupIndex];
        for(int i = 0; i < element.sentence.Count; i++)
        {
            if(element.sentence[i].eventCode == answer)
            {
                currentIndex = i;
                return;
            }
        }
    }
}
