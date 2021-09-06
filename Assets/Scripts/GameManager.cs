using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Settings")] public GameObject[] slots;
    private int steps;
    public Text stepsText;
    private float time;
    public Text timeText;
    private bool isSorting = false;
    
    /// <summary>
    /// Singleton
    /// </summary>
    public static GameManager S;

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
        
        Assert.IsNotNull(stepsText);
        Assert.IsNotNull(timeText);

        GameEvents.S.onStartSorting += EmptySlots;
        GameEvents.S.onStartSorting += GenerateBalls;
        GameEvents.S.onStartSorting += ResetStats;
        GameEvents.S.onStartSorting += SetFlag;
        GameEvents.S.onSortingComplete += ResetFlag;
        GameEvents.S.onRobotStateChange += IncrementStepCounter;
    }

    private void IncrementStepCounter()
    {
        steps++;
        stepsText.text = $"Steps: {steps}";
    }

    private void Update()
    {
        if (!isSorting)
            return;

        time += Time.deltaTime;
        timeText.text = $"Time: {(int)(time / 60)}:{(int)(time % 60)}";
    }

    public void Exit()
    {
        Application.Quit();
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

    private void ResetStats(SorthingMethod sm)
    {
        stepsText.text = "Steps: 0";
        steps = 0;
        timeText.text = "Time: 0:0";
        time = 0f;
    }

    private void SetFlag(SorthingMethod sm)
    {
        isSorting = true;
    }

    private void ResetFlag()
    {
        isSorting = false;
    }
}