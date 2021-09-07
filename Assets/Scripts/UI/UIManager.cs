using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text stepsText;
    public Text timeText;
    public InputField timeScaleInput;
    public Button cancelSortingButton;
    public Text messageText;
    
    private int steps;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(stepsText);
        Assert.IsNotNull(timeText);
        Assert.IsNotNull(timeScaleInput);
        Assert.IsNotNull(cancelSortingButton);
        Assert.IsNotNull(messageText);

        messageText.text = "";
        
        GameEvents.S.onRobotStateChange += IncrementStepCounter;
        GameEvents.S.onStartSorting += ResetStats;
        GameEvents.S.onStartSorting += EnableCancelSortingButton;
        GameEvents.S.onStartSorting += SetSortingTypeText;
        GameEvents.S.onSortingComplete += DisableCancelSortingButton;
        GameEvents.S.onSortingComplete += ResetSortingTypeText;
    }


    // Update is called once per frame

    void Update()
    {
        if (!GameManager.S.IsSorting)
            return;
        
        time += Time.deltaTime;
        timeText.text = $"Time: {(int)(time / 60)}:{(int)(time % 60)}";
    }

    private void IncrementStepCounter()
    {
        steps++;
        stepsText.text = $"Steps: {steps}";
    }

    public void CancelSorting()
    {
        GameEvents.S.Invoke_OnSortingComplete();
    }

    public void SetTimeScale()
    {
        Time.timeScale = Convert.ToInt32(timeScaleInput.text);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void ResetStats(SorthingMethod sm)
    {
        stepsText.text = "Steps: 0";
        steps = 0;
        timeText.text = "Time: 0:0";
        time = 0f;
    }

    private void DisableCancelSortingButton()
    {
        cancelSortingButton.interactable = false;
    }

    private void EnableCancelSortingButton(SorthingMethod obj)
    {
        cancelSortingButton.interactable = true;
    }

    private void SetSortingTypeText(SorthingMethod obj)
    {
        messageText.text = obj switch
        {
            SorthingMethod.BubbleSort => "Bubble sorting",
            SorthingMethod.InsertionSort => "Insertion sorting",
            SorthingMethod.SelectionSort => "Selection sorting"
        };
    }

    private void ResetSortingTypeText()
    {
        messageText.text = "";
    }
}
