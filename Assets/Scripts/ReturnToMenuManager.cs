using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenuManager : MonoBehaviour
{
    [SerializeField] KeyCode returnToMenu = KeyCode.Escape;

    SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    
    void Update()
    {
        if (Input.GetKey(returnToMenu))
        {
            sceneLoader.LoadScene(0);
        }
    }
}
