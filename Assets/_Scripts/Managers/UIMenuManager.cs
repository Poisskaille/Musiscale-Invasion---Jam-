using UnityEngine;

public class UIMenuManager : MonoBehaviour
{
    static public UIMenuManager instance;

    public CanvasGroup wholeUIMenuGroup;
    public CanvasGroup creditGroup;
    public CanvasGroup howToPlayGroup;

    void Awake() 
    {
        if (instance == null)
            instance = this;
    }

    void Start() 
    {
        OpenMenu("MainMenu");
    }

    public void OpenMenu(string mode) 
    {
        switch (mode) 
        {
            case "MainMenu":
                EnableGroup(wholeUIMenuGroup, true);
                EnableGroup(creditGroup, false);
                EnableGroup(howToPlayGroup, false);
                break;
            case "HowTo":
                EnableGroup(wholeUIMenuGroup, false);
                EnableGroup(creditGroup, false);
                EnableGroup(howToPlayGroup, true);
                break;
            case "Credits":
                EnableGroup(wholeUIMenuGroup, false);
                EnableGroup(creditGroup, true);
                EnableGroup(howToPlayGroup, false);
                break;
        }
    }
    
    private void EnableGroup(CanvasGroup group, bool value) 
    {
        group.alpha = value ? 1 : 0;
        group.interactable = value;
        group.blocksRaycasts = value;
    }

}
