using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceRoll : MonoBehaviour
{
    public int diceFaceNum;

    public event Action<int> onLandedCallback;
    private bool isRolling = false;
    private int targetNumber;

    [SerializeField] private float rollingDuration = 1.5f; // Час обертання
    [SerializeField] private float rollingSpeed = 720f; // Швидкість обертання
    [SerializeField] private AnimationCurve slowDownCurve; // Крива уповільнення

    private readonly Quaternion[] faceRotations =
{
        Quaternion.Euler(-90, 0, -90),      // 1 (верхня грань)
        Quaternion.Euler(0, 0, 0),    // 2
        Quaternion.Euler(0, 0, -90),     // 3
        Quaternion.Euler(-180, 0, -90),    // 4
        Quaternion.Euler(0, 0, -180),     // 5
        Quaternion.Euler(90, 0, -90)     // 6 (нижня грань)
    };

    public void RollDice()
    {
        if (isRolling) return;
        isRolling = true;

        targetNumber = Random.Range(1, 7); // Випадкове число 1-6
        StartCoroutine(RotateDiceRoutine(targetNumber));
    }
    private IEnumerator RotateDiceRoutine(int number)
    {
        // 1. Вмикаємо хаотичне обертання
        float elapsedTime = 0f;
        while (elapsedTime < rollingDuration)
        {
            float speedFactor = slowDownCurve.Evaluate(elapsedTime / rollingDuration);
            transform.Rotate(Random.onUnitSphere * rollingSpeed * speedFactor * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 2. Розвертаємо кубик у правильне положення
        Quaternion finalRotation = faceRotations[number - 1];
        elapsedTime = 0f;

        transform.rotation = finalRotation;
        yield return new WaitForSeconds(1); // чекаємо 1 секунду, щоб показати, що випало

        // 3. Виставляємо точну фінальну позицію
        transform.rotation = finalRotation;
        isRolling = false;
        onLandedCallback?.Invoke(number);
        Debug.Log($"Випало: {number}");
    }
}
