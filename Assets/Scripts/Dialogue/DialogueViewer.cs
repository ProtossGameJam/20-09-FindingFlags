using System.Collections.Generic;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

public class DialogueViewer : InteractModule
{
    [SerializeField] private TextBubble textBubble;
    [SerializeField] private GameObject coolDownObject;

    [ReadOnly] [SerializeField] private DialogueData dialogue;
    [SerializeField] private List<DialogueData.SentenceElement> sentenceData;
    [ReadOnly] [SerializeField] private int curIndex;
    [ReadOnly] [SerializeField] private string curEventCode = "";

    public EventTimer timer;
    [ReadOnly] public float cooldownTime;

    private void Awake() {
        if (textBubble == null) textBubble = GetComponentInChildren<TextBubble>();
    }

    private void Start() {
        InitDialogue();
        textBubble.BubbleEnabled = false;
        EventTimerManager.RegisterTimer(timer);
    }

    public override void Interact() {
        if (IsInteractable && enabled) {
            if (timer.isRunning) return;
        
            if (!textBubble.BubbleEnabled) textBubble.BubbleEnabled = true;
            PlayDialogue();
        }
    }

    public void PlayDialogue() {
        if (textBubble.IsEndWriteLine) {
            if (curIndex < sentenceData.Count) 
                WriteDialogue(curIndex++);
            else 
                EndDialogue();
        }
        else {
            textBubble.writeElement.WriteAllAndDestroy();
        }
    }

    private void WriteDialogue(int index) {
        if (!string.IsNullOrEmpty(curEventCode)) {
            EventManager.Instance.EventExecute(this, ref curEventCode);
        }
        else {
            curEventCode = EventManager.RemoveAnswerEventCode(sentenceData[index].eventCode);
            textBubble.Write(sentenceData[index].content, Constants.DEFAULT_DIALOGUE_TYPE_SPEED);
        }
    }

    public void EndDialogue() {
        Debug.LogWarning("End Dialogue");
        if (cooldownTime <= -1.0f)
            enabled = false;
        else
            timer.StartTimer(cooldownTime);
        
        InitDialogue();
        textBubble.BubbleEnabled = false;
    }

    private void InitDialogue() {
        curIndex = 0;
        curEventCode = null;
    }
    
    public void SetDialogue(DialogueData data) {
        dialogue = data;
        sentenceData = data.sentence;
    }

    public void SetIndex(int index) {
        curIndex = index;
    }

    public void SetIndex(string eventCode) {
        if (sentenceData.Exists(line => line.eventCode.Contains(eventCode))) {
            SetIndex(sentenceData.Find(line => line.eventCode.Contains(eventCode)).index);
        }
        else {
            Debug.LogError("[DEBUG] Execute : PrintEventSentence() - The answer event code is invalid.");
        }
    }

    public void ActiveBubble(bool enable) {
        textBubble.BubbleEnabled = enable;
    }
}