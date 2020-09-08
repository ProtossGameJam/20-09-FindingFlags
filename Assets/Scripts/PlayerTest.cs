using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private TextBubble _textBubble;
    
    private void Start()
    {
        _textBubble = TextBubble.CreateBubble(TextBubble.BubbleType.DEFAULT, transform);
        _textBubble.Write("Test");
    }
}
