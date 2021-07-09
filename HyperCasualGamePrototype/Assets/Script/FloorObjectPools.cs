using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorObjectPools : MonoBehaviour
{


    public Queue<GameObject> pooledObjects;
    public GameObject objectPrefab;
    public int poolSize;
    public Vector3 poolVector3;

    public void FirstIns(int value)
    {
        for (int i = 0; i < value; i++)
        {
            StartCoroutine(nameof(Spawn));
        }
    }

    private void Start()
    {
        pooledObjects = new Queue<GameObject>();
        for (int y = 0; y < poolSize; y++)
        {
            GameObject _object = Instantiate(objectPrefab);
            _object.SetActive(false);

            pooledObjects.Enqueue(_object);
        }

    }
    public GameObject GetPooledObjects()
    {
        GameObject _gameObject = pooledObjects.Dequeue();
        _gameObject.SetActive(true);
        pooledObjects.Enqueue(_gameObject);
        return _gameObject;
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.1f);
        var obj = GetPooledObjects();
        obj.transform.position = poolVector3;
        poolVector3 = new Vector3(poolVector3.x , poolVector3.y , poolVector3.z + 10);  
   
    }


}
