using UnityEngine;

[CreateAssetMenu(fileName = "Audio Data", menuName = "Scriptable/Audio Data")]
public class AudioData : ScriptableObject
{
    public AudioClip buttonClickSound;
    public AudioClip bookFoundSound;
    public AudioClip scanningSound;
}
