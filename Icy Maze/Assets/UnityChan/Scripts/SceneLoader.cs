using UnityEngine;
using UnityEngine.SceneManagement;

// Unity-chan sample script. Application.LoadLevel and friends were removed from the
// engine years ago; ported to SceneManager so the package still compiles.
public class SceneLoader : MonoBehaviour
{
	void OnGUI()
	{
		GUI.Box(new Rect(10 , Screen.height - 100 ,100 ,90), "Change Scene");
		if(GUI.Button( new Rect(20 , Screen.height - 70 ,80, 20), "Next"))
			LoadNextScene();
		if(GUI.Button(new Rect(20 ,  Screen.height - 40 ,80, 20), "Back"))
			LoadPreScene();
	}

	void LoadPreScene()
	{
		Step(-1);
	}

	void LoadNextScene()
	{
		Step(1);
	}

	void Step(int direction)
	{
		int count = SceneManager.sceneCountInBuildSettings;
		if (count <= 0) return;

		int index = SceneManager.GetActiveScene().buildIndex + direction;
		SceneManager.LoadScene(((index % count) + count) % count);
	}
}
