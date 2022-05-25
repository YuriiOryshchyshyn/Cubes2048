using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFX;

    ParticleSystem.MainModule cubeExplosionFXMaimModule;

    public static FX Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cubeExplosionFXMaimModule = cubeExplosionFX.main;
    }

    public void PlayCubeExplosionFX(Vector3 position, Color color)
    {
        cubeExplosionFXMaimModule.startColor = new ParticleSystem.MinMaxGradient(
            new Color(color.r, color.g, color.b, 1f));
        cubeExplosionFX.transform.position = position;
        cubeExplosionFX.Play();
    }
}
