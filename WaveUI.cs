using UnityEngine;
using TMPro;
using System.Collections;

public class WaveUI : MonoBehaviour
{
    public TMP_Text waveText;
    public float displayTime = 2f;

    private Coroutine waveRoutine;

    public void ShowWave(int waveNumber)
    {
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);

        waveRoutine = StartCoroutine(ShowWaveRoutine(waveNumber));
    }

    private IEnumerator ShowWaveRoutine(int wave)
    {
        waveText.gameObject.SetActive(true);
        waveText.text = $"WAVE {wave}";
        yield return new WaitForSeconds(displayTime);
        //waveText.gameObject.SetActive(false);
    }

}
