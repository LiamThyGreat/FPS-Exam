using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenuManager : MonoBehaviour
{
    [SerializeField] KeyCode returnToMenu = KeyCode.Escape;
    [SerializeField] GameObject pauseCanvas;

    private GameObject player;

    SceneLoader sceneLoader;

    void Start()
    {
        player = FindObjectOfType<PlayerHealth>().gameObject;

        sceneLoader = FindObjectOfType<SceneLoader>();

        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        }
    }

    
    void Update()
    {
        if (Input.GetKey(returnToMenu))
        {
            if(pauseCanvas != null)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseCanvas.SetActive(true);
                Time.timeScale = 0f;
                player.SetActive(false);
            }
            else
            {
                sceneLoader.LoadScene(0);
            }
        }
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        player.SetActive(true);
        pauseCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
