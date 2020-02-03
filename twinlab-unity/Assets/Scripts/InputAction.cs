using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAction
{
    public bool[] alphakeys;
    public float horizontalInput, scrollWheel;
    public bool isJumping, isCrouching, isSprinting, isFiring, isInteracting;

    public InputAction()
    {
        horizontalInput = 0f;
        isJumping = false;
        isCrouching = false;
        isSprinting = false;
        isInteracting = false;
    }

    public void Invert()
    {
        horizontalInput = -horizontalInput;
    }

    public void Set(float horizontalInput, float scrollWheel, bool isJumping, bool isCrouching, bool isSprinting, bool isFiring, bool isInteracting, bool[] alphakeys)
    {
        this.horizontalInput = horizontalInput;
        this.scrollWheel = scrollWheel;
        this.isJumping = isJumping;
        this.isCrouching = isCrouching;
        this.isSprinting = isSprinting;
        this.isFiring = isFiring;
        this.isInteracting = isInteracting;
        this.alphakeys = alphakeys;
    }

    public void SetHorizontal(float horizontalInput)
    {
        this.horizontalInput = horizontalInput;
    }

    public int GetAlphaKey()
    {
        int key = -1;
        for (int i = 0; i < alphakeys.Length; i++)
            if (alphakeys[i])
            {
                key = i;
                break;
            }
        return key;
    }

    public void Get()
    {
        bool[] alphakeys = new bool[] { Input.GetKeyDown(KeyCode.Alpha0), Input.GetKeyDown(KeyCode.Alpha1), Input.GetKeyDown(KeyCode.Alpha2),
                                        Input.GetKeyDown(KeyCode.Alpha3), Input.GetKeyDown(KeyCode.Alpha4), Input.GetKeyDown(KeyCode.Alpha5),
                                        Input.GetKeyDown(KeyCode.Alpha6), Input.GetKeyDown(KeyCode.Alpha7), Input.GetKeyDown(KeyCode.Alpha8),
                                        Input.GetKeyDown(KeyCode.Alpha9)} ;

        Set(Input.GetAxis("Horizontal"), Input.GetAxis("Mouse ScrollWheel"), Input.GetKeyDown(KeyCode.Space), Input.GetKey(KeyCode.LeftControl), 
            Input.GetKey(KeyCode.LeftShift), Input.GetKeyDown(KeyCode.Mouse0), Input.GetKeyDown(KeyCode.F), alphakeys);
    }

    override 
    public string ToString()
    {
        return "InputAction[horizontalInput="+horizontalInput+", isJumping="+isJumping+", isCrouching="+isCrouching+", isSprinting="+isSprinting+", isFiring="+isFiring+"]";
    }
}