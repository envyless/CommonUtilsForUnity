using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionRecord : IRecordDataBase
{
    public int index;
    public List<int> targetIndexes = new List<int>();
    public string characterName;
    public int turn;

    public ActionRecord(int _index, int _turn)
    {
        this.index = _index;
        this.turn = _turn;
        this.delayedTime = 0;
    }

    public double delayedTime { get; set; }

    public IEnumerator Invoke()
    {
        
        yield return null;
    }
}

