using UnityEngine;

[CreateAssetMenu(fileName ="GameSettings", menuName = "ScriptableObjects/Create Game Settings", order = 1)]
public class GameSettingsDatabase : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject targetPrefab;

    [Header("AudioClips")]
    public AudioClip targetHitSound;
    public AudioClip pullSound;
    public AudioClip shootSound;
    public AudioClip impactSound;
    public AudioClip restartSound;
    public AudioClip musicSound;
}
