using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전투를 플레이 할때 어떤 데이터가 어떻게 셋팅되었는지에 대한 레코딩이다.
/// </summary>
public class BattleSetupRecord : IRecordDataBase
{
    public double delayedTime { get; set; }

    public IEnumerator Invoke()
    {
        throw new System.NotImplementedException();
    }
}
