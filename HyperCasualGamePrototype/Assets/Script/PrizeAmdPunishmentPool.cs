using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeAmdPunishmentPool : MonoBehaviour
{

    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools = null;
    [SerializeField] private Vector3 spawnVector;
    [SerializeField] private Player player;

    

    // Start is called before the first frame update
    public void Start()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].pooledObjects = new Queue<GameObject>();
            for (int y = 0; y < pools[i].poolSize; y++)
            {
                GameObject _object = Instantiate(pools[i].objectPrefab);
                _object.SetActive(false);
                pools[i].pooledObjects.Enqueue(_object);
            }
        }
    }

    public GameObject GetPoolsObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }
        GameObject _object = pools[objectType].pooledObjects.Dequeue();
        _object.SetActive(true);
        pools[objectType].pooledObjects.Enqueue(_object);

        return _object;
    }

    public IEnumerator Spawner()
    {
        var _object = GetPoolsObject(UnityEngine.Random.Range(0, pools.Length));
        _object.transform.position = new Vector3(UnityEngine.Random.Range(-3f, 3f), spawnVector.y, spawnVector.z + player.transform.position.z);
        spawnVector.z = 7f + UnityEngine.Random.Range(0, 8);

        yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 5f));

        StartCoroutine(nameof(Spawner));
    }
}
