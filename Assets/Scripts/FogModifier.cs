using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FogModifier : MonoBehaviour
{
    public float defaultDensity = 0.005f;
    public float volumeDensity = 0.1f;
    public float transitionDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            DOTween.To(x => RenderSettings.fogDensity = x, defaultDensity, volumeDensity, transitionDuration);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            DOTween.To(x => RenderSettings.fogDensity = x, volumeDensity, defaultDensity, transitionDuration);
    }
}
