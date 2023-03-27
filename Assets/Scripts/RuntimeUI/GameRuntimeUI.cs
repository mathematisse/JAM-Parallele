using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameRuntimeUI : MonoBehaviour
{
    public AudioSource click;

    [Header("Cursor Manager")]
    [SerializeField] private CursorManager _cursorManager;
    
    [Header("Building Objects")]
    [SerializeField] private ScriptableBuilding _tower;
    [SerializeField] private ScriptableBuilding _barrack;
    [SerializeField] private ScriptableBuilding _stock;

    
    [Header("Unit Objects")]
    [SerializeField] private ScriptableUnit _peasant;
    [SerializeField] private ScriptableUnit _soldier;
    [SerializeField] private ScriptableUnit _knight;
    [SerializeField] private ScriptableUnit _priest;
    

    [SerializeField] private GameObject forest;
    [SerializeField] private GameObject stone;
    [SerializeField] private GameObject soul;
    [SerializeField] private GameObject mushroom;

    private Button _buildButton;
    private Button _trainButton;
    private Button _harvestButton;
    private Button _upgradeButton;

    private VisualElement _buildMenu;
    private Button _towerButton;
    private Button _barrackButton;
    private Button _stockButton;

    private VisualElement _trainMenu;
    private Button _peasantButton;
    private Button _soldierButton;
    private Button _knightButton;
    private Button _priestButton;

    private Button _fakeButton;

    // Start is called before the first frame update
    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        
        _buildButton = uiDocument.rootVisualElement.Q("BuildButton") as Button;
        _trainButton = uiDocument.rootVisualElement.Q("TrainButton") as Button;
        _harvestButton = uiDocument.rootVisualElement.Q("HarvestButton") as Button;
        _upgradeButton = uiDocument.rootVisualElement.Q("UpgradeButton") as Button;

        _buildMenu = uiDocument.rootVisualElement.Q("BuildMenu") as VisualElement;
        _towerButton = uiDocument.rootVisualElement.Q("TowerButton") as Button;
        _barrackButton = uiDocument.rootVisualElement.Q("BarrackButton") as Button;
        _stockButton = uiDocument.rootVisualElement.Q("StockButton") as Button;

        _trainMenu = uiDocument.rootVisualElement.Q("TrainMenu") as VisualElement;
        _peasantButton = uiDocument.rootVisualElement.Q("PeasantButton") as Button;
        _soldierButton = uiDocument.rootVisualElement.Q("SoldierButton") as Button;
        _knightButton = uiDocument.rootVisualElement.Q("KnightButton") as Button;
        _priestButton = uiDocument.rootVisualElement.Q("PriestButton") as Button;

        _fakeButton = uiDocument.rootVisualElement.Q("FakeButton") as Button;
        _fakeButton.SetEnabled(false);

        _towerButton.text = "Tower\n\n" + _tower.WoodCost + " Wood\n" + _tower.StoneCost + " Stone\n" + _tower.MushroomCost + " Flower\n" + _tower.SoulCost + " Soul";
        _barrackButton.text = "Barrack\n\n" + _barrack.WoodCost + " Wood\n" + _barrack.StoneCost + " Stone\n" + _barrack.MushroomCost + " Flower\n" + _barrack.SoulCost + " Soul";
        _stockButton.text = "Stock\n\n" + _stock.WoodCost + " Wood\n" + _stock.StoneCost + " Stone\n" + _stock.MushroomCost + " Flower\n" + _stock.SoulCost + " Soul";

        _peasantButton.text = "Peasant\n\n" + _peasant.WoodCost + " Wood\n" + _peasant.StoneCost + " Stone\n" + _peasant.MushroomCost + " Flower\n" + _peasant.SoulCost + " Soul";
        _soldierButton.text = "Soldier\n\n" + _soldier.WoodCost + " Wood\n" + _soldier.StoneCost + " Stone\n" + _soldier.MushroomCost + " Flower\n" + _soldier.SoulCost + " Soul";
        _knightButton.text = "Knight\n\n" + _knight.WoodCost + " Wood\n" + _knight.StoneCost + " Stone\n" + _knight.MushroomCost + " Flower\n" + _knight.SoulCost + " Soul";
        _priestButton.text = "Priest\n\n" + _priest.WoodCost + " Wood\n" + _priest.StoneCost + " Stone\n" + _priest.MushroomCost + " Flower\n" + _priest.SoulCost + " Soul";
        
        _buildButton.RegisterCallback<ClickEvent>(OnBuildButtonClicked);
        _trainButton.RegisterCallback<ClickEvent>(OnTrainButtonClicked);
        _harvestButton.RegisterCallback<ClickEvent>(OnHarvestButtonClicked);
        _upgradeButton.RegisterCallback<ClickEvent>(OnUpgradeButtonCLicked);

        _towerButton.RegisterCallback<ClickEvent>(OnTowerButtonClicked);
        _barrackButton.RegisterCallback<ClickEvent>(OnBarrackButtonClicked);
        _stockButton.RegisterCallback<ClickEvent>(OnStockButtonClicked);
        
        _peasantButton.RegisterCallback<ClickEvent>(OnPeasantButtonClicked);
        _soldierButton.RegisterCallback<ClickEvent>(OnSoldierButtonClicked);
        _knightButton.RegisterCallback<ClickEvent>(OnKnightButtonClicked);
        _priestButton.RegisterCallback<ClickEvent>(OnPriestButtonClicked);
    }

    private void OnDisable()
    {
        _buildButton.UnregisterCallback<ClickEvent>(OnBuildButtonClicked);
        _trainButton.UnregisterCallback<ClickEvent>(OnTrainButtonClicked);
        _harvestButton.UnregisterCallback<ClickEvent>(OnHarvestButtonClicked);
        _upgradeButton.UnregisterCallback<ClickEvent>(OnUpgradeButtonCLicked);

        _towerButton.UnregisterCallback<ClickEvent>(OnTowerButtonClicked);
        _barrackButton.UnregisterCallback<ClickEvent>(OnBarrackButtonClicked);
        _stockButton.UnregisterCallback<ClickEvent>(OnStockButtonClicked);

        _peasantButton.UnregisterCallback<ClickEvent>(OnPeasantButtonClicked);
        _soldierButton.UnregisterCallback<ClickEvent>(OnSoldierButtonClicked);
        _knightButton.UnregisterCallback<ClickEvent>(OnKnightButtonClicked);
        _priestButton.UnregisterCallback<ClickEvent>(OnPriestButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _buildMenu.visible = false;
            _trainMenu.visible = false;
            resetHarvest();
            _cursorManager.selected = null;
            _fakeButton.text = "None";
            _cursorManager.isUpgrading = false;
        }
    }

    public void resetHarvest()
    {
        forest.GetComponent<CollectScript>().resetCollectable();
        stone.GetComponent<CollectScript>().resetCollectable();
        soul.GetComponent<CollectScript>().resetCollectable();
        mushroom.GetComponent<CollectScript>().resetCollectable();
    }

    private void OnBuildButtonClicked(ClickEvent evt)
    {
        _buildMenu.SendToBack();
        _buildMenu.visible = !_buildMenu.visible;
        _trainMenu.visible = false;
        click.Play();
        resetHarvest();
        _fakeButton.text = "None";
        _cursorManager.selected = null;
        _cursorManager.isUpgrading = false;
    }

    private void OnTrainButtonClicked(ClickEvent evt)
    {
        _trainMenu.SendToBack();
        _trainMenu.visible = !_trainMenu.visible;
        _buildMenu.visible = false;
        click.Play();
        resetHarvest();
        _fakeButton.text = "None";
        _cursorManager.selected = null;
        _cursorManager.isUpgrading = false;
    }

    private void OnHarvestButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        forest.GetComponent<CollectScript>().switchCollectable();
        stone.GetComponent<CollectScript>().switchCollectable();
        soul.GetComponent<CollectScript>().switchCollectable();
        mushroom.GetComponent<CollectScript>().switchCollectable();
        click.Play();
        _fakeButton.text = "Harvest";
        _cursorManager.selected = null;
        _cursorManager.isUpgrading = false;
    }

    private void OnUpgradeButtonCLicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        click.Play();
        resetHarvest();
        _fakeButton.text = "Upgrade";
        _cursorManager.selected = null;
        _cursorManager.isUpgrading = true;
    }

    private void OnTowerButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        _cursorManager.selected = _tower;
        click.Play();
        _fakeButton.text = "Tower";
    }

    private void OnBarrackButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        _cursorManager.selected = _barrack;
        click.Play();
        _fakeButton.text = "Barrack";
    }

    private void OnStockButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        _cursorManager.selected = _stock;
        click.Play();
        _fakeButton.text = "Storage";
    }

    private void OnPeasantButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        _cursorManager.selected = _peasant;
        click.Play();
        _fakeButton.text = "Peasant";
    }

    private void OnSoldierButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        _cursorManager.selected = _soldier;
        click.Play();
        _fakeButton.text = "Soldier";
    }


    private void OnKnightButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        _cursorManager.selected = _knight;
        click.Play();
        _fakeButton.text = "Knight";
    }


    private void OnPriestButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        _cursorManager.selected = _priest;
        click.Play();
        _fakeButton.text = "Priest";
    }
}
