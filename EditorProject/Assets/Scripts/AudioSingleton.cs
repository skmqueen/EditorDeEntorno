using UnityEngine;

public class AudioSingleton : MonoBehaviour
{
    public static AudioSingleton Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip musicaFondo;
    public AudioClip sonidoClick;
    public AudioClip sonidoCancelar;
    public AudioClip sonidoColocar;

  private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Reproducir música de fondo en bucle
        PlayMusic();
    }

    // ====== Funciones para reproducir sonidos ======
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayClick()      { PlaySFX(sonidoClick); }
    public void PlayCancelar()   { PlaySFX(sonidoCancelar); }
    public void PlayColocar()    { PlaySFX(sonidoColocar); }

    public void PlayMusic(bool loop = true)
    {
        if (musicaFondo == null) return;

        musicSource.clip = musicaFondo;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}