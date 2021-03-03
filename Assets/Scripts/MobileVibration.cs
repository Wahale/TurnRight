using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveAndLoad))]
public class MobileVibration : MonoBehaviour
{
    [SerializeField] private float defaultVibrationTime;
    [SerializeField] private float longVibrationTime;
    [SerializeField] private float shortVibrationTime;

    [SerializeField] private SaveAndLoad saveAndLoad;

    private bool IsVibrationEnabled;

    private void Awake()
    {
        this.saveAndLoad = GetComponent<SaveAndLoad>();
    }

    private void Start()
    {
        this.IsVibrationEnabled = this.saveAndLoad.GetSave().SettingsVibration;
    }

    public void VibrateLong() => StartCoroutine(VibrateCoroutine(longVibrationTime));
    public void Vibrate() => StartCoroutine(VibrateCoroutine(defaultVibrationTime));
    public void VibrateShort() => StartCoroutine(VibrateCoroutine(shortVibrationTime));

    public void SettingsVibrationToggle() {
        if (IsVibrationEnabled)
        {
            this.saveAndLoad.GetSave().SettingsVibration = false;
            this.IsVibrationEnabled = false;
        }
        else 
        {
            this.saveAndLoad.GetSave().SettingsVibration = true;
            this.IsVibrationEnabled = true;
        }
        this.saveAndLoad.SaveToFile();
    }

    private IEnumerator VibrateCoroutine(float time) {
        float elapsedTime = 0f;
        while (elapsedTime < time) {
            Handheld.Vibrate();
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


}
