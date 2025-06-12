using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] Color myColor;
    //[SerializeField] public int score;
    [SerializeField] GameObject currentBalloon;
    [SerializeField] string targetTag;
    [SerializeField] GameManager gmanager;

    [SerializeField] public TextMeshProUGUI scoreText;

    public int Score;


    public float minX = -6.7f;
    public float maxX = 7f;
    public float minY = -3.6f;
    public float maxY = 4f;


    public float moveSpeed = 5f;        // Movement speed
    public Rigidbody2D rb;

    private Vector2 movement;

    [SerializeField] KeyCode keyUp;
    [SerializeField] KeyCode keyDown;
    [SerializeField] KeyCode keyLeft;
    [SerializeField] KeyCode keyRight;
    [SerializeField] KeyCode shoot;

    private void OnEnable()
    {
        FlexSensor.OnSlingshotShot+=HandleShot;
    }

    private void HandleShot()
    {
        Shot();
    }

    private void OnDisable()
    {
        FlexSensor.OnSlingshotShot-= HandleShot;


    }

    public string targetLeft;
    public string targetup;
    public string fireTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool ShowImpact = false;
    [SerializeField] Image ImpactImg;

    IEnumerator CO_ImpactEffect()
    {
        ImpactImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        ImpactImg.gameObject.SetActive(false);
    }
    void Update()
    {
        //if (!gmanager.GameOn) return;

        if (Input.GetKeyDown(shoot) || Input.GetKeyDown("e"))
        {
            Shot();
        }


        if (Input.anyKeyDown)
        {
            // Loop through all the possible keys and check which one was pressed
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    // Print the name of the key that was pressed
                    Debug.Log("Key pressed: " + key);
                    break;
                }
            }
        }


        movement.x = Input.GetAxisRaw(targetLeft);  // Left/Right arrow keys
        movement.y = Input.GetAxisRaw(targetup);    // Up/Down arrow keys


        //if (Input.GetKey(keyUp))
        //{
        //    movement.y = 1;
        //}
        //else if (Input.GetKey(keyDown))
        //{
        //    movement.y = -1;
        //}
        //else
        //{
        //    movement.y = 0;
        //}

        //if (Input.GetKey(keyLeft))
        //{
        //    movement.x = -1;
        //}
        //else if (Input.GetKey(keyRight))
        //{
        //    movement.x = 1;
        //}
        //else
        //{
        //    movement.x = 0;
        //}



        if (Score >= 500)
        {
            gmanager.EndGame(name, myColor);


        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;

        // Clamp the position of the Rigidbody2D
        float clampedX = Mathf.Clamp(rb.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(rb.position.y, minY, maxY);

        // Apply the clamped position back to the Rigidbody2D
        rb.position = new Vector2(clampedX, clampedY);
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
        if(ShowImpact)
        StartCoroutine(CO_ImpactEffect());
        if(currentBalloon != null)
        {
            if(currentBalloon.gameObject.tag == targetTag)
            {
                currentBalloon.GetComponent<Animator>().SetTrigger("Explode");
                currentBalloon.GetComponent<Animator>().SetTrigger("Explode");
                currentBalloon.GetComponent<AudioSource>().Play();
                AddScore(currentBalloon.GetComponent<Balloon>().score);
            }
        }
    }

    public void AddScore(int score)
    {
        Score += score;
        scoreText.text = Score.ToString();
    }
}
