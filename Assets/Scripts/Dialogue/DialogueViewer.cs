using UnityEngine;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation

public class DialogueViewer : InteractModule {
    [ReadOnly] public TextBubble textBubble;
    [SerializeField] private GameObject coolDownObject;

    [ReadOnly, SerializeField] private DialogueData dialogue;
    [ReadOnly, SerializeField] private int curIndex;
    [ReadOnly, SerializeField] private string curEventCode = "";

    public EventTimer timer;
    [ReadOnly] public float cooldownTime;

    private void Awake() {
        if (textBubble == null) textBubble = GetComponentInChildren<TextBubble>();
    }

    private void Start() {
        InitDialogue();
        textBubble.BubbleEnabled = false;
        IsInteractable = true;
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
            if (curIndex < dialogue.sentence.Count) {
                WriteDialogue(curIndex++);
            }
            else {
                EndDialogue();
            }
        }
        else {
            textBubble.writeElement.WriteAllAndDestroy();
        }
    }

    private void WriteDialogue(int index) {
        if (!string.IsNullOrEmpty(curEventCode)) {
            EventHandler.Instance.EventExecute(this, ref curEventCode);
        }
        else {
            curEventCode = EventHandler.RemoveAnswerEventCode(dialogue.sentence[index].eventCode);
            textBubble.Write(dialogue.sentence[index].content, Constants.DEFAULT_DIALOGUE_TYPE_SPEED);
        }
    }

    public void EndDialogue() {
        Debug.LogWarning("End Dialogue");
        if (cooldownTime <= -1.0f) {
            enabled = false;
        }
        else {
            timer.StartTimer(cooldownTime);
        }

        InitDialogue();
        textBubble.BubbleEnabled = false;
    }

    private void InitDialogue() {
        curIndex = 0;
        curEventCode = null;
    }

    public void SetDialogue(DialogueData data) => dialogue = data;

    public void SetIndex(int index) => curIndex = index;

    public void SetIndex(string eventCode) {
        if (dialogue.sentence.Exists(line => line.eventCode.Contains(eventCode))) {
            SetIndex(dialogue.sentence.Find(line => line.eventCode.Contains(eventCode)).index);
        }
        else {
            Debug.LogError("[DEBUG] Execute : PrintEventSentence() - The answer event code is invalid.");
        }
    }

    public void ActiveBubble(bool enable) => textBubble.BubbleEnabled = enable;
}