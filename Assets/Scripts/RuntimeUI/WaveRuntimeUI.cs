using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveRuntimeUI : MonoBehaviour
{
    private Label _waveLabel;
    private Label _timerLabel;

    // Start is called before the first frame update
    void Start()
    {
        var uiDocument = GetComponent<UIDocument>();

        _waveLabel = uiDocument.rootVisualElement.Q("WaveLabel") as Label;
        _timerLabel = uiDocument.rootVisualElement.Q("TimerLabel") as Label;
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    public void UpdateWave(int wave)
    {
        if (_waveLabel != null)
            _waveLabel.text = wave.ToString();
    }

    public void UpdateTimer(int timer)
    {
        if (_timerLabel == null)
            return;
        _timerLabel.text = timer.ToString();
        if (timer <= 10)
        {
            _timerLabel.style.color = Color.red;
        }
        else
        {
            _timerLabel.style.color = Color.white;
        }
    }
}
