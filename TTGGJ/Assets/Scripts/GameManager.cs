using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float gameTimer;

    public bool GameOn;

    public TextMeshProUGUI countDownText;

    [SerializeField] float speed = 1;
    public RawImage title;
    [SerializeField] Button[] MenuButtons;

    [SerializeField] GameObject ScoreBoards;

    [SerializeField] SpriteRenderer p1;
    [SerializeField] SpriteRenderer p2;

    [SerializeField] AudioSource countDownSFX;
    [SerializeField] AudioSource MainMenuSong;
    [SerializeField] AudioSource GameplaySong;

    [SerializeField] TextMeshProUGUI winnerUI;
    [SerializeField] Image idleImage;

    //winner UI
    //TutorialUI

    //[SerializeField] Image IdleLoop;
    //[SerializeField] Image IdleToLoop;
    //MainMenu videoPlayer
    //MainMenu Video img
    //GameVideoim;

    [SerializeField] TextMeshProUGUI[] instructions;

    public void StartGame()
    {
        GameOn = true;
        p1.DOColor(new Color(1, 1, 1, 1), 0);
        p2.DOColor(new Color(1, 1, 1, 1), 0);
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
        ScoreBoards.GetComponent<RectTransform>().DOAnchorPos(new Vector2(ScoreBoards.GetComponent<RectTransform>().anchoredPosition.x, ScoreBoards.GetComponent<RectTransform>().anchoredPosition.y - 220), speed);


        Vector3 posScore = title.rectTransform.position;
        //posScore.y += 5;
        title.rectTransform.DOAnchorPos(new Vector2(title.rectTransform.anchoredPosition.x, title.rectTransform.anchoredPosition.y + 600), speed);

        //title.DOColor(new Color(1, 1, 1, 0), 0.5f);

        MainMenuSong.DOFade(0, 1).OnComplete(() =>
        {
            GameplaySong.Play();
        });

        // Remove Menu buttons UI
        foreach (var button in MenuButtons) 
        {
            button.interactable = false;
            button.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), speed);
            button.GetComponentInChildren<TextMeshProUGUI>().DOColor(new Color(1, 1, 1, 0), speed).OnComplete(() => 
            { 
                button.gameObject.SetActive(false);
            });
        }

        yield return new WaitForSeconds(3);

       // instructions.DOColor(new Color(1, 1, 1, 1), 1);

        foreach(TextMeshProUGUI intruc in instructions)
        {
            intruc.DOColor(new Color(1, 1, 1, 1), 1);
        }
        //flatten your opponents bla bla bla instruction
        yield return new WaitForSeconds(6);
        //wait for 6 seconds
        countDownText.gameObject.SetActive(true);
        float timeRemaining = 3;
        countDownSFX.Play();
        while (timeRemaining > 0)
        {
            countDownText.text = Mathf.Ceil(timeRemaining).ToString();
            timeRemaining -= 1f;
            yield return new WaitForSeconds(1f);
        }

        countDownText.text = "Go!";
        //startgame
        StartGame();
        yield return new WaitForSeconds(1);
        //instructions.DOColor(new Color(1, 1, 1, 0), 1);
        instructions[0].gameObject.SetActive(false);
        countDownText.gameObject.SetActive(false);
    }



    public void EndGame(string winnerName, Color winnerColor)
    {
        StartCoroutine(CO_EndGame(winnerName));
        GameOn = false;
        winnerUI.gameObject.SetActive(true);
        winnerUI.color = winnerColor;
        winnerUI.text = $"{winnerName} Wins!";

        p1.DOColor(new Color(1, 1, 1, 0), 0);
        p2.DOColor(new Color(1, 1, 1, 0), 0);
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
            ret.Score = 0;
            ret.scoreText.text = "0";

        }

        winnerUI.gameObject.SetActive(false);
        gameTimer = 0;

        p1.DOColor(new Color(1, 1, 1, 1), 0);
        p2.DOColor(new Color(1, 1, 1, 1), 0);

        StartCoroutine(CO_ResetGame());
    }

    IEnumerator CO_ResetGame()
    {
        //after tutorial and 3 2 1

        GameOn = true;

        yield return null;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
