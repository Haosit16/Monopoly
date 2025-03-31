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

    [SerializeField] private float rollingDuration = 1.5f; // ��� ���������
    [SerializeField] private float rollingSpeed = 720f; // �������� ���������
    [SerializeField] private AnimationCurve slowDownCurve; // ����� �����������

    private readonly Quaternion[] faceRotations =
{
        Quaternion.Euler(-90, 0, -90),      // 1 (������ �����)
        Quaternion.Euler(0, 0, 0),    // 2
        Quaternion.Euler(0, 0, -90),     // 3
        Quaternion.Euler(-180, 0, -90),    // 4
        Quaternion.Euler(0, 0, -180),     // 5
        Quaternion.Euler(90, 0, -90)     // 6 (����� �����)
    };

    public void RollDice()
    {
        if (isRolling) return;
        isRolling = true;

        targetNumber = Random.Range(1, 7); // ��������� ����� 1-6
        StartCoroutine(RotateDiceRoutine(targetNumber));
    }
    private IEnumerator RotateDiceRoutine(int number)
    {
        // 1. ������� �������� ���������
        float elapsedTime = 0f;
        while (elapsedTime < rollingDuration)
        {
            float speedFactor = slowDownCurve.Evaluate(elapsedTime / rollingDuration);
            transform.Rotate(Random.onUnitSphere * rollingSpeed * speedFactor * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 2. ���������� ����� � ��������� ���������
        Quaternion finalRotation = faceRotations[number - 1];
        elapsedTime = 0f;

        transform.rotation = finalRotation;
        yield return new WaitForSeconds(1); // ������ 1 �������, ��� ��������, �� ������

        // 3. ����������� ����� �������� �������
        transform.rotation = finalRotation;
        isRolling = false;
        onLandedCallback?.Invoke(number);
        Debug.Log($"������: {number}");
    }
}
