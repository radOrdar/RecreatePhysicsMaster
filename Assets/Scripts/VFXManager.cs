using Cinemachine;
using UnityEngine;


public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private ParticleSystem spawnFx;
    [SerializeField] private ParticleSystem explosionFx;

    public void PlaySpawnFx(Vector3 pos)
    {
        Instantiate(spawnFx, pos, Quaternion.identity);
    }

    public void PlayExplosionFX(Vector3 pos)
    {
        Instantiate(explosionFx, pos, Quaternion.identity);
        impulseSource.GenerateImpulse();
    }
}