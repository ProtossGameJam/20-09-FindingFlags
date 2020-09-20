using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizBubble : Bubble
{
    [SerializeField] private TextMeshPro bubbleText;
    [HideInInspector] public WriteElement writeElement;

    public bool IsWriteOver => !writeElement.IsWritingText;


    protected override void Awake()
    {
        base.Awake();

        if (bubbleText == null) bubbleText = GetComponent<TextMeshPro>();
    }

    /// <summary>
    /// 버블에 텍스트를 입력합니다.
    /// </summary>
    /// <param name="text">버블에 표시 할 텍스트</param>
    /// <param name="typeTime">한 글자당 Type 소요 시간</param>
    public void Write(string text, float typeTime)
    {
        BubbleEnabled = true;

        writeElement = TextWriter.AddWriteInstance(bubbleText, text, typeTime, true);
    }




    protected override void BubbleRectUpdate(Vector2 contentSize)
    {
        
    }
}
