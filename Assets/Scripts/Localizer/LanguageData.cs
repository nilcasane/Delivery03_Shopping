﻿using System.Collections.Generic;

public enum Language
{
    English = 1,
    Español,
    Català
}

public class LanguageData
{
    // One string per language
    public Dictionary<Language, string> Data;

    public LanguageData(string[] rawData)
    {
        Data = new Dictionary<Language, string>();

        for (int i = 1; i < rawData.Length; i++)
        {
            Data.Add((Language)i, rawData[i]);
        }
    }

    public string GetText(Language language)
    {
        return Data[language];
    }

    public List<string> GetLanguages()
    {
        var languages = new List<string>();

        foreach (var value in Data.Values)
        {
            languages.Add(value);
        }

        return languages;
    }
}
