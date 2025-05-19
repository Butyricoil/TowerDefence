using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInputHandler : MonoBehaviour
{
    [Inject] private IPlayer _player;
    
    private Vector2 _moveInput;
    private bool _isSprinting;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _player.Move(_moveInput);
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player.Jump();
        }
    }
    
    public void OnSprint(InputAction.CallbackContext context)
    {
        _isSprinting = context.performed;
        _player.Sprint(_isSprinting);
    }
    
    public void OnDropCoin(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player.DropCoin();
        }
    }
    
}
