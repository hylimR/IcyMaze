using UnityEngine;
using System.Collections.Generic;

//Master class to store data across scene
public class MasterScript
{
    public static string mainScene = "scene_three";
    public static string firstScene = "scene_four";
    public static string secondScene = "scene_six";

    public static bool isFirstSceneCompleted = false;
    public static bool isSecondSceneCompleted = false;
    public static string[] keys = {
           "A", "B", "C", "D", "E", "AlterableBox1", "AlterableBox2", "AlterableBox3", "AlterableBox4", "unitychan"
    };
    public static Dictionary<string, GameObject> savedState = new Dictionary<string, GameObject>();

    public static void SaveGameState()
    {
        MasterScript.savedState.Clear();
        foreach (string s in keys)
        {
            GameObject obj = GameObject.Find(s);
            MasterScript.savedState.Add(obj.name, obj);
        }
    }

    public static void LoadGameState()
    {
        foreach (string s in keys)
        {
            GameObject g = new GameObject();
            if (savedState.TryGetValue(s, out g))
            {
                GameObject.Instantiate(g);
            }
        }
    }
}
