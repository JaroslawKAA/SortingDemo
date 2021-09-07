using SortingStrategies;
using UnityEngine;

public static class SortingStrategyFactory
{
    public static SortingStrategyBase GetStrategy(SorthingMethod sortingMethod, GameObject instance)
    {
        return sortingMethod switch
        {
            SorthingMethod.BubbleSort => new BubbleSortingStrategyBase(instance),
            SorthingMethod.InsertionSort => new InsertionSortingStrategy(instance),
            SorthingMethod.SelectionSort => new SelectionSortingStrategyBase(instance)
        };
    }
}
