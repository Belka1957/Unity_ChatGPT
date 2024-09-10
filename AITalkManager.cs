using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AITalkManager : MonoBehaviour
{
    public InputField inputField;
    public Button sendButton;

    public AudioSource audioSource;

    private AITalk aiTalk;
    private string username = "spajam2024";
    private string password = "gGLgPWBp";
    public string speakerName = "akane_west_emo";
    void Start()
    {
        aiTalk = new AITalk(username, password);
        sendButton.onClick.AddListener(OnSendButtonClick);
    }

    private async void OnSendButtonClick()
    {
        string text = inputField.text;

        if (!string.IsNullOrEmpty(text))
        {
            AudioClip clip = await aiTalk.RequestAsync(text, speakerName);

            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
