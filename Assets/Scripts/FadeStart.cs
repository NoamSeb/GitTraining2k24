using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeStart : MonoBehaviour
{
    [SerializeField] Image img;
    Color transparence;

    [SerializeField] float timer = 3f;
    float currentTimer;

    [SerializeField] AnimationCurve fade;

    private void Start()
    {
        transparence = img.color;
        transparence.a = 1f;
        img.color = transparence;

        currentTimer = timer;
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;

        float pourcentageTransparence = currentTimer / timer;
        transparence.a = fade.Evaluate(pourcentageTransparence);
        img.color = transparence;
    }
}
