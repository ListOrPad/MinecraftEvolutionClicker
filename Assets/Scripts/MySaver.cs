using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MySaver : MonoBehaviour
{
    // ������������� �� ������� GetDataEvent � OnEnable
    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    // ������������ �� ������� GetDataEvent � OnDisable
    //private void OnDisable()
    //{
    //    YandexGame.GetDataEvent -= GetLoad;
    //}

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();

            // ���� ������ ��� �� �����������, �� ����� �� ���������� � ������ Start,
            // �� �� ���������� ��� ������ ������� GetDataEvent, ����� ��������� �������
        }
    }

    public void GetLoad()
    {
        // �������� ������ �� ������� � ������ � ���� ��� �����
        // ��������, �� ����� �������� � ��������� UI.Text ������� � ������ �����:
        //textMoney.text = YandexGame.savesData.money.ToString();
        Game.CounterValue = YandexGame.savesData.CounterValue;
    }

    // ��������, ��� ��� ����� ��� ����������
    public void MySave()
    {
        // ���������� ������ � ������
        // ��������, �� ����� ��������� ���������� ����� ������:
        //YandexGame.savesData.money = money;
        YandexGame.savesData.CounterValue = Game.CounterValue;

        // ������ ������� ��������� ������
        YandexGame.SaveProgress();
    }

    private void Update()
    {
        MySave();
    }
}
