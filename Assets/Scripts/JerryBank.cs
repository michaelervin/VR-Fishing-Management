using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryBank : MonoBehaviour, ISavable
{
    public double jerryBucks = 100;

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
