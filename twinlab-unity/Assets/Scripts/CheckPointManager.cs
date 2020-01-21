using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public CheckPoint first;
    private static Vector3 point;

    void Start()
    {
        SetCheckPoint(first);
    }

    public static void SetCheckPoint(CheckPoint checkpoint)
    {
        Debug.Log("Setting checkPoint " + checkpoint.gameObject.name);
        point = checkpoint.transform.position;
    }

    public static Vector3 GetCheckPointPos()
    {
        return point;
    }
}
