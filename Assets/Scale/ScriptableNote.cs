using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ScriptableNote", menuName = "Scriptable Objects/ScriptableNote")]
public class ScriptableNote : ScriptableObject
{
    public int ID;

    public Texture2D noteTexture;

    public AudioClip minorSound;
    public AudioClip majorSound;
}
