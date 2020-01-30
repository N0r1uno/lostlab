using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cable : MonoBehaviour
{
    public Door door;
    public Sprite on;
    public Sprite off;
    public float timeout = 2f;

    private bool isOn = false;
    private new SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void GiveSignal()
    {
        if (!isOn)
        {
            door.Open();
            renderer.sprite = on;
            StartCoroutine(TurnOff());
        }
    }

    IEnumerator TurnOff()
    {
        isOn = true;
        yield return new WaitForSeconds(timeout);
        isOn = false;
        renderer.sprite = off;
        door.Close();
    }
}
