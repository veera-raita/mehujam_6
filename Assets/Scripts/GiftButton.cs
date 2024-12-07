using UnityEngine;

public class GiftButton : MonoBehaviour
{
    [SerializeField] private Gift.Type type;

    public void ChooseGift()
    {
        MenuManager.instance.GiftCheck(type);
    }
}
