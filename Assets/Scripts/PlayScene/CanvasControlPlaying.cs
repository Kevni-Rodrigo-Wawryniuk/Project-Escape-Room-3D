using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class CanvasControlPlaying : MonoBehaviour
{
    // Esto es para llamas a este codigo en otros codigos
    public static CanvasControlPlaying canvasControlPlaying;

    //
    [Header("Pause Control")]
    // variable que determina si se puede poner en pausa el juego
    public bool pause;
    // Canvas/lienzo pause
    [SerializeField] Canvas canvasPause;
    // vaiable que activa o desactiva el modo de pausa
    [SerializeField] bool pauseActive;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    // En esta funcion solo se ejecutara una ves al iniciar el juego
    private void StartProgram()
    {
        if(canvasControlPlaying == null)
        {
            canvasControlPlaying = this;
        }

        pauseActive = false;

        canvasPause = GameObject.Find("Canvas Pause").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    // Esta funcion es la que permite al jugador poner en pausa el juego
    private void PauseGame()
    {
        if(pause == true)
        {
            canvasPause.enabled = pauseActive;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseActive = !pauseActive;
            }

            if(pauseActive == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
