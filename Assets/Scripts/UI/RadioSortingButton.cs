using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class RadioSortingButton : MonoBehaviour
{
    [Header("Settings")] public ToggleGroup options;
    public float panelHidingSpeed = .5f;

    private SorthingMethod _sorthingMethod;

    public SorthingMethod SorthingMethod
    {
        get => _sorthingMethod;
        private set
        {
            _sorthingMethod = value;
        }
    }

    private void Awake()
    {
        Assert.IsNotNull(options);
        GameEvents.S.onStartSorting += HidePanel;
        GameEvents.S.onSortingComplete += ShowPanel;
    }



    private void Start()
    {
        SetSortingMethod();
    }

    public void Sort()
    {
        SetSortingMethod();
        GameEvents.S.Invoke_OnStartSorting(SorthingMethod);
    }

    private void SetSortingMethod()
    {
        Toggle toggle = options.ActiveToggles().FirstOrDefault();
        print(toggle.name + "_" + toggle.GetComponentInChildren<Text>().text);
        SorthingMethod = toggle.name switch
        {
            "Option1" => SorthingMethod.InsertionSort,
            "Option2" => SorthingMethod.BubbleSort,
            "Option3" => SorthingMethod.SelectionSort,
            _ => SorthingMethod
        };
    }

    private void ShowPanel()
    {
        StartCoroutine(MovePanelLeft());
    }
    
    private IEnumerator MovePanelLeft()
    {
        Vector3 targetPosition = new Vector3(-100, 0, 0);
        RectTransform rectTransform = GetComponent<RectTransform>();
        while (rectTransform.anchoredPosition3D.x != targetPosition.x)
        {
            Vector3 newPosition = Vector3.MoveTowards(
                rectTransform.anchoredPosition,
                targetPosition,
                panelHidingSpeed);
            rectTransform.anchoredPosition = newPosition;

            yield return null;
        }
    }
    
    public void HidePanel(SorthingMethod sm)
    {
        StartCoroutine(MovePanelRight());
    }

    private IEnumerator MovePanelRight()
    {
        Vector3 targetPosition = new Vector3(100, 0, 0);
        RectTransform rectTransform = GetComponent<RectTransform>();
        while (rectTransform.anchoredPosition3D.x != targetPosition.x)
        {
            Vector3 newPosition = Vector3.MoveTowards(
                rectTransform.anchoredPosition,
                targetPosition,
                panelHidingSpeed);
            rectTransform.anchoredPosition = newPosition;

            yield return null;
        }
    }
}
