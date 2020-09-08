using System;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour, IInteractable
{
    [SerializeField] private TextBubble textBubble;

    [SerializeField] private DialogueObject dialogueObjs;

    [ReadOnly] [SerializeField] private int currentGroupIndex;
    [ReadOnly] [SerializeField] private int currentIndex;

    [SerializeField] private float typeSpeed;

    public void Interact()
    {
        PrintDialogue();
    }

    private void PrintDialogue()
    {
        var element = dialogueObjs.data[currentGroupIndex];
        if (textBubble.IsWriteOver) {
            if (currentIndex < element.sentence.Count) {
                textBubble.Write(element.sentence[currentIndex++].content, typeSpeed);
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
}
