using System;
using System.Threading.Tasks;
using UnityEngine;
public class DiceManager : MonoBehaviour
{
    [SerializeField] private DiceRoll[] diceRolls;
    private TaskCompletionSource<int> diceRollTask;
    private int[] results;
    private int landedDiceCount;

    public async Task<int> GetNumber()
    {
        diceRollTask = new TaskCompletionSource<int>();
        results = new int[diceRolls.Length];
        landedDiceCount = 0;
        RollDice();
        return await diceRollTask.Task;
    }

    private void RollDice()
    {
        for (int i = 0; i < diceRolls.Length; i++)
        {
            results[i] = 0; // Clear results
            int index = i; // Capture the index for the callback

            // First remove any existing callbacks to avoid duplicates
            diceRolls[i].onLandedCallback -= (n) => OnDiceLanded(index, n);

            // Add the callback with the specific die index
            diceRolls[i].onLandedCallback += (n) => OnDiceLanded(index, n);

            diceRolls[i].RollDice();
        }
    }

    private void OnDiceLanded(int dieIndex, int number)
    {
        // Skip if this die has already reported a result
        if (results[dieIndex] != 0) return;

        results[dieIndex] = number;
        landedDiceCount++;

        // If all dice have landed, complete the task
        if (landedDiceCount == diceRolls.Length)
        {
            Debug.Log("All dice have landed");
            int total = 0;
            foreach (var num in results) total += num;
            diceRollTask.TrySetResult(total);
        }
    }
}