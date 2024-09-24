using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : SingletonBehavior<UserDataManager>
{
    // ����� ���� ������ ���� ����
    public bool ExistsSavedData { get ; private set; }
    // ��� ���� ������ �ν��Ͻ��� �����ϴ� �����̳�
    // ��� ���������� Ŭ������ IUserData �������̽��� �����ϱ� ������
    // IUserData Ÿ������ �����̳ʸ� �����ϸ� ��� ���������� Ŭ������ �� �����̳ʿ� ������ �� ����
    public List<IUserData> UserDataList { get; private set; } = new List<IUserData>();

    protected override void Init()
    {
        base.Init(); // �̱��� �ν��Ͻ� ó���� init �Լ����� ����Ǳ� ������ ����� ��

        // ��� ���� �����͸� UserDataList�� �߰�
        UserDataList.Add(new UserSettingData());
        UserDataList.Add(new UserGoodsData());
    }

    // ��� ���������͸� �⺻������ �ʱ�ȭ�ϴ� �Լ�
    public void SetDefaultUserData()
    {
        for(int i = 0; i< UserDataList.Count; i++)
        {
            UserDataList[i].SetDefaultData();
        }
    }

    // ��� ���������� Ŭ������ LoadData �Լ��� ȣ�����ִ� �Լ�
    public void LoadUserData()
    {
        ExistsSavedData = PlayerPrefs.GetInt("ExistsSavedData") == 1 ? true : false;

        // ����� �����Ͱ� �����Ѵٸ�
        if(ExistsSavedData)
        {
            // ��� ���������� Ŭ������ LoadData�� ȣ��
            for (int i = 0; i < UserDataList.Count; i++)
            {
                UserDataList[i].LoadData();
            }
        }
    }

    // ��� ���������� Ŭ������ SaveData �Լ��� ȣ���ؼ� ��� ���������͸� �����ϴ� �Լ�
    public void SaveUserData()
    {
        bool hasSaveError = false;

        for (int i = 0; i < UserDataList.Count; i++)
        {
            bool isSaveSuccess =  UserDataList[i].SaveData(); // ���̺갡 ���������� �̷�������� Ȯ��
            if(!isSaveSuccess) // ������ ���ٸ�
            {
                hasSaveError = true;
            }
        }

        // �̷��� �Ǹ� for���� ���������� ��, ��, ��� ���̺� ������ ������ ��
        // �ϳ��� ������ �߻��� ����������Ŭ������ �ִٸ� hasSaveError = true�� �� ����.

        // ���̺� ������ �ϳ��� �߻����� �ʾҴٸ�(���̺갡 ���������� �̷������ ����)
        if (!hasSaveError)
        {
            ExistsSavedData = true;
            PlayerPrefs.SetInt("ExistsSavedData", 1);
            PlayerPrefs.Save(); // ���� ����̽��� ����
        }
    }
}
