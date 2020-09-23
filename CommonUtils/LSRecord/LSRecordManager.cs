using LS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UniRx;
using UnityEngine;
using Debug = UnityEngine.Debug;


/// <summary>
/// subscribe currentRecord, 
/// and current record data will Invoke it self when that timing and after delay second current record will change and notify again
/// 
/// use this just override LSRecrodManager
/// </summary>
public class LSRecordManager : MonoBehaviour
{
    #region singletone

    //singletone
    public static LSRecordManager Instance;    
    [RuntimeInitializeOnLoadMethod]
    public static void SetUp()
    {
        var go = new GameObject();
        go.name = "LSRecordManager";
        Instance = go.AddComponent<LSRecordManager>();
        DontDestroyOnLoad(go);
    }
    #endregion
    //record manager code
    public Queue<IRecordDataBase> records = new Queue<IRecordDataBase>();

    public ReactiveProperty<IRecordDataBase> currentRecord = new ReactiveProperty<IRecordDataBase>();
    public ReactiveProperty<State> currentRecordState = new ReactiveProperty<State>();

    private DateTime dtLastRecord;

    private const float MaxWaitTime = 5;

    public enum State
    {
        None,
        RecordStart,
        Recording,
        RecordEnd,

        ReplayingStart,
        Replaying,        
        Stoped,
        ReplayEnd,
        NotWalk,
    }

    private void Start()
    {
        this.currentRecordState.Subscribe(_state => {
            Debug.LogError("State Changed : "+_state); 
            switch (_state)
            {
                case State.RecordStart:
                    dtLastRecord = DateTime.Now;
                    break;
                case State.ReplayingStart:
                    dtLastRecord = DateTime.Now;
                    break;
                case State.Replaying:
                    break;
                case State.ReplayEnd:
                    break;
            }
        });
    }

    private double GetDelayTimeFromBefore()
    {
        var totalSec = (dtLastRecord - DateTime.Now).TotalSeconds;
        if (totalSec > MaxWaitTime)
            totalSec = MaxWaitTime;
        return totalSec;
    }

    public void AddRecord(ActionRecord ar)
    {
        Debug.LogError("Record Added Call : "+" ar : "+ar);
        ar.delayedTime = GetDelayTimeFromBefore();
        records.Enqueue(ar);
    }

    public void ReplayStart()
    {
        StartCoroutine(ReplayCoroutine());
    }

    private IEnumerator ReplayCoroutine()
    {
        while(records.Count > 0)
        {            
            currentRecord.Value = records.Dequeue();
            yield return StartCoroutine(currentRecord.Value.Invoke());
            yield return null;
        }
    }

    [Conditional("UNITY_EDITOR")]
    private void Update()
    {
        //Temp Code
        if(Input.GetKeyDown(KeyCode.B))
        {
            
            ReplayStart();
        }
    }

    [Conditional("QA"), Conditional("TEST"), Conditional("UNITY_EDITOR")]
    private void OnGUI()
    {
        if(GUI.Button(GetButtonRect(0),"Start Record"))
        {
            this.currentRecordState.Value = State.Recording;
        }

        if (GUI.Button(GetButtonRect(1), "End Record"))
        {
            this.currentRecordState.Value = State.RecordEnd;
        }

        if (GUI.Button(GetButtonRect(2), "Replaying Start !"))
        {
            //do something
        }
        if(GUI.Button(GetButtonRect(3), "Record File Save !"))
        {
            LSLogger.Log("Saved Recrod ! : " + records);
            records.SerializeObject("../records");                        
        }

        if (GUI.Button(GetButtonRect(4), "Record File Load !"))
        {
            LSLogger.Log("Loaded Record !");
            records = records.DeSerializeObject("../records");
        }
    }

    public static Rect GetButtonRect(int nIndex)
    {
        return new Rect(Screen.width - Screen.width / 8 * (nIndex + 1), Screen.height - Screen.height / 10, Screen.width / 8, Screen.height / 10);
    }

    public bool IsReplaying()
    {
        return currentRecordState.Value == LSRecordManager.State.Replaying;
    }

    public bool IsRecording()
    {
        return currentRecordState.Value == LSRecordManager.State.Recording;
    }
}
