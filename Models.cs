using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChatGPTMessageModel
{
    public string role;
    public string content;
    public object refusal;
}

[Serializable]
public class RequestModel
{
    public string model;
    public List<ChatGPTMessageModel> messages;
    public int max_tokens;
    public int top_p;
}

[Serializable]
public class ChatGPTResponseModel
{
    public string id;
    public string @object;
    public int created;
    public Choice[] choices;
    public Usage usage;
    public string system_fingerprint;

    [Serializable]
    public class Choice
    {
        public int index;
        public ChatGPTMessageModel message;
        public string finish_reason;
        public object logprobs;
    }

    [Serializable]
    public class Usage
    {
        public int prompt_tokens;
        public int completion_tokens;
        public int total_tokens;
    }
}