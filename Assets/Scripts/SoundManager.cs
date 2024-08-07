using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource source;
    private bool soundOn = true;
    [SerializeField] Button soundButton;
    [SerializeField] Sprite soundImageOn;
    [SerializeField] Sprite soundImageOff;

    private void Awake()
    {
        source = GetComponent<AudioSource>();


        //Keep this object even when we go to a new scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //destroy duplicate gameobjects
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void ToggleSound()
    {
        if (soundOn)
        {
            source.gameObject.SetActive(false);
        }
        else
        {
            source.gameObject.SetActive(true);
        }
        soundOn = !soundOn;
        soundButton.image.sprite = soundOn ? soundImageOn : soundImageOff;
    }
}
