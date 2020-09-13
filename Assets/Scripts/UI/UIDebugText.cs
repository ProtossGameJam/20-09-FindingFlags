using TMPro;

public class UIDebugText : MonoSingleton<UIDebugText>
{
    public TMP_Text debugText;

    public static void Logging(string text)
    {
        var tempText = "[DEBUG] " + text;
        Instance.debugText.text = tempText;
#if UNITY_EDITOR
        print(tempText);
#endif
    }
}