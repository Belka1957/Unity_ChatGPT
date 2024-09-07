using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatUIManager : MonoBehaviour
{
    public InputField inputField;
    public Button sendButton;
    public ScrollRect scrollView;
    public Transform content;

    public GameObject ChatMessage;
    public GameObject GPTMessage;

    private ConnectionChatGPT chatGPTConnection;
    private string apiKey = "SET_YOUR_API_KEY"; // ChatGPTのAPIキーを設定。
    private string systemMessage = 
        "ChatGPTの人格設定。";

    void Start()
    {
        chatGPTConnection = new ConnectionChatGPT(apiKey, systemMessage);

        sendButton.onClick.AddListener(OnSendButtonClick);
    }

    private async void OnSendButtonClick()
    {
        string userMessage = inputField.text;

        if (!string.IsNullOrEmpty(userMessage))
        {
            CreateMessage(userMessage);

            var response = await chatGPTConnection.RequestAsync(userMessage);

            string ChatGPTMessage = response.choices[0].message.content;

            CreateGPTMessage(ChatGPTMessage);

            inputField.text = "";
            Debug.Log(ChatGPTMessage);
            Canvas.ForceUpdateCanvases();
            scrollView.verticalNormalizedPosition = 0f;

        }
    }

    private void CreateMessage(string message)
    {
        GameObject newMessage = Instantiate(ChatMessage, content);

        Text messageText = newMessage.GetComponent<Text>();
        messageText.text = message;
    }
    private void CreateGPTMessage(string message)
    {
        GameObject newMessage = Instantiate(GPTMessage, content);

        Text messageText = newMessage.GetComponent<Text>();
        messageText.text = message;
    }
}
