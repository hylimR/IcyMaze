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

    void Start()
    {
        MasterScript.main = this.gameObject;
    }

    void Update()
    {
        if(isFirstSceneCompleted && isSecondSceneCompleted && isThirdSceneCompleted)
        {
            //win
        }
    }

    public static bool IsSceneComplete(string scene)
    {
        switch (scene)
        {
            case "scene_four": return isFirstSceneCompleted;
            case "scene_six": return isSecondSceneCompleted;
            case "scene_yang": return isThirdSceneCompleted;
        }
        return false;
    }
}
