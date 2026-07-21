using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManagers : MonoBehaviour
{
    static public UIManagers instance;

    public CanvasGroup wholeUI;

    [Header("Pannel")]
    public RawImage pannelTexture;
    public Texture2D bluePannel;
    public Texture2D redPannel;

    public TextMeshProUGUI modeTXT;

    public CanvasGroup pannelGroup;

    [Header("Input")]
    public List<RawImage> allImage;
    public RawImage cursor;
    private Vector3 cursorBasePosition;
    private int cursorStartIndex;
    private int currentIndex;

    [Header("Score")]
    public TextMeshProUGUI scoreTXT;

    [Header("Timer")]
    public Scrollbar timerBar;

    [Header("GameOver")]
    public CanvasGroup gameOverGroup;
    public TextMeshProUGUI scoreGameOver;

    void Awake() 
    {
        if (instance == null)
            instance = this;
    }
    
    public void ChangeMode(bool isMinor) 
    {
        pannelTexture.texture = isMinor? bluePannel : redPannel;
        modeTXT.text = isMinor ? "Minor" : "Major";
    }

    public void ResetCursor() 
    { 
        cursor.rectTransform.anchoredPosition = cursorBasePosition;
        currentIndex = cursorStartIndex;
    }
    public void IncrementCursor() 
    {
        currentIndex++;
        Vector3 newPos = cursorBasePosition;
        newPos.x = allImage[currentIndex].rectTransform.localPosition.x;
        cursor.rectTransform.anchoredPosition = newPos;

    }

    public void ShowInput(ScaleEntry entry) 
    {
        if(entry.notes.Count == 7) 
        {
            foreach (var image in allImage)
                image.enabled = true;

            for(int i = 0; i < entry.notes.Count; i++)
                allImage[i].texture = entry.notes[i].noteTexture;

            cursorBasePosition.x = allImage[0].rectTransform.localPosition.x;
            cursorStartIndex = 0;

        }

        if(entry.notes.Count == 5) 
        {
            foreach (var image in allImage)
                image.enabled = true;

            allImage[0].enabled = false;
            allImage[6].enabled = false;

            for (int i = 0; i < entry.notes.Count; i++)
                allImage[i + 1].texture = entry.notes[i].noteTexture;

            cursorBasePosition.x = allImage[1].rectTransform.localPosition.x;
            cursorStartIndex = 1;
        }

        if(entry.notes.Count == 3) 
        {
            foreach (var image in allImage)
                image.enabled = true;

            allImage[0].enabled = false;
            allImage[1].enabled = false;
            allImage[5].enabled = false;
            allImage[6].enabled = false;

            for (int i = 0; i < entry.notes.Count; i++)
                allImage[i + 2].texture = entry.notes[i].noteTexture;

            cursorBasePosition.x = allImage[2].rectTransform.localPosition.x;
            cursorStartIndex = 2;
        }

        if(entry.notes.Count == 1) 
        {
            foreach (var image in allImage)
                image.enabled = true;

            allImage[0].enabled = false;
            allImage[1].enabled = false;
            allImage[2].enabled = false;
            allImage[4].enabled = false;
            allImage[5].enabled = false;
            allImage[6].enabled = false;

            allImage[3].texture = entry.notes[0].noteTexture;

            cursorBasePosition.x = allImage[3].rectTransform.localPosition.x;
            cursorStartIndex = 3;
        }

        currentIndex = cursorStartIndex;
        cursorBasePosition.y = -500f;
        ResetCursor();
    }
}
