using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public Image cellImage;
    private Cell cell;

    private Color startColor = new Color(0.93f, 0.89f, 0.85f); 
    private Color endColor = new Color(0.95f, 0.69f, 0.47f);  
    private float lerpSpeed = 0.2f;

    public void Init(Cell cell)
    {
        this.cell = cell;
        cell.OnValueChanged += UpdateValue;
        cell.OnPositionChanged += UpdatePosition;
        UpdateValue(cell.Value);
        UpdatePosition(cell.Position);
        valueText.transform.SetAsLastSibling();
    }

    private void UpdateValue(int value)
    {
        valueText.text = Mathf.Pow(2, value).ToString();
        StartCoroutine(AnimateColor(value));
    }

    private void UpdatePosition(Vector2Int position)
    {
        transform.localPosition = new Vector3(position.x * 100, position.y * 100, 0);
    }

    private System.Collections.IEnumerator AnimateColor(int value)
    {
        Color targetColor = Color.Lerp(startColor, endColor, Mathf.Clamp01((value - 1) / 10f));
        Color initialColor = cellImage.color;
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime / lerpSpeed;
            cellImage.color = Color.Lerp(initialColor, targetColor, time);
            yield return null;
        }
        cellImage.color = targetColor;
    }
    public void DestroyCell()
    {
        cell.OnValueChanged -= UpdateValue;
        cell.OnPositionChanged -= UpdatePosition;
        Destroy(gameObject);
    }
}