using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static public ScoreManager instance;

    private float currentScore = 0f;

    void Awake() 
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        currentScore += Time.deltaTime * 5;
        UIManagers.instance.scoreTXT.text = ((int)currentScore).ToString();
    }

    public void AddScore(float addedScore) => currentScore += addedScore;

    public float GetScore() => currentScore;
}
