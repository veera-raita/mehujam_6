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
    }

    public static string GetName()
    {
        if (!initialized) InitializeNames();

        for (int i = 0; i < names.Count; i++)
        {
            if (names.ElementAt(i).Value == false)
            {
                names[names.ElementAt(i).Key] = true;
                return names.ElementAt(i).Key;
            }
        }

        return "Roserock";
    }
}
