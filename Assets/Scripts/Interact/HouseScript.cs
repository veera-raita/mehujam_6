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
        "The kids"
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
            explanation = person[who] + " have";
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

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider == null) return;
        if (_collider.gameObject.CompareTag("Player"))
        {
            _collider.gameObject.GetComponent<PlayerController>().lastWantedGift = wantedGift.type;
        }
    }

    public void Interact()
    {
        Debug.Log("interacted with a house");
        MenuManager.instance.OpenGiftMenu(explanation, houseName);
    }
}
