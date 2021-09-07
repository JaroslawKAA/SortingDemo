using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BallFactory : MonoBehaviour
{
    [Header("Settings")] public GameObject ballPrefab;

    /// <summary>
    /// Singleton
    /// </summary>
    public static BallFactory S { get; private set; }

    private void Awake()
    {
        Assert.IsNotNull(ballPrefab);
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
    }

    public GameObject GetBall(Vector3 position, Transform parent)
    {
        GameObject instance = Instantiate(ballPrefab, position, Quaternion.identity, parent);

        return instance;
    }
}
