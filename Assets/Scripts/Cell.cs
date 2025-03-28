using System;
using UnityEngine;

public class Cell
{
    public Vector2Int Position { get; private set; }
    public int Value { get; private set; }
    public bool Merged { get; set; }

    public event Action<int> OnValueChanged;
    public event Action<Vector2Int> OnPositionChanged;

    public Cell(Vector2Int position, int value)
    {
        Position = position;
        Value = value;
    }

    public void SetValue(int newValue)
    {
        if (Value != newValue)
        {
            Value = newValue;
            OnValueChanged?.Invoke(Value);
        }
    }

    public void SetPosition(Vector2Int newPosition)
    {
        if (Position != newPosition)
        {
            Position = newPosition;
            OnPositionChanged?.Invoke(Position);
        }
    }
}
