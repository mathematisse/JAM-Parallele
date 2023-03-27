using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RessourceRuntimeUI : MonoBehaviour
{
    private Label _woodLabel;
    private Label _stoneLabel;
    private Label _mushroomLabel;
    private Label _soulLabel;
    private Label _merchantLabel;

    // Start is called before the first frame update
    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        _woodLabel = uiDocument.rootVisualElement.Q("WoodLabel") as Label;
        _stoneLabel = uiDocument.rootVisualElement.Q("StoneLabel") as Label;
        _mushroomLabel = uiDocument.rootVisualElement.Q("MushroomLabel") as Label;
        _soulLabel = uiDocument.rootVisualElement.Q("SoulLabel") as Label;
        _merchantLabel = uiDocument.rootVisualElement.Q("MerchantLabel") as Label;
    }
    
    // Update is called once per frame
    void Update()
    {

    }
    
    public void UpdateWood(int wood)
    {
        _woodLabel.text = wood.ToString();
    }

    public void UpdateStone(int stone)
    {
        _stoneLabel.text = stone.ToString();
    }

    public void UpdateMushroom(int mushroom)
    {
        _mushroomLabel.text = mushroom.ToString();
    }

    public void UpdateSoul(int soul)
    {
        _soulLabel.text = soul.ToString();
    }

    public void UpdateMerchant(int merchant)
    {
        _merchantLabel.text = merchant.ToString();
    }
}
