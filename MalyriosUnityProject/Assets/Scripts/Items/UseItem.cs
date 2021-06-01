﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    [SerializeField] private ItemDatabase database = null;
    [SerializeField] TextMeshProUGUI actualItem = null;
    public void useItem(int id){
        actualItem.text = database.GetItem(id).ItemName;

    }

}
