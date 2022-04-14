using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JerryBank : MonoBehaviour, ISavable
{
    public double jerryBucks = 100;
    [SerializeField] TextMeshPro text;

    public void Update()
    {
        text.text = $"Jerry Bucks: ${jerryBucks}";
    }

    Type ISavable.GetSaveDataType()
    {
        return typeof(JerryBankSaveData);
    }

    void ISavable.OnFinishLoad()
    {

    }

    class JerryBankSaveData : SaveData
    {
        public double jerryBucks;
    }
}
