using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class RadioSortingButton : MonoBehaviour
{
    [Header("Settings")] public ToggleGroup options;

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
    }

    private void Start()
    {
        SetSortingMethod();
    }

    public void Sort()
    {
        SetSortingMethod();
        GameEvents.S.StartSorting(SorthingMethod);
    }

    private void SetSortingMethod()
    {
        Toggle toggle = options.ActiveToggles().FirstOrDefault();
        print(toggle.name + "_" + toggle.GetComponentInChildren<Text>().text);
        SorthingMethod = toggle.name switch
        {
            "Option1" => SorthingMethod.QuickSort,
            "Option2" => SorthingMethod.BubbleSort,
            "Option3" => SorthingMethod.SelectionSort,
            _ => SorthingMethod
        };
    }
}
