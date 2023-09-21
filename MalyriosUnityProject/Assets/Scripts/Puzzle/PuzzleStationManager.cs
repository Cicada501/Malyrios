using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleStationManager : MonoBehaviour
{
    public static PuzzleStationManager Instance;
    private List<PuzzleStation> stations = new();
    private List<PuzzleStationData> loadedStationData = new();

    private void Awake()
    {
        Instance = this;
    }
    


    public void UpdateStation(PuzzleStation station)
    {
        bool stationExists = stations.Any(s => s.id == station.id);

        if (!stationExists)
        {
            stations.Add(station);
            //Debug.Log("Station mit ID " + station.id + " hinzugefügt.");
        }
        else
        {
            var existingStation = stations.Find(s => s.id == station.id);
            existingStation.itemIDsArray = station.itemIDsArray;
            //Debug.Log("Station mit ID " + station.id + " wurde aktualisiert.");
        }
    }



    public PuzzleStationDataList SaveStations()
    {
        var list = new List<PuzzleStationData>();
        foreach (var station in stations)
        {
            PuzzleStationData stationData = new PuzzleStationData(station);
            list.Add(stationData);
        }

        PuzzleStationDataList saveList = new PuzzleStationDataList(list);
        return saveList;
    }

    public void LoadStation(PuzzleStation station)
    {
        //var loadedPuzzleStations = JsonUtility.FromJson<PuzzleStationDataList>(PlayerPrefs.GetString("puzzleStations"));
        //loadedStationData = loadedPuzzleStations.puzzleStationDataList;
        var data = loadedStationData.Find(data => data.id == station.id);
        if (data == null) return;
        station.itemIDsArray = data.itemIDsArray;
        //print($"loading station: {station.id}, set itemIDArray: {data.itemIDsArray}");

    }
    public void LoadStations()
    {
        loadedStationData = ReferencesManager.Instance.gameData.LoadedPuzzleStations;
    }
}
