using UnityEngine;

public class HouseScript : MonoBehaviour, IInteractable
{
    //gift stuff
    private int wantedGiftType;
    private Gift wantedGift;
    private string explanation = "";
    private string houseName = "";
    [SerializeField] private string houseType = "";

    private string[] person = 
    {
        "I",
        "Little",
        "My love"
    };

    private void ConstructStrings()
    {
        //construct explanation
        int who = Random.Range(0, person.Length);
        if (who == 0)
        {
            explanation = person[who] + "'ve";
        }
        else if (who == 1)
        {
            explanation = person[who] + KidNamer.GetName() + " has";
        }
        else
        {
            explanation = person[who] + " has";
        }

        explanation += " wanted " + wantedGift.name;

        int howLong = Random.Range(0, 3);
        if (howLong == 0)
        {
            explanation += " all day!";
        }
        else if (howLong == 1)
        {
            explanation += " for a while now.";
        }
        else
        {
            explanation += " all year!";
        }

        //construct house name
        houseName = HouseNamer.GetName() + " " + houseType;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wantedGiftType = Random.Range(0, Gift.Type.GetNames(typeof(Gift.Type)).Length);
        wantedGift = new((Gift.Type)wantedGiftType);
        
        ConstructStrings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("interacted with a house");
        MenuManager.instance.OpenGiftMenu(explanation, houseName);
    }
}
