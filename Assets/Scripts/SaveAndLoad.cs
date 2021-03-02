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
    private int _totalLaps;
    private int _totalTurns;
    private int _totalLines;
    private int _totalDies;
    private int _totalRestart;

    private int _maxTurnsByGame;
    private int _maxLinesByGame;
    private int _maxLapsByGame;

    public int TotalLaps { get => _totalLaps; set => _totalLaps = value; }
    public int TotalTurns { get => _totalTurns; set => _totalTurns = value; }
    public int TotalLines { get => _totalLines; set => _totalLines = value; }
    public int TotalDies { get => _totalDies; set => _totalDies = value; }
    public int TotalRestart { get => _totalRestart; set => _totalRestart = value; }
    public int MaxTurnsByGame { get => _maxTurnsByGame; set => _maxTurnsByGame = value; }
    public int MaxLinesByGame { get => _maxLinesByGame; set => _maxLinesByGame = value; }
    public int MaxLapsByGame { get => _maxLapsByGame; set => _maxLapsByGame = value; }
}
