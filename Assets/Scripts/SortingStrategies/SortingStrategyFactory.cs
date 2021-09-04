using SortingStrategies;
using UnityEngine;

public static class SortingStrategyFactory
{
    public static SortingStrategyBase GetStrategy(SorthingMethod sorthingMethod, GameObject instance)
    {
        return sorthingMethod switch
        {
            SorthingMethod.BubbleSort => new BubbleSortingStrategyBase(instance),
            SorthingMethod.QuickSort => new QuickSortingStrategyBase(instance),
            SorthingMethod.SelectionSort => new SelectionSortingStrategyBase(instance)
        };
    }
}
