using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameRuntimeUI : MonoBehaviour
{
    [Header("Cursor Manager")]
    [SerializeField] private CursorManager _cursorManager;
    
    [Header("Building Objects")]
    [SerializeField] private ScriptableBuilding _tower;
    [SerializeField] private ScriptableBuilding _barrack;
    [SerializeField] private ScriptableBuilding _stock;

    /*
    [Header("Unit Objects")]
    [SerializeField] private ScriptableUnit _peasant;
    [SerializeField] private ScriptableUnit _soldier;
    [SerializeField] private ScriptableUnit _bowman;
    [SerializeField] private ScriptableUnit _knight;
    [SerializeField] private ScriptableUnit _thief;
    [SerializeField] private ScriptableUnit _priest;
    */
    private Button _buildButton;
    private Button _trainButton;
    private Button _harvestButton;
    private Button _attackButton;

    private VisualElement _buildMenu;
    private Button _towerButton;
    private Button _barrackButton;
    private Button _stockButton;

    private VisualElement _trainMenu;
    private Button _peasantButton;
    private Button _soldierButton;
    private Button _bowmanButton;
    private Button _knightButton;
    private Button _thiefButton;
    private Button _priestButton;

    // Start is called before the first frame update
    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        
        _buildButton = uiDocument.rootVisualElement.Q("BuildButton") as Button;
        _trainButton = uiDocument.rootVisualElement.Q("TrainButton") as Button;
        _harvestButton = uiDocument.rootVisualElement.Q("HarvestButton") as Button;
        _attackButton = uiDocument.rootVisualElement.Q("AttackButton") as Button;

        _buildMenu = uiDocument.rootVisualElement.Q("BuildMenu") as VisualElement;
        _towerButton = uiDocument.rootVisualElement.Q("TowerButton") as Button;
        _barrackButton = uiDocument.rootVisualElement.Q("BarrackButton") as Button;
        _stockButton = uiDocument.rootVisualElement.Q("StockButton") as Button;

        _trainMenu = uiDocument.rootVisualElement.Q("TrainMenu") as VisualElement;
        _peasantButton = uiDocument.rootVisualElement.Q("PeasantButton") as Button;
        _soldierButton = uiDocument.rootVisualElement.Q("SoldierButton") as Button;
        _bowmanButton = uiDocument.rootVisualElement.Q("BowmanButton") as Button;
        _knightButton = uiDocument.rootVisualElement.Q("KnightButton") as Button;
        _thiefButton = uiDocument.rootVisualElement.Q("ThiefButton") as Button;
        _priestButton = uiDocument.rootVisualElement.Q("PriestButton") as Button;

        _buildButton.RegisterCallback<ClickEvent>(OnBuildButtonClicked);
        _trainButton.RegisterCallback<ClickEvent>(OnTrainButtonClicked);
        _harvestButton.RegisterCallback<ClickEvent>(OnHarvestButtonClicked);
        _attackButton.RegisterCallback<ClickEvent>(OnAttackButtonCLicked);

        _towerButton.RegisterCallback<ClickEvent>(OnTowerButtonClicked);
        _barrackButton.RegisterCallback<ClickEvent>(OnBarrackButtonClicked);
        _stockButton.RegisterCallback<ClickEvent>(OnStockButtonClicked);
        
        _peasantButton.RegisterCallback<ClickEvent>(OnPeasantButtonClicked);
        _soldierButton.RegisterCallback<ClickEvent>(OnSoldierButtonClicked);
        _bowmanButton.RegisterCallback<ClickEvent>(OnBowmanButtonClicked);
        _knightButton.RegisterCallback<ClickEvent>(OnKnightButtonClicked);
        _thiefButton.RegisterCallback<ClickEvent>(OnThiefButtonClicked);
        _priestButton.RegisterCallback<ClickEvent>(OnPriestButtonClicked);
    }

    private void OnDisable()
    {
        _buildButton.UnregisterCallback<ClickEvent>(OnBuildButtonClicked);
        _trainButton.UnregisterCallback<ClickEvent>(OnTrainButtonClicked);
        _harvestButton.UnregisterCallback<ClickEvent>(OnHarvestButtonClicked);
        _attackButton.UnregisterCallback<ClickEvent>(OnAttackButtonCLicked);

        _towerButton.UnregisterCallback<ClickEvent>(OnTowerButtonClicked);
        _barrackButton.UnregisterCallback<ClickEvent>(OnBarrackButtonClicked);
        _stockButton.UnregisterCallback<ClickEvent>(OnStockButtonClicked);

        _peasantButton.UnregisterCallback<ClickEvent>(OnPeasantButtonClicked);
        _soldierButton.UnregisterCallback<ClickEvent>(OnSoldierButtonClicked);
        _bowmanButton.UnregisterCallback<ClickEvent>(OnBowmanButtonClicked);
        _knightButton.UnregisterCallback<ClickEvent>(OnKnightButtonClicked);
        _thiefButton.UnregisterCallback<ClickEvent>(OnThiefButtonClicked);
        _priestButton.UnregisterCallback<ClickEvent>(OnPriestButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBuildButtonClicked(ClickEvent evt)
    {
        _buildMenu.SendToBack();
        _buildMenu.visible = !_buildMenu.visible;
        _trainMenu.visible = false;
    }

    private void OnTrainButtonClicked(ClickEvent evt)
    {
        _trainMenu.SendToBack();
        _trainMenu.visible = !_trainMenu.visible;
        _buildMenu.visible = false;
    }

    private void OnHarvestButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
    }

    private void OnAttackButtonCLicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
    }

    private void OnTowerButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        _cursorManager.selected = _tower;
    }

    private void OnBarrackButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        _cursorManager.selected = _barrack;
    }

    private void OnStockButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        _cursorManager.selected = _stock;
    }

    private void OnPeasantButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        //_cursorManager.selected = _peasant as ISelectable;
    }

    private void OnSoldierButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        //_cursorManager.selected = _soldier as ISelectable;
    }

    private void OnBowmanButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        //_cursorManager.selected = _bowman as ISelectable;
    }

    private void OnKnightButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        //_cursorManager.selected = _knight as ISelectable;
    }

    private void OnThiefButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        //_cursorManager.selected = _thief as ISelectable;
    }

    private void OnPriestButtonClicked(ClickEvent evt)
    {
        _trainMenu.visible = false;
        _buildMenu.visible = false;
        //_cursorManager.selected = _priest as ISelectable;
    }
}
