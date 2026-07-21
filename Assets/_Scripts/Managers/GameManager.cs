using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    private List<ScaleEntry> currentEntry = new List<ScaleEntry>();
    private List<int> IDs;
    private List<int> playerInputs;

    [SerializeField]
    private GameObject minorEnemy;
    [SerializeField]
    private GameObject minorEnemyModified;

    [SerializeField]
    private GameObject majorEnemy;
    [SerializeField]
    private GameObject majorEnemyModified;

    [SerializeField]
    private List<GameObject> enemySpawnPoint;
    private List<GameObject> ennemies = new List<GameObject>();

    private float totalTime = 0f;
    private Timer timer;

    private bool wasModified = false;

    public bool gameEnded = false;

    void Awake() 
    {
        if(instance == null)
            instance = this;
    }

    void Start() 
    {
        for(int i = 0; i < 7; i++) 
        {
            CreateMonster(i);
        }

        StartCoroutine(StartGame());
    }

    void Update() 
    {
        if (timer == null) return;
        totalTime += Time.deltaTime;

        timer.UpdateTimer(Time.deltaTime);
    }

    private void CreateMonster(int i) 
    {
        ScaleEntry newEntry = ScaleRegistery.instance.CreateRandomChord();
        currentEntry.Add(newEntry);

        if (newEntry.isMinor)
            ennemies.Add(Instantiate(minorEnemy, enemySpawnPoint[i].transform.position, Quaternion.identity));
        else
            ennemies.Add(Instantiate(majorEnemy, enemySpawnPoint[i].transform.position, Quaternion.identity));
    }
    
    public IEnumerator StartGame() 
    {
        yield return new WaitForSeconds(3f);
        UIManagers.instance.pannelGroup.alpha = 1f;
        PlayQTE();
    }

    public void PlayQTE() 
    {
        timer = new Timer();
        timer.RecalculateTimer(totalTime);

        UIManagers.instance.ShowInput(currentEntry[0]);

        IDs = new List<int>();
        playerInputs = new List<int>();

        foreach (var i in currentEntry[0].notes)
            IDs.Add(i.ID);
    }

    public void CheckQTE(int value, bool playerMode, PlayerController controller) 
    {
        if (currentEntry[0] == null) return;

        if(playerMode == currentEntry[0].isMinor) 
        {
            SoundManager.instance.PlaySoundAtLocation("xd", transform.position, 0.2f);
            ScoreManager.instance.AddScore(-50f);
            UIManagers.instance.ResetCursor();
            controller.PlayAnimation(FightState.FAILURE);
            timer.ReduceTime(0.5f);
            playerInputs.Clear();
            return;
        }

        playerInputs.Add(value);

        if (playerInputs[playerInputs.Count - 1] == IDs[playerInputs.Count - 1]) 
        {
            InverseScale(playerInputs.Count == IDs.Count);
            controller.PlayAnimation(FightState.SUCCESS);
            UIManagers.instance.IncrementCursor();
        }
        else
        {
            ScoreManager.instance.AddScore(-50f);
            SoundManager.instance.PlaySoundAtLocation("xd", transform.position, 0.2f);
            playerInputs.Clear();
            UIManagers.instance.ResetCursor();
            controller.PlayAnimation(FightState.FAILURE);
            timer.ReduceTime(1f);
        }

        if (playerInputs.Count == IDs.Count) 
        {
            controller.PlayAnimation(FightState.SUCCESS);
            ScoreManager.instance.AddScore(150f);

            ReorganiseMonster();

            currentEntry.RemoveAt(0);

            wasModified = false;
            CreateMonster(6);
            PlayQTE();
        }

    }

    private void ReorganiseMonster() 
    {
        Destroy(ennemies[0]);
        ennemies.RemoveAt(0);
        int index = 0;
        foreach (var entity in ennemies) 
        {
            entity.transform.position = enemySpawnPoint[index].transform.position;
            index++;
        }
    }

    private void InverseScale(bool finished) 
    {
        if (wasModified || finished) return;

        bool changed = Random.value < 0.3f;

        if (!changed) return;

        currentEntry[0].isMinor = !currentEntry[0].isMinor;

        if (currentEntry[0].isMinor) 
        {
            Destroy(ennemies[0]);
            ennemies[0] = Instantiate(majorEnemyModified, enemySpawnPoint[0].transform.position, Quaternion.identity);
        }

        else
        {
            Destroy(ennemies[0]);
            ennemies[0] = Instantiate(minorEnemyModified, enemySpawnPoint[0].transform.position, Quaternion.identity);
        }

        wasModified = true;
    }

    public void EndGame() 
    {
        timer = null;
        gameEnded = true;
        GameOver over = new GameOver();
        over.CreateGameOver(ScoreManager.instance.GetScore());
    }
}
