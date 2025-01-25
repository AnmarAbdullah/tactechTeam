using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] public int score;
    [SerializeField] GameObject currentBalloon;
    [SerializeField] string targetTag;
    [SerializeField] GameManager gmanager;

    public int Score;

    void Start()
    {
        
    }

    void Update()
    {
        //if (!gmanager.GameOn) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }


        if(score >= 10)
        {
            gmanager.EndGame(name);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.GetComponent<Balloon>() != null)
        {
            currentBalloon = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentBalloon != null) currentBalloon = null;
    }

    public void Shot()
    {
        if(currentBalloon != null)
        {
            if(currentBalloon.gameObject.tag == targetTag)
            {
                currentBalloon.GetComponent<Animator>().SetTrigger("Explode");
                currentBalloon.GetComponent<Animator>().SetTrigger("Explode");
                AddScore(currentBalloon.GetComponent<Balloon>().score);
            }
        }
    }

    public void AddScore(int score)
    {
        Score += score;
    }
}
