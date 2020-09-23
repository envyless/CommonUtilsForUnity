using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecordDataBase
{
    IEnumerator Invoke();

    double delayedTime { get; set; }
}
