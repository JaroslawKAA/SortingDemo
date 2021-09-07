using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Settings")] public GameObject[] slots;
    
    /// <summary>
    /// Singleton
    /// </summary>
    public static GameManager S;

    public bool IsSorting { get; private set; } = false;

    private void Awake()
    {
        Assert.IsNotNull(slots);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            throw new ArgumentException("Try to create second singleton object.");
        }
        
        GameEvents.S.onStartSorting += EmptySlots;
        GameEvents.S.onStartSorting += GenerateBalls;
        GameEvents.S.onStartSorting += SetFlag;
        GameEvents.S.onSortingComplete += ResetFlag;

    }
    
    private void GenerateBalls(SorthingMethod sm)
    {
        List<GameObject> slots =
            this.slots
                .ToList()
                .Shuffle();

        foreach (GameObject slot in slots)
        {
            BallFactory.S.GetBall(slot.transform.position, slot.transform);
        }
    }

    private void EmptySlots(SorthingMethod sm)
    {
        foreach (GameObject o in slots)
        {
            if (o.transform.childCount > 0)
            {
                DestroyImmediate(o.transform.GetChild(0).gameObject);
            }
        }
    }

    private void SetFlag(SorthingMethod sm)
    {
        IsSorting = true;
    }

    private void ResetFlag()
    {
        IsSorting = false;
    }
}