using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static Vector3 point;

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
