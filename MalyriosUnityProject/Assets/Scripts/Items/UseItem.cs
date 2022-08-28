using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI actualItem = null;
    public void useItem(int id){
        actualItem.text = ItemDatabase.GetItem(id).ItemName;

    }

}
