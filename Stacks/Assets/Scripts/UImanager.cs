using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UImanager : MonoBehaviour
{
    public Text timer, first, second, third, startTimerText, winnerNameText, endGameMessage;
    public GameObject gameStartPanel, PlayerInterFace, EndGameScreenPanel;
    public List<Actor> Actors = new List<Actor>();
   
    void Start()
    {
        Actors.AddRange(FindObjectsOfType<Actor>());     
    }
    // Update is called once per frame
    void Update()
    {
        switch (GamePlayManager.Instance.GameState)
        {
            case State.Starting:
                Time.timeScale = 1;
                StartCountDown();
                break;
            case State.Playing:
                gameStart();
                ActivePlayerInterFace();
                Sort();
                UpdateScore();
                Timer();
                timer.text = GamePlayManager.Instance.timeLeft.ToString("0");
                break;
            case State.End:
                Time.timeScale = 0;
                EndGamePanel();
                break;
        }
    }

    private void EndGamePanel()
    {
        PlayerInterFace.SetActive(false);
        EndGameScreenPanel.SetActive(true);
        winnerNameText.text = Actors[0].gameObject.name + " Win with  " + Actors[0].point + " point";
        winnerNameText.color = Actors[0].ActorGraphics.material.color;
        if (Actors[0].gameObject.name == "Player")
        {
            endGameMessage.text = "You win";
            endGameMessage.color = Actors[0].ActorGraphics.material.color;
        }
        else
        {
            endGameMessage.text = "You lose";
            endGameMessage.color = Color.red;
        }     
    }

    private static void Timer()
    {
        GamePlayManager.Instance.timeLeft -= Time.deltaTime;
    }

    public void StartCountDown()
    {
        GamePlayManager.Instance.startTimer -= Time.deltaTime;
        startTimerText.text = GamePlayManager.Instance.startTimer.ToString("0");
    }
    public void gameStart()
    {
        gameStartPanel.SetActive(false);
    }
    private void UpdateScore()
    {
        first.text = Actors[0].gameObject.name + " " + Actors[0].GetComponent<Actor>().point.ToString();
        first.color = Actors[0].ActorGraphics.material.color;
        second.text = Actors[1].gameObject.name + " " + Actors[1].GetComponent<Actor>().point.ToString();
        second.color = Actors[1].ActorGraphics.material.color;
        third.text = Actors[2].gameObject.name + " " + Actors[2].GetComponent<Actor>().point.ToString();
        third.color = Actors[2].ActorGraphics.material.color;
    }

    void Sort()
    {
        Actors = Actors.OrderByDescending(x => x.point).ToList();
    }
    void ActivePlayerInterFace()
    {
        PlayerInterFace.SetActive(true);
    }

   
}
