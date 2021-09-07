using UnityEngine;

public class Ball : MonoBehaviour
{
    public int Id;

    public Color Color;

    private static int _nextId = 1;
    private static int NextId
    {
        get
        {
            int currentId = _nextId;
            _nextId++;
            return currentId;
        }
    }

    // Start is called before the first frame update

    void OnEnable()
    {
        Id = NextId;
        SetColor();
    }
    
    private void SetColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color color = new Color(renderer.material.GetColor("_Color").r,
            (float)((float) Id / (float) GameManager.S.slots.Length),
            renderer.material.GetColor("_Color").b);
        renderer.material.SetColor("_Color", color);
    }

    private void OnDestroy()
    {
        _nextId--;
    }
}
