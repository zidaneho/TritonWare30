using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // [SerializeField] private string GameScene;

    // public void StartGame() {
    //     SceneManager.LoadSceneAsync(GameScene);
    // }

    public void QuitGame() {
        Application.Quit();
    }
}
