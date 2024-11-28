using TMPro;
using UnityEngine;

public abstract class Text<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _text;
    [SerializeField] protected Spawner<T> _spawner;
    [SerializeField] protected string _name;

    private void OnEnable()
    {
        _spawner.SendInfo += ShowText;
    }

    private void OnDisable()
    {
        _spawner.SendInfo -= ShowText;
    }

    private void ShowText(float allTime, float created, float activeOnScene)
    {
        _text.text = $"{_name}\n" +
            $"\n���������� ����������� �������� �� �� �����: {allTime}\n" +
            $"\n���������� ��������� ��������: {created}\n" +
            $"\n���������� �������� �������� �� �����: {activeOnScene}";
    }
}