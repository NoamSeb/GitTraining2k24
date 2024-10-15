using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeStart : MonoBehaviour
{
    [SerializeField] Image debut;
    Color transparence;

    [SerializeField] float timer = 3f;
    float currentTimer;

    [SerializeField] AnimationCurve fade;

    private void Start()
    {
        transparence = debut.color;
        transparence.a = 1f;
        debut.color = transparence;

        currentTimer = timer;
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;

        float pourcentageTransparence = currentTimer / timer;
        transparence.a = fade.Evaluate(pourcentageTransparence);
        debut.color = transparence;
    }
}
