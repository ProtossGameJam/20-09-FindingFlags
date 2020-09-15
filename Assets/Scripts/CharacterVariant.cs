using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterVariant : MonoBehaviour
{ 
    [Header("Variant Settings")]
    [Range(0,5)]
    public int hairIndex;
    [Range(0,4)]
    public int faceIndex;
    public Color hairColor;
    [Header("Sprite Preset")]
    public Sprite[] hairSprites;
    public Sprite[] faceSprites;
   
    [Header("Object Reference")]
    public SpriteRenderer hairRenderer;
    public SpriteRenderer faceRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hairRenderer){
            hairRenderer.color = hairColor;
            hairRenderer.sprite = hairSprites[hairIndex];
        }
        if (faceRenderer){
            faceRenderer.sprite = faceSprites[faceIndex];
        }
    }
}
