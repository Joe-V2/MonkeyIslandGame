using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class loadingGraphicsManager : MonoBehaviour
{

    public Image blackout;
    public GameObject logo;
    public AudioClip menuMusic;
    public AudioClip sfx;
    public AudioClip continueSfx;
    bool fadeIn = false;
    bool fadeOut = false;
    float fadeTime = 0f;

    private AudioSource musicPlayer;
    private AudioSource envSfxPlayer;
    private AudioSource cursorSfxPlayer;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += FadeBlinds;

        musicPlayer = gameObject.AddComponent<AudioSource>();
        envSfxPlayer = gameObject.AddComponent<AudioSource>();
        cursorSfxPlayer = gameObject.AddComponent<AudioSource>();

        musicPlayer.clip = menuMusic;
        musicPlayer.loop = true;
        envSfxPlayer.clip = sfx;
        envSfxPlayer.loop = true;
        envSfxPlayer.Play();
    }

    void FadeBlinds(Scene scene, LoadSceneMode loadMode)
    {
        if (scene == SceneManager.GetSceneByName("menu"))
            fadeIn = true;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= FadeBlinds;
    }
    private void FixedUpdate()
    {
        if (fadeIn || fadeOut) 
        {
            fadeTime += Time.deltaTime * 0.5f;

            float alpha = 0;
            if (fadeIn)
            {
                alpha = Mathf.Lerp(1.5f, 0, fadeTime);
                envSfxPlayer.volume = fadeTime;
            }
            else
            {
                alpha = Mathf.Lerp(0, 1.5f, fadeTime);
                envSfxPlayer.volume = (fadeTime * -1) + 1;
            }
            
            
            blackout.color = new Vector4(0, 0, 0, alpha);

            if (fadeTime >= 1)
            {

                musicPlayer.Play();

                fadeTime = 0;
                if (fadeOut)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                fadeIn = fadeOut = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        else if(Input.anyKeyDown && !fadeOut && !fadeIn)
        {
            cursorSfxPlayer.PlayOneShot(continueSfx);
            fadeOut = true;
        }
    }
}
