using UnityEngine;
using System.Collections.Generic;

//Master class to store global data
public class MasterScript
{
    public static string mainScene = "scene_three";
    public static string firstScene = "scene_four";
    public static string secondScene = "scene_six";

    public static bool isFirstSceneCompleted = false;
    public static bool isSecondSceneCompleted = false;
    public static string playerName = "unitychan";
    //manual method to compare vector3
    public static bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.1f;
    }
}
