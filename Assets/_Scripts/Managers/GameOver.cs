using UnityEngine;

public class GameOver
{
    
    public void CreateGameOver(float score) 
    {
        SoundManager.instance._audio.Stop();
        SoundManager.instance.PlaySoundAtLocation("GameOver", Vector3.zero, 1f);

        UIManagers ui = UIManagers.instance;

        ui.wholeUI.alpha = 0;
        ui.wholeUI.blocksRaycasts = false;
        ui.wholeUI.interactable = false;

        ui.gameOverGroup.alpha = 1;
        ui.gameOverGroup.blocksRaycasts = true;
        ui.gameOverGroup.interactable = true;

        ui.scoreGameOver.text = score.ToString();
    }
}
