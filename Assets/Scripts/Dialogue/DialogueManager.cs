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
    private bool isCoroutine;   //쿨타임 코루틴 실행 여부 확인용

    private void Update()
    {
        CoolDownTime -= Time.deltaTime;
    }

    public void Interact()
    {
        if (CoolDownTime > 0)
        {
            if(!isCoroutine)
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
        if (textBubble.IsWriteOver) {
            if (currentIndex < element.sentence.Count) {
                textBubble.Write(element.sentence[currentIndex++].content, typeSpeed);
            }
            else if(element.sentence[currentIndex - 1].eventCode != "")
            {
                DoEventCode(element.sentence[currentIndex - 1].eventCode);
            }
            else {
                print("End Dialogue");
                // End group dialogue
            }
        }
        else {
            textBubble.writeElement.WriteAllAndDestroy();
        }
    }

    /* Purpose: 쿨타임일때 ... 을 출력하는 함수.
     * Variable:
     * Notice: 코루틴 함수. 너무 조악한 함수인듯. 다이얼로그와 쿨타임이 동시 출력되는 버그가 존재. 개선이 필요함.
     */
    IEnumerator PrintCoolDown()
    {
        isCoroutine = true;

        coolDownObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        coolDownObj.SetActive(false);

        isCoroutine = false;
    }


    /* Purpose: 이벤트 코드를 판독하고 실행하는 함수.
     * Variable: 이벤트 코드
     * Notice: PrintDialogue() 에서 호출될 것임.
     */
    private void DoEventCode(string eventCode)
    {
        switch(eventCode) 
        {
            case "#q":
                textBubble.gameObject.SetActive(false); //SmokyOnion : TextBubble 에 대한 이해도가 부족하여 디버그용으로 넣은 코드.
                FindObjectOfType<QuizManager>().Quiz_StartQuiz(this, transform.GetChild(0).position, 0); //Smokyonion : 위치값을 수작업으로 찾고 있음. 변수로 등록할 여부도 고려..
                break;

            default: print("Error : 이벤트 코드가 이상합니다. (~DoEventCode())"); return;
        }
    }

    /* Purpose: 새로운 다이얼로그를 세팅하고 실행하는 함수.
     * Variable: 다이얼로그 그룹 번호.
     * Notice: 
     */
    public void SetNewDialogue(int _currentGroupIndex)
    {
        currentGroupIndex = _currentGroupIndex;
        currentIndex = 0;
        PrintDialogue();
    }
}
