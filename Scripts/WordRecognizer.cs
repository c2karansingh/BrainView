using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;
using HoloToolkit.Unity.InputModule;
public class WordRecognizer : MonoBehaviour
{
    DictationRecognizer wordRecog;
    public GameObject textMeshPro;
    public string stopword = "stop";
    
    
    void OnEnable()
    {
        wordRecog = new DictationRecognizer(ConfidenceLevel.Low, DictationTopicConstraint.Dictation);
        wordRecog.DictationResult += WordRecog_DictationResult;
        PhraseRecognitionSystem.Shutdown();
        wordRecog.Start();
        Debug.Log("Enabled");
        textMeshPro.GetComponent<TextMeshPro>().text = "Listening...";
    }

    private void WordRecog_DictationResult(string text, ConfidenceLevel confidence)
    {
        if(text.Contains(stopword))
        {
            wordRecog.Stop();
            wordRecog.Dispose();
            PhraseRecognitionSystem.Restart();
            gameObject.SetActive(false);
            Debug.Log("restarting system...");
            return;
        }
        textMeshPro.GetComponent<TextMeshPro>().text = text;
    }
}
