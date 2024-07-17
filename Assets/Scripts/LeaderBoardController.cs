using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using static UnityEditor.Progress;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderBoardController : MonoBehaviour
{
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> scores;
    private string publicKey = "c0b87afc086a522e6235a60b4a74f9890ea4746b753f490a333ad8d64a929d45"; //Minijuego1 Nivel 1

    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) =>
        {
            int loopLenght = (msg.Length < names.Count) ? msg.Length :names.Count;

            for (int i = 0; i < loopLenght; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderBoardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicKey, username, score,((msg)=>
        {
            GetLeaderboard();
        }));
        
    }

    

}
