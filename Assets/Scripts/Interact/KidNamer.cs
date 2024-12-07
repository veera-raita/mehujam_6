using UnityEngine;

[CreateAssetMenu]
public class KidNamer : ScriptableObject
{
    private static string[] kidNames =
    {
        "Liam",
        "Noah",
        "Timmy",
        "Oliver",
        "Charlie",
        "Emma",
        "Amelia",
        "Sophia",
        "Riley",
        "Lily"
    };

    public static string GetName()
    {
        return kidNames[Random.Range(0, kidNames.Length)];
    }
}
