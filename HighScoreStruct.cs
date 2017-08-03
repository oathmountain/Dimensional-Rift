using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class HighScoreStruct {
    public int score;
    public string playerName;

    public HighScoreStruct(string n, int s)
    {
        playerName = n;
        score = s;
    }
    public int getScore()
    {
        return score;
    }
    public string getName()
    {
        return playerName;
    }
    public override String ToString()
    {
        return playerName +" " + score;
    }
}
