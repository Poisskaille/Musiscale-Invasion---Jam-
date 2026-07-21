using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private List<AudioClip> tempGeneralAudio = new List<AudioClip>();
    private Dictionary<string,AudioClip> generalAudio = new Dictionary<string,AudioClip>();

    public AudioSource _audio;
    
    void Awake() 
    {
        if (instance == null)
            instance = this;
    }

    void Start() 
    {
        foreach (var clip in tempGeneralAudio)
            generalAudio.Add(clip.name, clip);

        _audio = GetComponent<AudioSource>();
        _audio.Play();
    }
    
    public void PlaySoundAtLocation(AudioClip _audio, Vector3 position, float volume = 1) 
    {
        float _lenght = _audio.length;

        var newAudio = Instantiate(audioSource, position, Quaternion.identity);
        newAudio.resource = _audio;
        newAudio.volume = volume;
        newAudio.Play();

        Destroy(newAudio.gameObject, _lenght);
    }

    public void PlaySoundAtLocation(string _audioName, Vector3 position, float volume = 1)
    {
        AudioClip _audio = GetGeneralAudio(_audioName);
        float _lenght = _audio.length;

        var newAudio = Instantiate(audioSource, position, Quaternion.identity);
        newAudio.resource = _audio;
        newAudio.volume = volume;
        newAudio.Play();

        Destroy(newAudio.gameObject, _lenght);
    }

    public AudioClip GetGeneralAudio(string name)
    {  return generalAudio[name]; }
}
