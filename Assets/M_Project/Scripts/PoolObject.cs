using System.Collections.Generic;
using UnityEngine;

public class PoolObject
{
    private GameObject _prefab; // Префаб, який буде використовуватися пулом
    private int _initialPoolSize; // Початковий розмір пулу

    private List<GameObject> pool; // Список вільних об'єктів   

    private List<List<GameObject>> poolL;


    public PoolObject(GameObject prefab, int initialPoolSize)
    {
        _prefab = prefab;
        _initialPoolSize = initialPoolSize;
        
        pool = new List<GameObject>();
        CreateInitialPool();
    }

    // Створення початкового пулу об'єктів
    void CreateInitialPool()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            CreateNewObjectInPool();
        }
    }

    // Створення нового об'єкта в пулі
    GameObject CreateNewObjectInPool()
    {
        GameObject obj = Object.Instantiate(_prefab);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    // Отримання вільного об'єкту з пулу
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

    // Повернути об'єкт в пул
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.GetComponent<CityUIElement>().ResetUI();
    }
}
