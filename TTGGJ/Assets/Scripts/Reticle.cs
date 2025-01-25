using UnityEngine;

public class Reticle : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] GameObject currentBalloon;
    [SerializeField] string targetTag;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
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
                Score();
            }
        }
    }

    public void Score()
    {

    }
}
