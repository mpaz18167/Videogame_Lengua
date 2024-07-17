using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI inputscore;
    public TMP_InputField inputname;

    public UnityEvent<string, int> submitScoreEvent;
    public void SubmitScore() {

        submitScoreEvent.Invoke(inputname.text,int.Parse(inputscore.text));
    }
}
