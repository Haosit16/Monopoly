using UnityEngine;
[CreateAssetMenu(fileName = "CellData",menuName = "CellData")]
public class CellData : ScriptableObject
{
    public string CellName;
    public int price;
    public int priceToStep;
    public int[] priceHouse = new int[3];
    public int priceHotel;
}
