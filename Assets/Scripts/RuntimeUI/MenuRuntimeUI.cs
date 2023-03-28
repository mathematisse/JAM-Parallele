using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuRuntimeUI : MonoBehaviour
{
    private Button _startbutton;
    private Button _optionsbutton;
    private Button _quitbutton;
    private VisualElement _howToPlay;
    
    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        _startbutton = uiDocument.rootVisualElement.Q("StartButton") as Button;
        _optionsbutton = uiDocument.rootVisualElement.Q("OptionsButton") as Button;
        _quitbutton = uiDocument.rootVisualElement.Q("QuitButton") as Button;
        _howToPlay = uiDocument.rootVisualElement.Q("HowToPlay") as VisualElement;

        _startbutton.RegisterCallback<ClickEvent>(LoadGameScene);
        _optionsbutton.RegisterCallback<ClickEvent>(OpenOptions);
        _quitbutton.RegisterCallback<ClickEvent>(QuitGame);
    }

    private void OnDisable()
    {
        _startbutton.UnregisterCallback<ClickEvent>(LoadGameScene);
        _optionsbutton.UnregisterCallback<ClickEvent>(OpenOptions);
        _quitbutton.UnregisterCallback<ClickEvent>(QuitGame);
    }

    private void LoadGameScene(ClickEvent evt)
    {
        SceneManager.LoadScene(1);
    }

    private void OpenOptions(ClickEvent evt)
    {
        _howToPlay.visible = !_howToPlay.visible;
        _optionsbutton.text = _howToPlay.visible ? "Close" : "How to play";
        _quitbutton.SetEnabled(!_howToPlay.visible);
    }

    private void QuitGame(ClickEvent evt)
    {
        Application.Quit();
    }
}
