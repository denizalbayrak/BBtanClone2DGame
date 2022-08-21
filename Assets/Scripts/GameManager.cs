using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    public GameObject StartPanel;
    public GameObject GamePanel;
    public GameObject GameOverPanel;
    public GameObject PauseButton;
    public GameObject ResumeButton;



    public ShooterMovement shooterMovement;
    public BlockController blockController;
    public BallController ballController;

    void Start()
    {
        ResumeButton.SetActive(false);
    }

    public void StartGameButton()
    {     
        ballController.BallCreator();  
     
        StartPanel.transform.DOMove(GameOverPanel.transform.position, 0.25f).OnComplete(GameStart);
    }

    public void GameStart()
    {
        shooterMovement.BallsDown = false;
        blockController.RandomBlockNumberCreate();
    }

    public void GameOver()
    {
        shooterMovement.BallsDown = false;
        PlayerPrefs.SetInt("level", 0);
        GameOverPanel.transform.DOMove(GamePanel.transform.position, 0.35f);
    }

    public void StartAgainButton()
    {
        shooterMovement.BallsDown = false;
        GameOverPanel.transform.DOMove(StartPanel.transform.position, 0.35f);
        StartPanel.transform.DOMove(GamePanel.transform.position, 0.35f);
    }

    public void PauseGame()
    {
        shooterMovement.BallsDown = false;
        PauseButton.SetActive(false);
        ResumeButton.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
       StartCoroutine(blockController.touchEnable());
        ResumeButton.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1;
    }
}
