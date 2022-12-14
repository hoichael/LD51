using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv_pool : MonoBehaviour
{
    [SerializeField] List<lv_pool_entry_config> poolInfoList;
    [SerializeField] Transform outerContainer;
    Dictionary<PoolType, lv_pool_entry> poolDict;

    private void Awake()
    {
        Setup();
    }
    private void Setup()
    {
        poolDict = new Dictionary<PoolType, lv_pool_entry>();

        foreach (lv_pool_entry_config config in poolInfoList)
        {
            lv_pool_entry newEntry = new lv_pool_entry();
            newEntry.amount = config.amount;
            newEntry.objArr = new GameObject[config.amount];

            GameObject container = new GameObject();
            container.name = config.name;
            container.transform.SetParent(outerContainer);
            newEntry.container = container.transform;

            for(int i = 0; i < config.amount; i++)
            {
                GameObject obj = Instantiate(config.prefab);
                obj.transform.SetParent(container.transform);
                obj.SetActive(false);
                newEntry.objArr[i] = obj;
            }

            poolDict.Add(config.key, newEntry);
        }
    }

    public void Dispatch(PoolType key, Vector3 pos)
    {
        lv_pool_entry poolEntry = poolDict[key];
        poolEntry.objArr[poolEntry.currentIDX].transform.localPosition = pos;
        poolEntry.objArr[poolEntry.currentIDX].SetActive(true);
        IncrementIDX(poolEntry);
    }

    public void Return(PoolType key, Transform objTrans, bool active)
    {
        objTrans.SetParent(poolDict[key].container);
        //Debug.Log(objTrans.parent);
        //objTrans.parent = poolDict[key].container;
        objTrans.gameObject.SetActive(active);
    }

    public void DisableAll(PoolType key)
    {
        foreach(GameObject obj in poolDict[key].objArr)
        {
            obj.SetActive(false);
        }
    }

    private void IncrementIDX(lv_pool_entry poolEntry)
    {
        poolEntry.currentIDX++;
        if(poolEntry.currentIDX == poolEntry.amount)
        {
            poolEntry.currentIDX = 0;
        }
    }
    
    public enum PoolType
    {
        Ball
    }
}

[System.Serializable]
public class lv_pool_entry_config
{
    public lv_pool.PoolType key;
    public GameObject prefab;
    public int amount;
    public string name;
}

public class lv_pool_entry
{
    public GameObject[] objArr;
    public Transform container;
    public int amount;
    public int currentIDX;
}
