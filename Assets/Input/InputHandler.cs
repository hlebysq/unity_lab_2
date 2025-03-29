using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private GameControls controls;

    public delegate void MoveAction(Vector2Int direction);
    public static event MoveAction OnMove;

    private Vector2 touchStartPos;
    private float swipeThreshold = 50f;

    private void Awake()
    {
        controls = new GameControls();
        controls.Player.Move.performed += ctx => HandleKeyboardInput(ctx.ReadValue<Vector2>());
        controls.Player.Swipe.performed += ctx => HandleSwipe(ctx.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void HandleKeyboardInput(Vector2 input)
    {
        Vector2Int direction = Vector2Int.zero;
        if (input.y > 0) direction = Vector2Int.up;
        else if (input.y < 0) direction = Vector2Int.down;
        else if (input.x > 0) direction = Vector2Int.right;
        else if (input.x < 0) direction = Vector2Int.left;

        if (direction != Vector2Int.zero)
        {
            Debug.Log($"Move: {direction}");
            OnMove?.Invoke(direction);
        }
    }

    private void HandleSwipe(Vector2 delta)
    {
        if (delta.magnitude > swipeThreshold)
        {
            Vector2Int direction = Vector2Int.zero;
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                direction = delta.x > 0 ? Vector2Int.right : Vector2Int.left;
            else
                direction = delta.y > 0 ? Vector2Int.up : Vector2Int.down;

            Debug.Log($"Swipe: {direction}");
            OnMove?.Invoke(direction);
        }
    }
}