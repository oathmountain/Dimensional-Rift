using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class HighScore : MonoBehaviour
{
	private int highScore;
	public SwordScript sword;
    private HighScoreStruct[] easyHighScore, normalHighScore, hardHighScore,insaneHighScore, HS;    
    private string stringToEdit = "";
    private bool initialised, activateTextField, submittedText, scoreChanged;
    
    public int getHighScore () //Getter for sending HighScore to the UI
	{
		return highScore;
	}

    void initialise()//this will only run the first time the game is started on a new computer
    {
        
        if (!initialised)
        {
            print("initialising highscore");
            HighScoreStruct init = new HighScoreStruct("Default", 0);
            easyHighScore = new HighScoreStruct[10];
            for (int i = 0; i < 10; i++)
            {
                easyHighScore.SetValue(init, i);
            }
            normalHighScore = new HighScoreStruct[10];
            for (int i = 0; i < 10; i++)
            {
                normalHighScore.SetValue(init, i);
            }
            hardHighScore = new HighScoreStruct[10];
            for (int i = 0; i < 10; i++)
            {
                hardHighScore.SetValue(init, i);
            }
            insaneHighScore = new HighScoreStruct[10];
            for (int i = 0; i < 10; i++)
            {
                insaneHighScore.SetValue(init, i);
            }
            HS = new HighScoreStruct[10];
            for (int i = 0; i < 10; i++)
            {
                HS.SetValue(init, i);
            }
            initialised = true;
        }
    }
    void OnGUI()
    {
        if (activateTextField)
        {
            print("gui text for score on screen");
            stringToEdit = GUI.TextField(new Rect(Screen.width / 2, Screen.height / 2,200, 200), stringToEdit, 15);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                submittedText = true;
                activateTextField = false;
            }
        }
    }
    
    void checkHighScore()
    {
        print("check highscore called");
        switch (DifficultyLevel.getDifficultyLevel())
        {
            case 2:
                HS = easyHighScore;
                calculateHighScore();
                easyHighScore = HS;
                break;
            case 3:
                HS = normalHighScore;
                calculateHighScore();
                normalHighScore = HS;
                break;
            case 4:
                HS = hardHighScore;
                calculateHighScore();
                hardHighScore = HS;
                break;
            case 5:
                HS = insaneHighScore;
                calculateHighScore();
                insaneHighScore = HS;
                break;
        }
    }

    void calculateHighScore()
    {
        int score = sword.getScore();
        print("score set");
        for (int i = 0; i < 10; i++)
        {
            print("current lap " + i);
            print(HS.GetValue(i));
                
            if (score > ((HighScoreStruct)HS.GetValue(i)).getScore()) //check if current score is higher than the old score
            {
                scoreChanged = true;                    
                activateTextField = true;
                //while (!submittedText) { }
                HighScoreStruct temp = new HighScoreStruct(stringToEdit, score);
                print("created new highscorestruct");
                HighScoreStruct temp2 = (HighScoreStruct)HS.GetValue(i);
                HS.SetValue(temp, i);

                for (int y = i+1; y < 10; y++)
                {                      
                    temp = (HighScoreStruct)HS.GetValue(y);
                    HS.SetValue(temp2, y);
                    temp2 = temp;
                }
                i = 10;
            }
        }
    }

    public void Save ()         //Saves the highscore in binary to a .dat file
	{
        print("save called");
        checkHighScore();
		if (scoreChanged) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/HighScore.dat");      //Create the save file on disk
	
			ScoreData data = new ScoreData ();
            data.easyHighScore = easyHighScore;
            data.normalHighScore = normalHighScore;
            data.hardHighScore = hardHighScore;
            data.insaneHighScore = insaneHighScore;
            data.initialised = initialised;

			bf.Serialize (file, data);
			file.Close ();
            scoreChanged = false;
            print("Saved score");
		}
	}

	public void Load ()         //Loads and decodes the binary file into the highScore variable
	{
        print("loading highscore");
		if (File.Exists (Application.persistentDataPath + "/HighScore.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/HighScore.dat", FileMode.Open); //Load the save file from disk
            ScoreData data = (ScoreData)bf.Deserialize (file);
			file.Close ();

            easyHighScore = data.easyHighScore;
            normalHighScore = data.normalHighScore;
            hardHighScore = data.hardHighScore;
            insaneHighScore = data.insaneHighScore;
            initialised = data.initialised;
            print("Highscore Loaded");

			//Debug.Log (Application.persistentDataPath); //Print save location to log
		}
        initialise();
	}
		
}

[Serializable]
class ScoreData //Serializable class for storing high score
{
    public HighScoreStruct[] easyHighScore;
    public HighScoreStruct[] normalHighScore;
    public HighScoreStruct[] hardHighScore;
    public HighScoreStruct[] insaneHighScore;
    public bool initialised;
}