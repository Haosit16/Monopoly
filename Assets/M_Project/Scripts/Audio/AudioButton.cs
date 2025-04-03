using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    public List<Button> buttons;
    public AudioClip click;
    public AudioSource audioSource;
    private void Start()
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(PlayClick);
        }
    }
    private void PlayClick()
    {
        audioSource.PlayOneShot(click);
    }
}
