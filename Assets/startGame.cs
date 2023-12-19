using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public void OnPlayButton() {
        SceneManager.LoadScene(1);
    }

    public void OnExitButton() {
        Application.Quit();
    }
}
