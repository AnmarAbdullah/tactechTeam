using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    float gameTimer;

    public bool GameOn;

    public TextMeshProUGUI countDownText;

    public RawImage title;

    //winner UI
    //TutorialUI

    //MainMenu videoPlayer
    //MainMenu Video img
    //GameVideoimg


    private void Update()
    {
        
    }

    public void StartGame()
    {
        GameOn = true;
    }

    public void TransitionToGame()
    {
        StartCoroutine(CO_TransitionToGame());
    }

    public IEnumerator CO_TransitionToGame()
    {
        //Play Transition Video
        //remove title
        Vector3 pos = title.rectTransform.position;
        pos.y += 5;
        title.rectTransform.DOAnchorPos(new Vector2(title.rectTransform.anchoredPosition.x, title.rectTransform.anchoredPosition.y + 600), 1f);

        //title.DOColor(new Color(1, 1, 1, 0), 0.5f);
        
        // Remove Menu buttons UI
        //flatten your opponents bla bla bla instruction
        //wait for 6 seconds
        //3 2 1 countdown coroutine
        //startgame
        StartGame();
        yield return null;
    }

    public void EndGame(string winnerName)
    {
        StartCoroutine(CO_EndGame(winnerName));
        GameOn = false;
    }

    IEnumerator CO_EndGame(string winnerName)
    {
        yield return null;
    }

    public void ResetGame()
    {
        Player[] retics = FindObjectsOfType<Player>();
        
        foreach(Player ret in retics) 
        {
            ret.score = 0;
        }

        gameTimer = 0;

        StartCoroutine(CO_ResetGame());
    }

    IEnumerator CO_ResetGame()
    {
        //after tutorial and 3 2 1
        GameOn = true;
        yield return null;
    }

    IEnumerator CO_Countdown()
    {
        countDownText.gameObject.SetActive(true);
        //countdown
        countDownText.text = "GO!";
        yield return new WaitForSeconds(1);
        countDownText.gameObject.SetActive(false);

        yield return null;
    }
}
