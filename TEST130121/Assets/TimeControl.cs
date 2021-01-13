using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [SerializeField] float timeVar;
    private void Start()
    {
        timeVar = 1.0f;
    }
    private void Update()
    {
        Time.timeScale = timeVar;
    }
}
