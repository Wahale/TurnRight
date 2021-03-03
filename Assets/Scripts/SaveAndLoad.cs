using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private string _path;
    private string data = "data.json";
    private Save _save;

    private void Awake()
    {
        this._save = new Save();
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, data);
#else
        _path = Path.Combine(Application.dataPath, data);
#endif
        LoadFromFile();
    }

    public void SaveToFile()
    {
        File.WriteAllText(_path, JsonUtility.ToJson(_save));
    }

    public Save GetSave() => this._save;

    private void LoadFromFile()
    {
        if (File.Exists(_path))
        {
            _save = JsonUtility.FromJson<Save>(File.ReadAllText(_path));
        }
    }

    private void OnApplicationQuit()
    {
        SaveToFile();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (Application.platform == RuntimePlatform.Android)
            SaveToFile();
    }
}

[System.Serializable]
public class Save
{
    public int TotalLaps;
    public int TotalTurns;
    public int TotalLines;
    public int TotalDies;
    public int TotalRestart;

    public int MaxTurnsByGame;
    public int MaxLinesByGame;
    public int MaxLapsByGame;

    public bool SettingsVibration;
    public bool SettingsMusic;

    public Sprite SelectedCar;
}
