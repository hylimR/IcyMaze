using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

//Master script to control current scene
public class CMasterScript : MonoBehaviour {

    public GameObject winScreen;
    public GameObject instruction;
    public GameObject startButton, showInstructButton;
    public GameObject mCircle1, mCircle2, mCircle3, mCircle4;
    MagicCircleScript s1, s2, s3, s4;

	void Start () {
        //Pause the game at start screen
        Time.timeScale = 0;
        s1 = mCircle1.GetComponent<MagicCircleScript>();
        s2 = mCircle2.GetComponent<MagicCircleScript>();
        s3 = mCircle4.GetComponent<MagicCircleScript>();
        s4 = mCircle4.GetComponent<MagicCircleScript>();
        //hide win screen
        winScreen.SetActive(false);
        UnityEngine.Events.UnityAction action = () => { ShowInstruction(); };
        startButton.GetComponent<Button>().onClick.AddListener(action);

        //Hide this button by default
        showInstructButton.SetActive(false);
        showInstructButton.GetComponent<Button>().onClick.AddListener(action);
    }
	
	void Update () {
        if (IsPuzzleComplete())
        {
            //Allow the block to properly stand on top the circle before ending the game
            Invoke("FinishGame", 0.5f);
        }
    }

    private bool IsPuzzleComplete()
    {
        //If all the four circle is stepped on , end the game
        if (s1.isSteppedOn && s2.isSteppedOn && s3.isSteppedOn && s4.isSteppedOn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Toggle instruction
    private void ShowInstruction()
    {
        if (instruction.activeSelf)
        {
            instruction.SetActive(false);
            showInstructButton.SetActive(true);
            Time.timeScale = 1;
        }
        else
        {
            instruction.SetActive(true);
            showInstructButton.SetActive(false);
            Time.timeScale = 0;
        }
    }

    //End the game
    private void FinishGame()
    {
        Destroy(GameObject.Find(MasterScript.firstScene));
        MasterScript.isFirstSceneCompleted = true;
        MasterScript.main.SetActive(true);
    }
}
