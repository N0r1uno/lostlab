using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAction
{
    public float horizontalInput;
    public bool isJumping, isCrouching, isSprinting, isFiring, isInteracting;

    public InputAction()
    {
        horizontalInput = 0f;
        isJumping = false;
        isCrouching = false;
        isSprinting = false;
        isInteracting = false;
    }

    public void Set(float horizontalInput, bool isJumping, bool isCrouching, bool isSprinting, bool isFiring, bool isInteracting)
    {
        this.horizontalInput = horizontalInput;
        this.isJumping = isJumping;
        this.isCrouching = isCrouching;
        this.isSprinting = isSprinting;
        this.isFiring = isFiring;
        this.isInteracting = isInteracting;
    }

    public void SetHorizontal(float horizontalInput)
    {
        this.horizontalInput = horizontalInput;
    }

    public void Get()
    {
        Set(Input.GetAxis("Horizontal"), Input.GetKeyDown(KeyCode.Space), Input.GetKey(KeyCode.LeftControl), 
            Input.GetKey(KeyCode.LeftShift), Input.GetKeyDown(KeyCode.Mouse0), Input.GetKeyDown(KeyCode.F));
    }

    override 
    public string ToString()
    {
        return "InputAction[horizontalInput="+horizontalInput+", isJumping="+isJumping+", isCrouching="+isCrouching+", isSprinting="+isSprinting+", isFiring="+isFiring+"]";
    }
}