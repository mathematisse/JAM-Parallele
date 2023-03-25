using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EscapeRuntimeUI : MonoBehaviour
{
    private Button _continuebutton;
    private Button _mainmenubutton;
    private Button _optionsbutton;
    private Button _quitbutton;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        _continuebutton = uiDocument.rootVisualElement.Q("ContinueButton") as Button;
        _mainmenubutton = uiDocument.rootVisualElement.Q("MainMenuButton") as Button;
        _optionsbutton = uiDocument.rootVisualElement.Q("OptionsButton") as Button;
        _quitbutton = uiDocument.rootVisualElement.Q("QuitButton") as Button;

        _continuebutton.RegisterCallback<ClickEvent>(DisableThis);
        _mainmenubutton.RegisterCallback<ClickEvent>(LoadMenuScene);
        _optionsbutton.RegisterCallback<ClickEvent>(OpenOptions);
        _quitbutton.RegisterCallback<ClickEvent>(QuitGame);
    }

    private void OnDisable()
    {
        _continuebutton.UnregisterCallback<ClickEvent>(LoadMenuScene);
        _optionsbutton.UnregisterCallback<ClickEvent>(OpenOptions);
        _quitbutton.UnregisterCallback<ClickEvent>(QuitGame);
    }

    private void DisableThis(ClickEvent evt)
    {
        this.gameObject.SetActive(false);
    }

    private void LoadMenuScene(ClickEvent evt)
    {
        SceneManager.LoadScene(0);
    }

    private void OpenOptions(ClickEvent evt)
    {
        Debug.Log("Open Options");
    }

    private void QuitGame(ClickEvent evt)
    {
        Application.Quit();
    }
}
