using System;
using System.Linq;
using UnityEngine;
using ArticleAndIndicatorEnums;

public class Participant
{
#nullable enable

    private static string[] Keys
    {
        get
        {
            return typeof(Participant)
                    .GetProperties()
                    .Select(property => property.Name)
                    .ToArray();
        }
    }

    public static int startIndex = 1;

    public static int GetIndexForProperty(string propertyName)
    {
        return Array.IndexOf(Participant.Keys, propertyName);
    }

    public string? Id
    {
        get;
        set;
    }

    public string? Article0
    {
        get;
        set;
    }

    public string? Indicator0
    {
        get;
        set;
    }

    public string? Article1
    {
        get;
        set;
    }

    public string? Indicator1
    {
        get;
        set;
    }

    public string? Article2
    {
        get;
        set;
    }

    public string? Indicator2
    {
        get;
        set;
    }

    public string? Article3
    {
        get;
        set;
    }

    public string? Indicator3
    {
        get;
        set;
    }

    public Participant() { }

    public void SaveData(Article article, Indicator indicator, int iteration)
    {
        int articleNumber = (int)article;
        int indicatorNumber = (int)indicator;
        string articleNumberAsString = articleNumber.ToString();
        string indicatorNumberAsString = indicatorNumber.ToString();

        switch (iteration)
        {
            case 0:
                this.Article0 = articleNumberAsString;
                this.Indicator0 = indicatorNumberAsString;
                break;
            case 1:
                this.Article1 = articleNumberAsString;
                this.Indicator1 = indicatorNumberAsString;
                break;
            case 2:
                this.Article2 = articleNumberAsString;
                this.Indicator2 = indicatorNumberAsString;
                break;
            case 3:
                this.Article3 = articleNumberAsString;
                this.Indicator3 = indicatorNumberAsString;
                break;
            default:
                Debug.LogError($"Iteration maximum is 3. Actual: {iteration}");
                break;
        }
    }
}
