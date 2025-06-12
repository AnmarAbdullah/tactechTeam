using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Transition : MonoBehaviour
{
    public RawImage IceBG;
    public Image IdleLoop;
    public Image ColaLoop;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealColaLoop()
    {
        //IdleLoop.DOColor(new Color(1, 1, 1, 0), 1);
        Vector3 pos = IceBG.rectTransform.position;
        //pos.y += 5;
        IdleLoop.DOColor(new Color(1, 1, 1, 0f), 0.5f);

        IceBG.rectTransform.DOAnchorPos(new Vector2(IceBG.rectTransform.anchoredPosition.x, IceBG.rectTransform.anchoredPosition.y + 2.1f), 1.2f);
        ColaLoop.DOColor(new Color(1, 1, 1, 0.75f), 0.5f);
        ColaLoop.GetComponent<Animator>().SetBool("true", true);
    }
}
