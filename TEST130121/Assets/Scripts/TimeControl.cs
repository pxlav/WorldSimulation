using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeControl : MonoBehaviour
{
    [SerializeField] float timeVar;
    [SerializeField] float simulationTime;
    [SerializeField] bool showRayCasts;
    [SerializeField] bool isStop;
    public Slider timeSlider;
    public TextMeshProUGUI timeUI;
    private void Start()
    {
        timeSlider.value = 0.2f;
    }
    private void Update()
    {
        simulationTime += Time.deltaTime;
        timeUI.text = simulationTime.ToString("0.0");
        timeVar = timeSlider.value * 6;
        Time.timeScale = timeVar;
    }
}
