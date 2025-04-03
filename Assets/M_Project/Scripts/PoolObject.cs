using System.Collections.Generic;
using UnityEngine;

public class PoolObject
{
    private GameObject _prefab; // ������, ���� ���� ����������������� �����
    private int _initialPoolSize; // ���������� ����� ����

    private List<GameObject> pool; // ������ ������ ��'����   

    private List<List<GameObject>> poolL;


    public PoolObject(GameObject prefab, int initialPoolSize)
    {
        _prefab = prefab;
        _initialPoolSize = initialPoolSize;
        
        pool = new List<GameObject>();
        CreateInitialPool();
    }

    // ��������� ����������� ���� ��'����
    void CreateInitialPool()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            CreateNewObjectInPool();
        }
    }

    // ��������� ������ ��'���� � ���
    GameObject CreateNewObjectInPool()
    {
        GameObject obj = Object.Instantiate(_prefab);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    // ��������� ������� ��'���� � ����
    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newTube = CreateNewObjectInPool();
        newTube.SetActive(true);
        return newTube;
    }

    // ��������� ��'��� � ���
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.GetComponent<CityUIElement>().ResetUI();
    }
}
