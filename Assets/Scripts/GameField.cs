using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    private Dictionary<Cell, CellView> cellViews = new Dictionary<Cell, CellView>();
    public int fieldSize = 4;
    public GameObject cellPrefab;
    public Transform gameBoard;
    private List<Cell> cells = new List<Cell>();

    private void Start()
    {
        CreateCell(new Vector2Int(0, 0));
        CreateCell(new Vector2Int(1, 0));
        
    }

    public Vector2Int GetEmptyPosition()
    {
        List<Vector2Int> emptyPositions = new List<Vector2Int>();
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                if (!cells.Exists(cell => cell.Position == new Vector2Int(x, y)))
                {
                    emptyPositions.Add(new Vector2Int(x, y));
                }
            }
        }
        return emptyPositions.Count > 0 ? emptyPositions[UnityEngine.Random.Range(0, emptyPositions.Count)] : new Vector2Int(-1, -1);
    }



    public void CreateCell(Vector2Int? fixedPosition = null)
    {
        Vector2Int position = fixedPosition ?? GetEmptyPosition();
        if (position.x == -1) return;

        int value = UnityEngine.Random.value < 0.8f ? 1 : 2;
        Cell newCell = new Cell(position, value);
        cells.Add(newCell);

        GameObject cellObject = Instantiate(cellPrefab, gameBoard);
        CellView cellView = cellObject.GetComponent<CellView>();
        cellView.Init(newCell);
        cellViews.Add(newCell, cellView);

        ScoreManager.Instance.AddScore(value);
        newCell.OnValueChanged += ScoreManager.Instance.AddScore;
    }
    public void CreateCell(Vector2Int fixedPosition, int value)
    {
        Vector2Int position = fixedPosition;
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].Position == position)
            {
                cells.RemoveAt(i);
            }
        }
        if (position.x == -1) return;

        Cell newCell = new Cell(position, value);
        cells.Add(newCell);

        GameObject cellObject = Instantiate(cellPrefab, gameBoard);
        CellView cellView = cellObject.GetComponent<CellView>();
        cellView.Init(newCell);
        cellViews.Add(newCell, cellView);

        ScoreManager.Instance.AddScore(value);
        newCell.OnValueChanged += ScoreManager.Instance.AddScore;
    }


    public void Turn(Vector2Int direction)
    {
        if (direction.x > 0)
        {
            int[] sumValue = { 0, 0, 0, 0 };
            foreach (Cell cell in cells)
            {
                sumValue[cell.Position.x] += cell.Value;
            }
            for (int i = 0; i < 4; i++)
            {
                if (sumValue[i] > 0) CreateCell(new Vector2Int(3, i), sumValue[i]);

            }
        }
        else if (direction.x < 0)
        {
            int[] sumValue = { 0, 0, 0, 0 };
            foreach (Cell cell in cells)
            {
                sumValue[cell.Position.x] += cell.Value;
            }
            for (int i = 0; i < 4; i++)
            {
                if (sumValue[i] > 0) CreateCell(new Vector2Int(0, i), sumValue[i]);

            }
        }
        CreateCell();
    }
}