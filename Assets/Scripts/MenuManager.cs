using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject giftMenu;
    [SerializeField] private TextMeshProUGUI explanationText;
    [SerializeField] private TextMeshProUGUI houseName;

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

    public void CloseGiftMenu()
    {
        giftMenu.SetActive(false);
        inputReader.EnableGameplay();
    }
}
