
public class SellCityAssets : Cell
{
    private CityAssetManager cityAssetManager;
    private void Start()
    {
        cityAssetManager = FindObjectOfType<CityAssetManager>();
    }
    public override void Active(Player player)
    {
        cityAssetManager.UpdateCityUI(player, false);
    }

 
}
