using System.Collections.Generic;
using UnityEngine;

public class CityAssetManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private int countInitialize;
    [SerializeField] private GameObject panelSell;
    [SerializeField] private Transform container;
    [SerializeField] private ActionManager actionManager;
    private PoolObject uiPool;
    private List<Assets> playerCities = new List<Assets>();
    private Player _player;
    private bool _needPayOwner;
    private void Start()
    {
        uiPool = new PoolObject(m_Prefab, countInitialize);
    }
    public void UpdateCityUI(Player player, bool needPayOwner) // оновлюємо інтерфейс з майном
    {
        _player = player;
        playerCities = player.ownedAssets;
        foreach (var city in playerCities)
        {
            var cityElement = uiPool.GetObjectFromPool();
            cityElement.transform.parent = container;
            cityElement.GetComponent<CityUIElement>().Initialize(city, SellCity);
        }
        panelSell.SetActive(true);
    }
    private void SellCity(Assets city) // метод для продажу майна
    {
        Debug.Log($"Sold city: {city.cellData.CellName} for {city.cellData.price}$");
        playerCities.Remove(city);
        if(!_needPayOwner)
        {
            actionManager.EndStep();
        }
        
    }
}
