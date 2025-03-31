using UnityEngine;
[CreateAssetMenu(fileName = "PlayerConfig", menuName = "PlayerConfig", order = 51)]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public GameObject[] PrefabsModel { get; private set; }
    [field: SerializeField] public Color[] Color { get; private set; }
    [field: SerializeField] public GameObject PrefabsUIpanel { get; private set; }
}
