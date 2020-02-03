using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [Range(0f, 4f)]
    public float smoothness = 1f;

    private new Camera camera;
    private Player player;
    private Vector3 initial;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        camera = GetComponent<Camera>();
        initial = camera.transform.position;
    }

    void FixedUpdate()
    {
        Vector3 dest = new Vector3(player.transform.position.x, player.transform.position.y, 0) + initial;
        camera.transform.position = Vector3.MoveTowards(camera.transform.position, dest, Vector3.Distance(camera.transform.position, dest)*smoothness*Time.deltaTime); 
    }
}
