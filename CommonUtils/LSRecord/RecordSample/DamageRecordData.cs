using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Damage
/// </summary>
public struct DamageRecordData : IRecordDataBase
{
    public string characterName;
    public int turn;
    public double damage;

    public DamageRecordData(string _characterName, int _turn, double _damage, double _delayedTime)
    {
        this.characterName = _characterName;
        this.turn = _turn;
        this.damage = _damage;
        this.delayedTime = _delayedTime;
    }

    public double delayedTime { get; set; }

    public IEnumerator Invoke()
    {
        Debug.Log("Record name : " + characterName+"\nDamage : "+damage);
        yield return null;
    }
}
