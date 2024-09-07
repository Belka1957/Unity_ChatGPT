using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ConnectionChatGPT
{
    //API�L�[
    private readonly string _apiKey;
    //��b������ێ����郊�X�g
    private readonly List<ChatGPTMessageModel> _messageList = new List<ChatGPTMessageModel>();

    public ConnectionChatGPT(string apiKey, string settingText)
    {
        _apiKey = apiKey;
        //AI�̉����̃v�����v�g���L�^
        _messageList.Add(
            new ChatGPTMessageModel() { role = "system", content = settingText });
    }

    public async UniTask<ChatGPTResponseModel> RequestAsync(string userMessage)
    {
        //���͐���AI��API�̃G���h�|�C���g��ݒ�
        var apiUrl = "https://api.openai.com/v1/chat/completions";

        //���[�U�[����̃��b�Z�[�W���L�^
        _messageList.Add(new ChatGPTMessageModel { role = "user", content = userMessage });

        //OpenAI��API���N�G�X�g�ɕK�v�ȃw�b�_�[����ݒ�
        var headers = new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + _apiKey},
                {"Content-type", "application/json"}
            };


        //���p���郂�f����g�[�N������A�v�����v�g���I�v�V�����ɐݒ�
        var options = new RequestModel()
        {
            model = "gpt-3.5-turbo",
            messages = _messageList,
            max_tokens = 200,
            top_p = 1
        };
        var jsonOptions = JsonConvert.SerializeObject(options);

        //HTTP�iPOST�j�̏���ݒ�
        using UnityWebRequest request = new UnityWebRequest(apiUrl, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOptions)),
            downloadHandler = new DownloadHandlerBuffer()
        };

        //HTTP�w�b�_�[��ݒ�
        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        //�����Ń��N�G�X�g���M
        await request.SendWebRequest();

        //�G���[�̎�
        if (request.result == UnityWebRequest.Result.ConnectionError ||
           request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            throw new Exception();
        }
        else
        {
            Debug.Log("�G���[�͂Ȃ��͂�����!");
            var responseString = request.downloadHandler.text;


            var responseObject = JsonConvert.DeserializeObject<ChatGPTResponseModel>(responseString);
            _messageList.Add(responseObject.choices[0].message);

            Debug.Log("Parsed response with Newtonsoft: " + JsonConvert.SerializeObject(responseObject, Formatting.Indented));

            // _messageList�ɂ͍��܂ł̂��Ƃ肪�ǉ�����Ă������߁A��Ɉ�O�̂����̂ݕێ����Ă����悤�ɂ���
            // _messageList[0]�ɂ́AAI�L�����̐ݒ�����Ă���v�����v�g�������Ă���
            if (_messageList.Count > 3)
            {
                _messageList.RemoveRange(1, 2);
            }
            return responseObject;
        }
    }
}

