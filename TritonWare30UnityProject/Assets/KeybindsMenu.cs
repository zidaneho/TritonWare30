using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeybindsMenu : MonoBehaviour
{
    [SerializeField] private string GameScene;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadSceneAsync(GameScene);
        }
    }
}
