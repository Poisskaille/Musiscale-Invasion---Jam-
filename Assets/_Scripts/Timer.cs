using UnityEngine;
using UnityEngine.UI;

public class Timer
{
    private float barpercent = 1f;

    private float totalTime = 15f;
    private float currentTime = 0f;
    public void UpdateTimer(float dt)
    {
        currentTime += dt;

        barpercent = 1 - currentTime / totalTime;
        UIManagers.instance.timerBar.size = barpercent;

        if (currentTime > totalTime)
            GameManager.instance.EndGame();
    }

    public void RecalculateTimer(float _totalTime) 
    {
        if (_totalTime > 60f)
            totalTime = 12.5f;

        if (_totalTime > 120f)
            totalTime = 10f;

        if (_totalTime > 180f)
            totalTime = 7.5f;

        if (_totalTime > 240f)
            totalTime = 5f;
    }

    public void ReduceTime(float time) => currentTime += time;
}
