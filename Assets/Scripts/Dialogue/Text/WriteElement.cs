using System;
using TMPro;
using UnityEngine;

[Serializable]
public class WriteElement {
    public TextMeshPro textComponent;
    public string text;

    public int charIndex;
    public float charPerTime;
    public float typeTimer;

    public Action onComplete;

    public WriteElement(TextMeshPro textComponent, string text, float charPerTime, Action onComplete) {
        this.textComponent = textComponent;
        this.text = text;
        this.charPerTime = charPerTime;
        this.onComplete = onComplete;

        charIndex = 0;
        typeTimer = 0.0f;
    }

    public bool IsWritingText => charIndex < text.Length;

    public bool WriteText() {
        typeTimer += Time.deltaTime;
        while (typeTimer > charPerTime) {
            typeTimer = 0.0f;
            textComponent.text = text.Substring(0, charIndex++);

            if (charIndex <= text.Length) continue;
            onComplete?.Invoke();
            return true;
        }

        return false;
    }

    public void WriteAllAndDestroy() {
        textComponent.text = text;
        charIndex = text.Length;
        onComplete?.Invoke();
        TextWriter.RemoveWriteInstance(textComponent);
    }
}