using UnityEngine;

[CreateAssetMenu(fileName = "Application Settings", menuName = "Scriptable/Application Settings")]
public class ApplicationSettings : ScriptableObject
{
    public float appCurrentVersion;
    public float appLiveVersion;
}
