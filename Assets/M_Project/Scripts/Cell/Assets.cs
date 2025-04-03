using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assets : Cell
{
    public CellData cellData;
    [SerializeField] private GameObject ownerView;
    private Player owner;
    private bool upgraded = false;
    private ActionManager actionManager;
    private float alpha = 0.5f;

    public bool Upgraded { get { return upgraded; } }

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
                if(upgraded || !cellData.canUpgrade)
                {
                    actionManager.EndStep();
                }
                else if(!upgraded && cellData.canUpgrade)
                {
                    
                }

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
    public void Upgrade()
    {
        upgraded = true;
    }
    public int GetPayPrice()
    {
        if(upgraded)
        {
            return cellData.priceToStep + cellData.priceUpgradeToStep;
        }
        return cellData.priceToStep;
    }
    public void Sell()
    {
        ownerView.SetActive(false);
        owner = null;
    }
}
