using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputReader : MonoBehaviour
{
    static public event Action<Vector2> movementPlayer;
    static public event Action shoot;
    static public event Action reload;
    static public event Action<bool> jump;
    static public event Action<bool> shoot2;
    static public event Action interactive;
    static public event Action<Vector2> movementCamera;
    static public event Action<Vector2> scroll;
    public void MovementPlayer(InputAction.CallbackContext context)
    {
        Vector2 value=context.ReadValue<Vector2>();
        movementPlayer?.Invoke(value);
    }
    public void MovementCamera(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        movementCamera?.Invoke(value);
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            shoot?.Invoke();
        }
        shoot2?.Invoke(context.performed);
        print(context.performed);   
    }
    public void Jump(InputAction.CallbackContext context)
    {
        jump?.Invoke(context.performed);
    }
    public void Interactive(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            interactive?.Invoke();
        }
    }
    public void Reload(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            reload?.Invoke();
        }
    }
    public void Scrool(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 value = context.ReadValue<Vector2>();
            scroll?.Invoke(value);
        }
    }
}
