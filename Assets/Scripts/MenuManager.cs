using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    //base functionality
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform playerPos;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject giftMenu;
    [SerializeField] private TextMeshProUGUI explanationText;
    [SerializeField] private TextMeshProUGUI houseName;

    //audio stuff
    [SerializeField] private AudioSource jingleSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Image muteObj;
    [SerializeField] private Sprite muteImg;
    [SerializeField] private Sprite unmuteImg;
    private bool muted = false;

    //pointer
    [SerializeField] private RectTransform pointer;
    [SerializeField] private Transform[] houses;

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
    [SerializeField] private int totalHouses;
    public int deliveredHouses { get; private set; } = 0;
    private int realScore;

    //game end stuff here
    [SerializeField] private TextMeshProUGUI recapText;
    [SerializeField] private GameObject endScreen;

    //game start stuff here
    [SerializeField] private Image startPanel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI tutText;
    [SerializeField] private Image logo;
    private const float fadeInTime = 3.0f;
    private const float startTime = 4.0f;
    private Coroutine fader;

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

    void Start()
    {
        inputReader.DisableGameplay();
        startPanel.gameObject.SetActive(true);
        fader = StartCoroutine(FadeIn());
        totalHouses = houses.Length;
    }

    void Update()
    {
        PointerUpdate();
    }

    private IEnumerator DelayControls()
    {
        yield return new WaitForSeconds(0.2f);
        inputReader.EnableGameplay();
    }

    private void PointerUpdate()
    {
        Vector2 dir;

        if (deliveredHouses < totalHouses)
        {
            dir = (playerPos.position - houses[deliveredHouses].position).normalized;
        }
        else
        {
            dir = (playerPos.position - houses[deliveredHouses-1].position).normalized;
        }

        float zRotation = Vector2.SignedAngle(Vector2.up, dir);
        pointer.transform.rotation = Quaternion.Euler(0, 0, zRotation);
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
            realScore++;
        }
        else
        {
            if (score > 1) score--;
        }

        deliveredHouses++;
        jingleSource.Play();

        UpdateHappiness();
        CloseGiftMenu();
    }

    public void CloseGiftMenu()
    {
        giftMenu.SetActive(false);
        inputReader.EnableGameplay();


        if (deliveredHouses == totalHouses)
        {
            GameEnd();
        }
    }

    public void ToggleMute()
    {
        if (muted) //if muted, unmute
        {
            muteObj.sprite = muteImg;
            jingleSource.mute = false;
            musicSource.mute = false;
            muted = false;
        }
        else
        {   
            muteObj.sprite = unmuteImg;
            jingleSource.mute = true;
            musicSource.mute = true;
            muted = true;
        }
    }

    private void GameEnd()
    {
        endScreen.SetActive(true);

        string recap = "You delivered gifts to a total of " + totalHouses +
        " houses, and in the end, " + realScore + "/" + totalHouses + " were correct!\n";

        if ((float)realScore / (float)totalHouses >= 0.8f)
        {
            recap += "Good work, Santa Paws!";
        }
        else
        {
            recap += "Still a bit left to learn!";
        }

        recapText.text = recap;
    }

    public void RestartGame()
    {
        playerController.ClearEvents();
        SceneManager.LoadScene(0);
    }

    private IEnumerator FadeIn()
    {
        float timer = 0;
        Color panelColor = startPanel.color;
        Color textColor = titleText.color;

        yield return new WaitForSeconds(startTime);

        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;

            panelColor.a = Mathf.Lerp(1f, 0f, timer / fadeInTime);
            textColor.a = Mathf.Lerp(1f, 0f, timer / fadeInTime);

            startPanel.color = panelColor;
            titleText.color = textColor;
            tutText.color = textColor;
            logo.color = textColor;
            yield return null;
        }

        StartCoroutine(DelayControls());
        startPanel.gameObject.SetActive(false);
    }

    public void CloseStartScreen()
    {
        StartCoroutine(DelayControls());
        startPanel.gameObject.SetActive(false);
        StopCoroutine(fader);
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
