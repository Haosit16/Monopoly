using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerFactory
{
    public List<Player> Get(int count, PlayerConfig playerConfig, Transform parentUIPanel, CellManager cellManager)
    {
        List<Player> list = new List<Player>();

        for (int i = 0; i < count; i++)
        {
            GameObject clonePlayer = Object.Instantiate(playerConfig.PrefabsModel[i]);
            Player player = clonePlayer.GetComponent<Player>();

            GameObject panelPlayer = Object.Instantiate(playerConfig.PrefabsUIpanel);
            panelPlayer.transform.parent = parentUIPanel;
            

            player.Initialize($"Player {i + 1}", i, playerConfig.Color[i], false, cellManager, panelPlayer.GetComponent<PlayerPanelUI>());
            cellManager.outerCirclePath[0].AddPlayer(player);

            
            list.Add(player);

        }
        return list;
    }
}
