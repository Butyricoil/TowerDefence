using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Kingdom.Core.Domain.Interfaces;

public class PlayerInputHandler : MonoBehaviour
{
    [Inject] private IPlayer _player;
    
    private Vector2 _moveInput;
    
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        _player.Move(_moveInput.x);
    }
    
    public void OnMount(InputValue value)
    {
        if (value.isPressed)
        {
            if (_player.IsMounted)
                _player.Dismount();
            else
                _player.Mount();
        }
    }
} 