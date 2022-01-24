using UnityEngine;

public class Cell3D : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    public CellColor color { get; private set; }

    public void SetColor(CellColor color)
    {
        this.color = color;
        UpdateRenderer(color);
    }

    private void UpdateRenderer(CellColor color)
    {
        if (!renderer)
            renderer = GetComponentInChildren<Renderer>();
        
        switch (color)
        {
            case CellColor.White:
                SetRendererColor(new Color(0.9f, 0.9f, 0.9f));
                break;
            case CellColor.Red:
                SetRendererColor(new Color(0.67f, 0.05f, 0f));
                break;
            case CellColor.Blue:
                SetRendererColor(new Color(0f, 0f, 0.85f));
                break;
            case CellColor.Orange:
                SetRendererColor(new Color(0.82f, 0.45f, 0f));
                break;
            case CellColor.Green:
                SetRendererColor(new Color(0f, 0.78f, 0f));
                break;
            case CellColor.Yellow:
                SetRendererColor(new Color(0.89f, 0.91f, 0f));
                break;
        }
    }

    private void SetRendererColor(Color color)
    {
        Material material = renderer.material;
        material.SetColor("_Color" , color);
    }
}