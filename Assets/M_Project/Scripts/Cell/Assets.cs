using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assets : Cell
{
    public CellData cellData;
    [SerializeField] private GameObject ownerView;
    private Player owner;
    private int countHouse;
    private int countHotel;
    private ActionManager actionManager;
    private float alpha = 0.5f;

    private void Start()
    {
        actionManager = FindObjectOfType<ActionManager>();
        ownerView.SetActive(false);
    }

    public override void Active(Player player)
    {
        CheckCell(player);
    }
    private void CheckCell(Player player)
    {
        if(owner == null)
        {
            // викликаєм панель активності
            actionManager.OpenNoOwnre(this, player);
        }
        else
        {
            if(owner.playerID != player.playerID)
            {
                actionManager.OpenHasOwner(this, player, owner);
            }
            else
            {
                actionManager.EndStep();
            }
        }
    }
    public void ChangeOwner(Player newOwner)
    {
        owner = newOwner;
        ownerView.SetActive(true);

        Renderer renderer = ownerView.GetComponent<Renderer>();
        Color color = owner.playerColor;
        color.a = alpha;
        renderer.material.color = color;

        Material material = new Material(renderer.sharedMaterial);
        renderer.material = material;
    }
    public int GetPayPrice()
    {
        if(countHouse!=0)
        {
            return cellData.priceToStep + cellData.priceHouse[countHouse];
        }
        else if(countHotel != 0)
        {
            return cellData.priceToStep + cellData.priceHotel;
        }
        return cellData.priceToStep;
    }
}
