using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // IM2073 Project
    public void LoadScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
// End Code