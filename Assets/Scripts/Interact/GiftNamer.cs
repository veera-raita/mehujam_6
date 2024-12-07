using UnityEngine;

public class GiftNamer
{
    static string[] softGiftNames = 
    {
        "some socks",
        "a pair of mittens",
        "a new jacket",
        "a comfy scarf",
        "a cool beanie"
    };

    static string[] toyGiftNames =
    {
        "a cute doll",
        "a toy car",
        "an easy puzzle",
        "a toy sword",
        "a cool yo-yo"
    };

    static string[] treatGiftNames =
    {
        "some chocolate",
        "a box of cookies",
        "some caramel",
        "a whole bag of liquorice",
        "a lot of dried fruits"
    };

    public static string GetName(Gift.Type type)
    {
        if (type == Gift.Type.soft)
        {
            return softGiftNames[Random.Range(0, softGiftNames.Length)];
        }
        else if (type == Gift.Type.toy)
        {
            return toyGiftNames[Random.Range(0, toyGiftNames.Length)];
        }
        else
        {
            return treatGiftNames[Random.Range(0, treatGiftNames.Length)];
        }
    }
}
