using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStationManager : MonoBehaviour
{
    private PuzzleStation[] stations;
    // Start is called before the first frame update
    void Start()
    {
        stations = FindObjectsOfType<PuzzleStation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
