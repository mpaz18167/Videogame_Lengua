using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private List<QuizDatAScriptable> quizDataList;
    [SerializeField] private float timeLimit =30;
    
    private List<Question> questions;
    private Question selectedQuestion;
    private int scoreCount = 0;
    private float currentTime;
    private int lifeRemaining = 3;

    public GameStatus gameStatus = GameStatus.Next;

    public GameStatus GameStatus { get { return gameStatus; } }
    // Start is called before the first frame update
    public void StartGame(int index)
    {
        scoreCount = 0;
        currentTime = timeLimit;
        lifeRemaining = 3;

        questions = new List<Question>();   

        for (int i = 0;i< quizDataList[index].questions.Count; i++)
        {
            questions.Add(quizDataList[index].questions[i]);
        }
        
        SelectQuestion();

        gameStatus = GameStatus.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStatus == GameStatus.Playing)
        {
            currentTime -=Time.deltaTime;
            SetTimer(currentTime);
        }
    }
    void SelectQuestion()
    {
        int val = UnityEngine.Random.Range(0, questions.Count);
        selectedQuestion = questions[val];
        quizUI.SetQuestion(selectedQuestion);

        questions.RemoveAt(val);
    }

    private void SetTimer (float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUI.TimerText.text = "TIEMPO:" + time.ToString("mm':'ss"); 

        if (currentTime <= 0)
        {
            gameStatus = GameStatus.Next;
            quizUI.GameOverPanel.SetActive(true);
        }
    }
    
    public bool Answer(string answered)
    {
        bool correctAns = false;
        if (answered == selectedQuestion.correctAns)
        {
            correctAns = true;
            scoreCount += 50;
            quizUI.ScoreText.text = "PUNTAJE: " + scoreCount;
        }
        else
        {
            lifeRemaining--;
            quizUI.ReduceLife(lifeRemaining);

            if(lifeRemaining <=0)
            {
                gameStatus = GameStatus.Next;
                quizUI.GameOverPanel.SetActive(true);
            }
        }

        if (GameStatus == GameStatus.Playing)
        {
            if (questions.Count > 0)
            {
                Invoke("SelectQuestion", 0.4f);
            }
            else
            {
                gameStatus = GameStatus.Next;
                quizUI.GameOverPanel.SetActive(true);

            }
        }

        
        return correctAns;
    }
}


[System.Serializable]
public class Question
{
    public string questionInfo;
    public QuestionType questionType;
    public Sprite questionImg;
    public AudioClip questionClip;
    public UnityEngine.Video.VideoClip questionVideo;
    public List<string> options;
    public string correctAns;

}

[System.Serializable]

public enum QuestionType
{
    TEXT,
    IMAGE,
    VIDEO,
    AUDIO
}

[System.Serializable]

public enum GameStatus
{
    Next,
    Playing
}

