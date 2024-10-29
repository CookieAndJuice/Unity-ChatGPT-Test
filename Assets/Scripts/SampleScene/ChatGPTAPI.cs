using OpenAI;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChatGPTAPI : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    [SerializeField] private Button button;
    [SerializeField] private GameObject textArea;

    public static Action<string> gptEvent;
    private float height;
    private OpenAIApi openai = new OpenAIApi();

    private List<ChatMessage> messages = new List<ChatMessage>();
    private string prompt = "Act as a random stranger in a chat room and reply to the questions. Don't break character. Don't ever mention that you are an AI model.";

    private void Start()
    {
        button.onClick.AddListener(SendReply);
        gptEvent += DescribeImage;
    }

    private async void SendReply()
    {
        var newMessage = new ChatMessage()
        {
            Role = "user",
            Content = ""
        };

        // Show question in text field
        // textArea.GetComponent<Text>().text = newMessage.Content;

        // Set the prompt
        if (messages.Count == 0) newMessage.Content = prompt + "\n";

        // Set the question
        newMessage.Content += inputField.text;
        messages.Add(newMessage);

        button.enabled = false;
        inputField.text = "";
        inputField.enabled = false;

        // Complete the instruction
        var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-4o",
            Messages = messages
        });

        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();

            messages.Add(message);
            textArea.GetComponent<Text>().text = message.Content;
        }
        else
        {
            Debug.LogWarning("No text was generated from this prompt.");
        }

        button.enabled = true;
        inputField.enabled = true;
    }

    private async void DescribeImage(string base64Path)
    {
        Debug.Log("Describe");

        var newMessage = new ChatMessage()
        {
            Role = "user",
            Content = ""
        };

        //{
        //    "type": "image_url",
        //  "image_url": {
        //        "url":  f"data:image/jpeg;base64,{base64_image}"
        //  },
        //}

        // Show question in text field
        // textArea.GetComponent<Text>().text = newMessage.Content;

        // Set the prompt
        if (messages.Count == 0) newMessage.Content = prompt + "\n";

        // Set the question
        newMessage.Content += "다음 이미지를 설명해 줘.\n" + "data:image/png;base64," + base64Path;
        messages.Add(newMessage);

        button.enabled = false;
        inputField.text = "";
        inputField.enabled = false;

        // Complete the instruction
        var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-4o",
            Messages = messages
        });

        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();

            messages.Add(message);
            textArea.GetComponent<Text>().text = message.Content;
        }
        else
        {
            Debug.LogWarning("No text was generated from this prompt.");
        }

        button.enabled = true;
        inputField.enabled = true;
    }
}