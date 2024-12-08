using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HouseNamer
{
    private static Dictionary<string, bool> names = new();
    private static bool initialized = false;

    public static void InitializeNames()
    {
        names.Add("Roserock",false);
        names.Add("Ashwood",false);
        names.Add("Stonecreek",false);
        names.Add("Steelwood",false);
        names.Add("Runebloom",false);
        names.Add("Crowlove",false);
        names.Add("Palebrook",false);
        names.Add("Smallfire",false);
        names.Add("Eveglade",false);
        names.Add("Heartglow",false);
        names.Add("Ironbraid",false);
        names.Add("Pinekeep",false);
        names.Add("Hearthhold",false);
        names.Add("Redvale",false);
        initialized = true;
    }

    void Start()
    {
        names.Clear();
        InitializeNames();
        initialized = true;
    }

    public static string GetName()
    {
        if (!initialized) InitializeNames();

        int nameIndex = Random.Range(0, names.Count);
        if (names.ElementAt(nameIndex).Value == false) return names.ElementAt(nameIndex).Key;

        int startingIndex = nameIndex;

        while (true)
        {
            nameIndex++;
            if (nameIndex >= names.Count) nameIndex = 0;
            if (names.ElementAt(nameIndex).Value == false)
            {
                names[names.ElementAt(nameIndex).Key] = true;
                return names.ElementAt(nameIndex).Key;
            }
            if (startingIndex == nameIndex) break;
        }

        return "Roserock";
    }
}
