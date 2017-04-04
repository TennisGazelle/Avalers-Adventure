using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOpener : MonoBehaviour {

    // index of the scene from the build settings
    public int sceneIndex;

    // will load appropriate scene
	public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
