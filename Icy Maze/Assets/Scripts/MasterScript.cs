using UnityEngine;
using System.Collections.Generic;

//Master class to store global data
public class MasterScript : MonoBehaviour
{
    public static string mainScene = "Main";
    public static GameObject main;
    public static string firstScene = "scene_four";
    public static string secondScene = "scene_six";
    public static string thirdScene = "scene_yang";

    public static bool isFirstSceneCompleted = false;
    public static bool isSecondSceneCompleted = false;
    public static bool isThirdSceneCompleted = false;
    public static string playerName = "unitychan";
    private AudioSource victory;
    public AudioClip victoryOST;
    public Texture winScreen;
    private bool isAudioPlaying = false;

    void Start()
    {
        MasterScript.main = this.gameObject;
        victory = GetComponent<AudioSource>();
    }

    void Update()
    {
        //If the game is completed, Play the victory sound and freeze the game
        if (isFirstSceneCompleted && isSecondSceneCompleted && isThirdSceneCompleted)
        {
            if (!isAudioPlaying)
            {
                victory.clip = victoryOST;
                victory.Play();
                isAudioPlaying = true;
                Time.timeScale = 0;
            }
        }
    }

    public static bool IsSceneComplete(string scene)
    {
        //Check whether the scenes is completed
        switch (scene)
        {
            case "scene_four": return isFirstSceneCompleted;
            case "scene_six": return isSecondSceneCompleted;
            case "scene_yang": return isThirdSceneCompleted;
        }
        return false;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2 + 350, Screen.height / 2 - 400, 300, 90),
                            "1. WASD for movement, \n2.K for action key to activate traps/toggles" + 
                            "\n 3. Search for portal in the maze \n4.complete three of them to complete the game");
        //Show the winning screen when game is completed
        if (isFirstSceneCompleted && isSecondSceneCompleted && isThirdSceneCompleted)
        {
            GUI.DrawTexture(new Rect(Screen.width /2 - 400, Screen.height /2 - 200, 1100, 400), winScreen);
        }
    }
}
