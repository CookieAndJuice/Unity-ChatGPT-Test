using OpenAI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ImageToChatGPT : MonoBehaviour
{
    [SerializeField] private Button imageSetButton;
    [SerializeField] private GameObject textArea;
    private string[] files;

    string imagePath;
    string imageBase64String;
    
    void Start()
    {
        imageSetButton.onClick.AddListener(SetImage);
        imagePath = Application.dataPath + "/Resources/ScreenShots/";

        // 디렉토리 내 png 파일 탐색, 배열에 각 파일 경로 저장
        files = Directory.GetFiles(imagePath, "*.png");
    }

    private void SetImage()
    {
        if (files.Length == 0)
        {
            imageBase64String = CameraCapture.capture();

            ChatGPTAPI.gptEvent(imageBase64String);
        }
        else
        {
            Debug.Log(files.Length);
            Debug.Log("Set Image : " + files[0]);

            byte[] fileData = File.ReadAllBytes(files[0]);
            imageBase64String = Convert.ToBase64String(fileData);

            ChatGPTAPI.gptEvent(imageBase64String);

            Debug.Log("Finish to set : " + imageBase64String);
        }
    }
}
