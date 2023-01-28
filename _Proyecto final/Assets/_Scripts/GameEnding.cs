using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEnding : MonoBehaviour
{
    //duracion del fade
    [SerializeField] private float fadeDuration = 1f;

    public float displayImageDuration = 1f;

    //Tomamos el objeto del jugador
    public GameObject player;

    private bool isPlayerAtExit, isPlayerCought;
    
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup CoughtBackgroundImageCanvasGroup;
    
    private float timer;

    public AudioSource exitAudio, coughtAudio;

    private bool hasAudioPlayed;

    private void OnTriggerEnter(Collider other)
    {
        //si el jugador entra en el trigger entonces...
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    private void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (isPlayerCought)
        {
            EndLevel(CoughtBackgroundImageCanvasGroup, true, coughtAudio);
        }
        
    }

  
    /// <summary>
    /// Lanza la imagen de fin de la partida
    /// </summary>
    /// <param name="imageCanvasGroup">Imagen de fin de partida correspondiente</param>
    /// <param name="doRestart">Nos dice las condiciones en las que queremos o no reiniciar el juego</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {

        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }
        timer += Time.deltaTime;
            
        //Esto hara que la pantalla de que ganamos el juego aparesca
        imageCanvasGroup.alpha = Mathf.Clamp(timer / fadeDuration,0,1);

        //si el tiempo supera el valor de el fade, entonces espera un segundo mas y luego termina el juego
        if (timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void CatchPlayer()
    {
        isPlayerCought = true;
    }
}
