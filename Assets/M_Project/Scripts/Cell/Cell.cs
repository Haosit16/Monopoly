using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour
{
    protected List<Player> playersOnTile = new List<Player>();
    // Метод для додавання гравця
    public void AddPlayer(Player player)
    {
        if (!playersOnTile.Contains(player))
        {
            playersOnTile.Add(player);
            UpdatePlayerPositions();
        }
    }

    // Метод для видалення гравця
    public void RemovePlayer(Player player)
    {
        if (playersOnTile.Contains(player))
        {
            playersOnTile.Remove(player);
            UpdatePlayerPositions();
        }
    }

    // Оновлення позицій гравців на клітинці
    private void UpdatePlayerPositions()
    {
        Vector3 tilePosition = transform.position;
        Vector3[] offsets = GetOffsets(playersOnTile.Count);

        for (int i = 0; i < playersOnTile.Count; i++)
        {
            playersOnTile[i].transform.position = tilePosition + offsets[i];
        }
    }

    // Отримання офсетів для позицій гравців
    private Vector3[] GetOffsets(int playerCount)
    {
        switch (playerCount)
        {
            case 1:
                return new Vector3[] { Vector3.zero };
            case 2:
                return new Vector3[] { new Vector3(-0.3f, 0, 0), new Vector3(0.3f, 0, 0) };
            case 3:
                return new Vector3[] { new Vector3(-0.3f, 0, 0.3f), new Vector3(0.3f, 0, 0.3f), new Vector3(0, 0, -0.3f) };
            case 4:
                return new Vector3[] { new Vector3(-0.3f, 0, 0.3f), new Vector3(0.3f, 0, 0.3f), new Vector3(-0.3f, 0, -0.3f), new Vector3(0.3f, 0, -0.3f) };
            default:
                return new Vector3[] { Vector3.zero };
        }
    }

    public abstract void Active(Player player);

}
