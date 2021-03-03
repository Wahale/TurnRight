using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class MobileSound : MonoBehaviour
{
    [SerializeField] private SaveAndLoad saveAndLoad;

    private void Start()
    {
        AudioListener.pause = saveAndLoad.GetSave().SettingsMusic;
    }

    public void ToggleSoundSettings() {
        if (AudioListener.pause)
        {
            AudioListener.pause = false;
        }
        else 
        {
            AudioListener.pause = true;
        }

        saveAndLoad.SaveToFile();
    }
}
