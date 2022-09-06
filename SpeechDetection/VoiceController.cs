using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;
using TMPro;
using UnityEngine.Android;

public class VoiceController : MonoBehaviour
{
    public string LANG_CODE = "en-US";

    [SerializeField]
    private TextMeshProUGUI uiText;
    [SerializeField]
    private MainAIMLScript chatBot;
    [SerializeField]
    private Translate translate;


    private void Start()
    {
        Setup(LANG_CODE);

    #if UNITY_ANDROID
        SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
    #endif

        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;

        CheckPermission();
    }

    void CheckPermission()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    #endif
    }

    #region Text to Speech
    public void StartSpeaking(string message)
    {
        TextToSpeech.Instance.StartSpeak(message);
    }

    public void EndSpeaking()
    {
        TextToSpeech.Instance.StopSpeak();
    }

    void OnSpeakStart()
    {
        Debug.Log("Talking started...");
    }

    void OnSpeakStop()
    {
        Debug.Log("Talking stopped");
    }

    #endregion

    #region Speech to Text

    public void StartListening()
    {
        SpeechToText.Instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.Instance.StopRecording();
    }

    void OnFinalSpeechResult(string result)
    {
        // uiText.text = result;
        StartCoroutine(translate.ProcessUserTalk("en",result));

        //chatBot.currentQuestion = translated;
        //chatBot.SendQuestionToRobot();
    }

    void OnPartialSpeechResult(string result)
    {
        //uiText.text = result;
    }

    #endregion

    public void Setup(string code)
    {
        TextToSpeech.Instance.Setting(code, 1f, 1f);
        SpeechToText.Instance.Setting(code);
    }



}
