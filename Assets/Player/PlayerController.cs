using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum FightState { SUCCESS, FAILURE}

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer _renderer;

    [SerializeField]
    private InputActionReference switchMode;

    [SerializeField]
    private List<InputActionReference> inputActions;

    private bool isMinor = true;

    [Header("Textures")]
    [SerializeField]
    private Sprite baseSprite;

    [SerializeField]
    private List<Sprite> fightTexture;

    [SerializeField]
    private Sprite failTexture;

    void Start() 
    {
        _renderer = GetComponent<SpriteRenderer>();

        StartCoroutine(WaitForGame());
    }

    private IEnumerator WaitForGame() 
    {
        yield return new WaitForSeconds(3.01f);
        for (int i = 0; i < inputActions.Count; i++)
            StartCoroutine(WaitForInput(inputActions[i], i));
    }

    private void OnEnable()
    {
        switchMode.action.Enable();

        foreach (var action in inputActions)
            action.action.Enable();
    }

    void Update()
    {
        SwitchMode();
        CheckEndGame();
    }

    private void SwitchMode() 
    {
        if (switchMode.action.WasPressedThisFrame()) { 
            isMinor = !isMinor;
            UIManagers.instance.ChangeMode(isMinor);
        }
    }

    private void CheckEndGame() 
    {
        if(GameManager.instance.gameEnded)
        {
            foreach (var action in inputActions)
                action.action.Disable();


            StopAllCoroutines();
        }
    }
    private IEnumerator WaitForInput(InputActionReference _action, int index) 
    {
        yield return new WaitUntil(() => _action.action.WasPressedThisFrame());
        SoundManager.instance.PlaySoundAtLocation(isMinor ? ScaleRegistery.instance.GetNote(index).minorSound : ScaleRegistery.instance.GetNote(index).majorSound, transform.position, 0.5f);
        GameManager.instance.CheckQTE(index,isMinor,this);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(WaitForInput(_action, index));
    }

    public void PlayAnimation(FightState state) 
    {
        switch (state) 
        {
            case FightState.SUCCESS:
                StartCoroutine(SetSprite(fightTexture[Random.Range(0, fightTexture.Count)]));
                break;
            case FightState.FAILURE:
                StartCoroutine(SetSprite(failTexture));
                break;
        }
    }

    private IEnumerator SetSprite(Sprite newSprite) 
    {
        _renderer.sprite = newSprite;
        yield return new WaitForSeconds(0.3f);
        _renderer.sprite = baseSprite;
    }

}
