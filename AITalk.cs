using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class AITalk
{
    private readonly string _username;
    private readonly string _password;

    public AITalk(string username, string password)
    {
        _username = username;
        _password = password;
    }

    public async UniTask<AudioClip> RequestAsync(string text, string speakerName)
    {
        var apiUrl = "https://webapi.aitalk.jp/webapi/v5/ttsget.php";

        var form = new WWWForm();
        form.AddField("username", _username);
        form.AddField("password", _password);
        form.AddField("speaker_name", speakerName);
        form.AddField("text", text);

        using UnityWebRequest request = UnityWebRequest.Post(apiUrl, form);
        request.downloadHandler = new DownloadHandlerAudioClip(apiUrl, AudioType.OGGVORBIS);

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            throw new Exception("Request failed.");
        }

        var audioClip = DownloadHandlerAudioClip.GetContent(request);
        return audioClip;
    }
}

