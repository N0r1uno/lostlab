using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Door : MonoBehaviour
{
    public bool isOpen;
    public List<Sprite> sprites;
    public float speed = 0.1f;

    public new Collider2D collider;
    private new SpriteRenderer renderer;
    private bool isPlayingAnimation;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
        if (isOpen) Open();
    }

    public void Toggle()
    {
        if (!isPlayingAnimation)
        {
            if (isOpen) Close();
            else Open();
        }
    }

    public void Open()
    {
        if (!isOpen && !isPlayingAnimation)
            StartCoroutine(OpenAnimation());
    }

    public void Close()
    {
        if (isOpen && !isPlayingAnimation)
            StartCoroutine(CloseAnimation());
    }

    IEnumerator OpenAnimation()
    {
        isPlayingAnimation = true;
        isOpen = true;
        for (int i = 0; i < sprites.Count; i++)
        {
            renderer.sprite = sprites[i];
            yield return new WaitForSeconds(speed);
        }
        collider.enabled = false;
        isPlayingAnimation = false;
    }

    IEnumerator CloseAnimation()
    {
        isPlayingAnimation = true;
        isOpen = false;
        for (int i = sprites.Count-1; i >= 0;i--)
        {
            renderer.sprite = sprites[i];
            yield return new WaitForSeconds(speed);
        }
        collider.enabled = true;
        isPlayingAnimation = false;
    }
}
    /*
    void Start()
    {
        if (open)
            Open();  
    }

    // Update is called once per frame
    void Update()
    {
        if (!completed)
        {
            if (open)
            {
                collider.enabled = false;
                completed = true;
            }
            else
            {
                collider.enabled = true;
                completed = true;
            }
        }
        if (timed && currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0f)
            {
                Close();
            }
        }
    }

    public void Open()
    {
        completed = false;
        open = true;
        if (timed)
        {
            currentTime = timeTillClosed;
        }
    }

    public void Close()
    {
        completed = false;
        open = false;
        currentTime = 0f;
    }

    public void ChangeState()
    {
        if (open)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
*/
