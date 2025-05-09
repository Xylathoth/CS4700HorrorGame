using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Initial Music")]
    public AudioClip initialAmbientMusic;

    [Header("Sound Effects")]
    public AudioClip roundStartSFX;
    public AudioClip flashlightClickSFX;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persist between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (initialAmbientMusic != null)
        {
            Debug.Log("Playing ambient music: " + initialAmbientMusic.name);
            PlayMusic(initialAmbientMusic);
        } else
        {

        }
    }

    public void PlayMusic(AudioClip newClip)
    {
        if (musicSource.clip == newClip) return;
        musicSource.clip = newClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
