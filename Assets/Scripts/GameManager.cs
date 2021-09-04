using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [Header("Settings")] public GameObject[] slots;

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
        GenerateBalls();
    }

    private void GenerateBalls()
    {
        Random rnd = new Random();
        List<GameObject> slots =
            this.slots
                .ToList()
                .Shuffle();

        foreach (GameObject slot in slots)
        {
            BallFactory.S.GetBall(slot.transform.position, slot.transform);
        }
    }
}