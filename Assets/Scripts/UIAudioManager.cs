using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public AudioSource buttonClickAudio;
    public AudioSource menuMusic;
    public AudioSource backgroundMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonClickAudio()
    {
        buttonClickAudio.PlayOneShot(buttonClickAudio.clip);
    }

    public void PlayMainMenuMusic()
    {
        if (!menuMusic.isPlaying)
            menuMusic.Play();
        if (backgroundMusic.isPlaying)
            backgroundMusic.Stop();
    }

    public void PlayGameplayMusic()
    {
        if (!backgroundMusic.isPlaying)
            backgroundMusic.Play();
        if (menuMusic.isPlaying)
            menuMusic.Stop();
    }

    public void PauseMusic()
    {
        if (menuMusic.isPlaying)
            menuMusic.Stop();
        if (backgroundMusic.isPlaying)
            backgroundMusic.Stop();
    }
}
