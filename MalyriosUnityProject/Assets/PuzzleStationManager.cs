using System;
using System.Collections;
using System.Collections.Generic;
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
   

    public void AddStation(PuzzleStation station)
    {
        stations.Add(station);
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
        var data = loadedStationData.Find(data => data.id == station.id);
        if (data != null) station.itemIDsArray = data.itemIDsArray;

    }
    public void LoadStations()
    {
        loadedStationData = ReferencesManager.Instance.gameData.LoadedPuzzleStations;
    }
}
