using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject giftMenu;
    [SerializeField] private TextMeshProUGUI explanationText;
    [SerializeField] private TextMeshProUGUI houseName;

    //happiness counter stuff
    [SerializeField] private Image verySadObj;
    [SerializeField] private Image sadObj;
    [SerializeField] private Image neutralObj;
    [SerializeField] private Image happyObj;
    [SerializeField] private Image veryHappyObj;

    [SerializeField] private Sprite verySadGrey;
    [SerializeField] private Sprite verySadColors;
    [SerializeField] private Sprite sadGrey;
    [SerializeField] private Sprite sadColors;
    [SerializeField] private Sprite neutralGrey;
    [SerializeField] private Sprite neutralColors;
    [SerializeField] private Sprite happyGrey;
    [SerializeField] private Sprite happyColors;
    [SerializeField] private Sprite veryHappyGrey;
    [SerializeField] private Sprite veryHappyColors;

    private int score = 3; //1 for very sad, 2 for sad, 3 for neutral, 4 for happy, 5 for very happy

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Found more than one MenuManager, fix this immediately!");
        }
    }
    
    public void OpenGiftMenu(string _explanation, string _houseName)
    {
        giftMenu.SetActive(true);
        explanationText.text = _explanation;
        houseName.text = _houseName;
        inputReader.DisableGameplay();
    }

    public void GiftCheck(Gift.Type _type)
    {
        if (playerController.lastWantedGift == _type)
        {
            if (score < 5) score++;
        }
        else
        {
            if (score > 1) score--;
        }
        UpdateHappiness();
        CloseGiftMenu();
        
        //UPDATE POINTER HERE
    }

    public void CloseGiftMenu()
    {
        giftMenu.SetActive(false);
        inputReader.EnableGameplay();
    }

    private void UpdateHappiness()
    {
        switch (score)
        {
            case 1:
            verySadObj.sprite = verySadColors;
            sadObj.sprite = sadGrey;
            neutralObj.sprite = neutralGrey;
            happyObj.sprite = happyGrey;
            veryHappyObj.sprite = veryHappyGrey;
            break;
            case 2:
            verySadObj.sprite = verySadGrey;
            sadObj.sprite = sadColors;
            neutralObj.sprite = neutralGrey;
            happyObj.sprite = happyGrey;
            veryHappyObj.sprite = veryHappyGrey;
            break;
            case 3:
            verySadObj.sprite = verySadGrey;
            sadObj.sprite = sadGrey;
            neutralObj.sprite = neutralColors;
            happyObj.sprite = happyGrey;
            veryHappyObj.sprite = veryHappyGrey;
            break;
            case 4:
            verySadObj.sprite = verySadGrey;
            sadObj.sprite = sadGrey;
            neutralObj.sprite = neutralGrey;
            happyObj.sprite = happyColors;
            veryHappyObj.sprite = veryHappyGrey;
            break;
            case 5:
            verySadObj.sprite = verySadGrey;
            sadObj.sprite = sadGrey;
            neutralObj.sprite = neutralGrey;
            happyObj.sprite = happyGrey;
            veryHappyObj.sprite = veryHappyColors;
            break;
        }
    }
}
