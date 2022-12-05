using System;
using UnityEditor.Tilemaps;
using UnityEngine;
 
namespace UnityEditor
{
    [CustomGridBrush(true, true, true, "Alpha Sorted Brush")]
    public class AlphaSortedBrush : GridBrush {}
 
    [CustomEditor(typeof(AlphaSortedBrush))]
    public class AlphaSortedBrushEditor : GridBrushEditor
    {
        public override GameObject[] validTargets
        {
            get
            {
                var vt = base.validTargets;
                Array.Sort(vt, (go1, go2) => String.Compare(go1.name, go2.name));
                return vt;
            }
        }
    }
}