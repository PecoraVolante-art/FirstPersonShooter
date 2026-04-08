using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

[RequireComponent(typeof(AudioSource))]
public class GestioneSFX : MonoBehaviour
{
    public static GestioneSFX Instance;

    [Header("Gameplay")] //categorie degli sfx
    public AudioClip death;
    public AudioClip shoot;
    public AudioClip destroy;
    public AudioClip reload;


    [Range(0f, 1f)]
    public float volume = 1f;

    private AudioSource audioSource;

    void Awake()
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

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; //non ripete e non parte da solo 
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; //audio2d

        if (PlayerPrefs.HasKey("SFXVolume")) // carichi il volume sfx salvato oppure ne crei uno nuovo
            volume = PlayerPrefs.GetFloat("SFXVolume");
        else
            PlayerPrefs.SetFloat("SFXVolume", volume);

        audioSource.volume = volume; //applichi il volume
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        Debug.Log("GestioneSFX attivo in scena: " + scene.name);
    }

    public void PlayDeath()
    {
        PlaySFX(death);
    }

    public void PlayShoot()
    {
        PlaySFX(shoot);
    }

    public void PlayDestroy()
    {
        PlaySFX(destroy);
    }

    public void PlayReload()
    {
        PlaySFX(reload);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;
        audioSource.PlayOneShot(clip, volume); //suona la clip senza interrompere le gli altri audio in base al volume
    }


    public void AddSFXToButton(Button button, AudioClip clip)
    {
        if (button == null || clip == null) return;

        button.onClick.AddListener(() => PlaySFX(clip)); //per ogni click riproduce sfx
    }

    // Cambia volume e salva
    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume); // salva il volume sfx
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

