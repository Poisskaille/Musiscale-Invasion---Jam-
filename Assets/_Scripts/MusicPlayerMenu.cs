using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayerMenu : MonoBehaviour
{
    public List<RawImage> monstersPNG;

    private Color32 savedColor = new Color(200f,200f,200f);
    private Color32 shadowColor = new Color(138f, 138f, 138f);
    void Start()
    {
        GetComponent<AudioSource>().Play();

        StartCoroutine(PlaySounds());
    }


    private IEnumerator PlaySounds() 
    {
        yield return new WaitForSeconds(Random.Range(1f, 10f));

        Debug.Log(savedColor);
        monstersPNG[Random.Range(0, monstersPNG.Count)].color = savedColor;

        ScaleEntry newChord = ScaleRegistery.instance.CreateRandomChord();
        foreach(var notes in newChord.notes) 
        {
            AudioSource.PlayClipAtPoint(notes.minorSound, transform.position, 0.4f);
            yield return new WaitForSeconds(Random.Range(0f,0.2f));
        }

        monstersPNG[Random.Range(0, monstersPNG.Count)].color = shadowColor;

        StartCoroutine(PlaySounds());
    }



}
