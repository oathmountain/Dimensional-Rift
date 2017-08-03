using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MidWaveText : MonoBehaviour {
    public SwordScript sword;
    public LevelController LC;
    private bool displayText;
    private Text text;
    private int CW;
    private int timer;
    private int score;


    public void waveComplete()
    {
		timer = (int)Time.time + 6;
        displayText = true;
        CW = LC.getCurrentLevel();
    }
    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (displayText && ((int)Time.time <= timer))
        {
            score = sword.getScore();
            text.text = "Wave " + CW.ToString() + " complete\n" + "Next wave in " + (timer - (int)Time.time) + ".." +  "\n\n" + "Score: " + score.ToString();
        }
        else
        {
            text.text = null;
            displayText = false;
        }
	}
}
