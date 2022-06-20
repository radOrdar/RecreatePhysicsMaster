using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] private AudioSource backMusicSource;
    [SerializeField] private AudioClip normalBackMusic;
    [SerializeField] private AudioClip reverseBackMusic;

    [SerializeField] private AudioSource[] spawnAudioSourcePool;
    [SerializeField] private AudioClip[] spawnClips;

    [SerializeField] private AudioClip explosionAudio;

    private void OnEnable()
    {
        PhysicsManager.OnGravityChanged += HandleGravityChanged;
        PhysicsManager.OnSimulationToggle += HandleSimulationToggle;
    }

    private void OnDisable()
    {
        PhysicsManager.OnGravityChanged -= HandleGravityChanged;
        PhysicsManager.OnSimulationToggle -= HandleSimulationToggle;
    }

    public void PlaySpawnAudio(Vector3 pos)
    {
        var freeSource = PickAudioSource();
        freeSource.transform.position = pos;
        freeSource.PlayOneShot(spawnClips[Random.Range(0, spawnClips.Length)]);
    }

    public void PlayExplosionAudio(Vector3 pos)
    {
        AudioSource freeSource = PickAudioSource();
        freeSource.transform.position = pos;
        freeSource.PlayOneShot(explosionAudio);
    }

    private AudioSource PickAudioSource()
    {
        AudioSource freeSource = spawnAudioSourcePool.FirstOrDefault(a => !a.isPlaying);
        if (freeSource == null) freeSource = spawnAudioSourcePool[0];
        return freeSource;
    }

    private void HandleGravityChanged(PhysicsManager.GravityMode gravityMode)
    {
        backMusicSource.clip = gravityMode == PhysicsManager.GravityMode.Normal ? normalBackMusic : reverseBackMusic;
        backMusicSource.Play();
    }

    private void HandleSimulationToggle(bool enabled)
    {
        if (enabled)
        {
            backMusicSource.pitch = 1f;
            backMusicSource.volume = 1f;
        } else
        {
            backMusicSource.pitch = 0.5f;
            backMusicSource.volume = 0.5f;
        }
    }
}