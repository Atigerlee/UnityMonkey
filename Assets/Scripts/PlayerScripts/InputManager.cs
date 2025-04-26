using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; // reference for playerinput script
    private PlayerInput.OnfootActions onFoot;// reference for input system Map onfoot 
    private PlayerMotor motor;
    private PlayerLook look;
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.Onfoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() {
        // tell the plyermotor to move using the value from movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate(){
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable(){
        onFoot.Enable();
    }
    private void OnDisable(){
        onFoot.Disable();
    }
}
