using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimulationControl : MonoBehaviour
{
    [SerializeField] float timeVar;
    [SerializeField] float simulationTime;
    [SerializeField] bool showRayCasts;
    [SerializeField] bool isStop;
    public Slider timeSlider;
    public TextMeshProUGUI timeUI;
    public TextMeshProUGUI entitiesCounterUI;
    public TextMeshProUGUI destroyCounterUI;
    public TextMeshProUGUI actualCounterUI;
    public TextMeshProUGUI blueCounterUI;
    public TextMeshProUGUI pinkCounterUI;
    public TextMeshProUGUI actualBlueCounterUI;
    public TextMeshProUGUI actualPinkCounterUI;
    public TextMeshProUGUI actualInfectedCounterUI;
    //public GameObject spawnObj;
    bool canShowSpawn;
    public int birthCounter;
    public int destroyCounter;
    public int actualEntityCounter;
    public int actualBlueCounter;
    public int actualPinkCounter;
    public int pinkCounter;
    public int blueCounter;
    public int infectedCounter;
    private void Start()
    {
        timeSlider.value = 0.1f;
        canShowSpawn = true;
    }
    private void Update()
    {
        actualPinkCounterUI.text = actualPinkCounter.ToString();
        actualBlueCounterUI.text = actualBlueCounter.ToString();
        blueCounterUI.text = blueCounter.ToString();
        pinkCounterUI.text = pinkCounter.ToString();
        entitiesCounterUI.text = birthCounter.ToString();
        destroyCounterUI.text = destroyCounter.ToString();
        actualCounterUI.text = actualEntityCounter.ToString();
        actualInfectedCounterUI.text = infectedCounter.ToString();
        if (Input.GetKeyDown(KeyCode.P))
        {
            canShowSpawn = !canShowSpawn;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(0);
        }
        if (canShowSpawn == true)
        {
            //spawnObj.SetActive(true);
        }
        else
        {
            //spawnObj.SetActive(false);
        }
        simulationTime += Time.deltaTime;
        timeUI.text = simulationTime.ToString("0.0");
        timeVar = timeSlider.value * 12;
        Time.timeScale = timeVar;
    }
}
