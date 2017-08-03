using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScriptCombo : MonoBehaviour {

	private Text text;
	private string comboText;
	public SwordScript sword;
	public GameObject textObject;
    private Animator animator;

    public void spawnCombo(){
        textObject.SetActive(true);
        text.text = "x" + comboText + " Combo!";       
        animator.speed = 1.0f;                                              //Activate animation at normal speed
    }

    public void spawnComboBreak()
    {
        textObject.SetActive(true);
        text.text = "Combobreak!";
        animator.speed = 0.5f;                                              //Activate animation at half speed
    }

    public GameObject getTextObject()
    {
        return textObject;
    }

	// Use this for initialization
	void Start () {
        textObject.SetActive(false);
        text = GetComponent<Text>();
        animator = GetComponent<Animator>();
        animator.speed = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
		comboText = sword.getComboMultiplier().ToString();      
    }
}
