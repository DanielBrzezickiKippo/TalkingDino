using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class Translate : MonoBehaviour
{
	// Should we debug?
	public bool isDebug = true;
	// Here's where we store the translated text!
	private string translatedText = "";

	[SerializeField]
	private VoiceController voiceController;

	[SerializeField]
	private MainAIMLScript chatBot;

	// This is only called when the scene loads.
	void Start()
	{
		// Strictly for debugging to test a few words!
		//if (isDebug)
		//	StartCoroutine(Process("en", "Mam na imie Tom!"));
	}

	// We have use googles own api built into google Translator.
	public IEnumerator ProcessUserTalk(string targetLang, string sourceText)
	{
		// We use Auto by default to determine if google can figure it out.. sometimes it can't.
		string sourceLang = "auto";
		// Construct the url using our variables and googles api.
		string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
			+ sourceLang + "&tl=" + targetLang + "&dt=t&q=" + UnityWebRequest.EscapeURL(sourceText);

		using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
		{
			// Request and wait for the desired page.
			yield return webRequest.SendWebRequest();

			// Check to see if we don't have any errors.
			if (string.IsNullOrEmpty(webRequest.error))
			{
				// Parse the response using JSON.
				var N = JSONNode.Parse(webRequest.downloadHandler.text);
				// Dig through and take apart the text to get to the good stuff.
				translatedText = N[0][0][0];
				// This is purely for debugging in the Editor to see if it's the word you wanted.
				if (isDebug)
					Debug.Log(translatedText);

				chatBot.currentQuestion = translatedText;
				chatBot.SendQuestionToRobot();

			}
		}
	}

	public IEnumerator ProcessAndTalk(string targetLang, string sourceText)
	{
		// We use Auto by default to determine if google can figure it out.. sometimes it can't.
		string sourceLang = "auto";
		// Construct the url using our variables and googles api.
		string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
			+ sourceLang + "&tl=" + targetLang + "&dt=t&q=" + UnityWebRequest.EscapeURL(sourceText);

		using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
		{
			// Request and wait for the desired page.
			yield return webRequest.SendWebRequest();

			// Check to see if we don't have any errors.
			if (string.IsNullOrEmpty(webRequest.error))
			{
				// Parse the response using JSON.
				var N = JSONNode.Parse(webRequest.downloadHandler.text);
				// Dig through and take apart the text to get to the good stuff.
				translatedText = N[0][0][0];
				// This is purely for debugging in the Editor to see if it's the word you wanted.
				if (isDebug)
					Debug.Log(translatedText);

				voiceController.StartSpeaking(translatedText);
			}
		}
	}

	// Exactly the same as above but allow the user to change from Auto, for when google get's all Jerk Butt-y
	public IEnumerator Process(string sourceLang, string targetLang, string sourceText)
	{
		string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
			+ sourceLang + "&tl=" + targetLang + "&dt=t&q=" + UnityWebRequest.EscapeURL(sourceText);

		using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
		{
			// Request and wait for the desired page.
			yield return webRequest.SendWebRequest();

			// Check to see if we don't have any errors.
			if (string.IsNullOrEmpty(webRequest.error))
			{
				// Parse the response using JSON.
				var N = JSONNode.Parse(webRequest.downloadHandler.text);
				// Dig through and take apart the text to get to the good stuff.
				translatedText = N[0][0][0];
				// This is purely for debugging in the Editor to see if it's the word you wanted.
				if (isDebug)
					Debug.Log(translatedText);
			}
		}
	}
}
