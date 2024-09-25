using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

// �Լ��� ���� �� �ִ� ������� ����
// ������ UIȭ�鿡 ���ؼ��� � ��Ȳ������ A��� ����� ����������ϰ�
// � ��Ȳ������ B��� ����� ��������� �� ���� ����
// �׷��� ������ UIȭ�� Ŭ���� �ȿ��� �̷� OnShow�� OnClose�� �����ϴ� �ͺ���
// �� ȭ���� ���ڴٰ� UI�Ŵ����� ȣ���� �� � ������ ����� ���� �����ؼ�
// �Ѱ��ִ� ���� �� �����ϰ� ���ϴ� ��ȹ ������ ������ �� �ִ�.
public class BaseUIData
{
    public Action OnShow;         // UI ȭ���� ������ �� ���ְ� ���� ������ ����
    public Action OnClose;        // UI ȭ���� �����鼭 �����ؾ� �ϴ� ��� ����
}

public class BaseUI : MonoBehaviour
{
    //UI ������ �� ����� �ִϸ��̼� ����
    public Animation m_UIOpenAnim;

    private Action m_OnShow;
    private Action m_OnClose;
    // �� �������� ȭ���� �� �� �Ű������� �Ѿ�� UIData Ŭ������
    // ���ǵ� OnShow�� OnClose �״�� BaseUI Ŭ������ �ִ� m_OnShow�� OnClose��
    // m_OnShow = uiData.Onshow; �̷� ������

    public virtual void Init(Transform anchor)
    {
        Logger.Log($"{GetType()}::Init");

        m_OnShow = null; 
        m_OnClose = null;

        transform.SetParent(anchor); // anchor : UI ĵ���� ������Ʈ�� Ʈ������

        var rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.offsetMin = Vector3.zero;
        rectTransform.offsetMax = Vector3.zero;
    }

    // UIȭ�鿡 UI��Ҹ� �������ִ� �Լ�
    public virtual void SetInfo(BaseUIData uiData)
    {
        Logger.Log($"{GetType()}::SetInfo");

        m_OnShow = uiData.OnShow;
        m_OnClose = uiData.OnClose;
    }

    // UIȭ���� ������ ��� ȭ�鿡 ǥ���� �ִ� �Լ�
    public virtual void ShowUI()
    {
        if(m_UIOpenAnim)
        {
            m_UIOpenAnim.Play();
        }

        m_OnShow?.Invoke(); // m_OnShow�� null�� �ƴ϶�� m_Onshow ����
                            // ? Ű���带 ����� ����ó��
                            // _action?.Invoke(3); // if(_action!=null) �� ? Ű���带 ����Ͽ� null���� üũ
        m_OnShow = null;    // ���� �� �ΰ����� �ʱ�ȭ
    }

    // ȭ���� �ݴ� �Լ�
    public virtual void CloseUI(bool isCloseAll = false)
    {
        // isCloseAll : ���� ��ȯ�ϰų� �� �� �����ִ� ȭ����
        // ���� �� �ݾ� �� �ʿ䰡 ���� ��
        // true�� �Ѱ��༭ ȭ���� ���� �� �ʿ��� ó������
        // �� �����ϰ� ȭ�鸸 �ݾ��ֱ� ���ؼ� ����� ����.
        if(!isCloseAll)
        {
            m_OnClose?.Invoke();
        }
        m_OnClose = null;

        UIManager.Instance.CloseUI(this);
    }

    // �ݱ� ��ư�� ������ �� �����ϴ� �Լ�
    // ���� ��κ� UI���� �ݱ� ��ư�� �����Ƿ�
    // ���⼭ �ƿ� �ݱ� ��ư ����� ����
    public virtual void OnClickloseButton()
    {
        AudioManager.Instance.PlaySFX(SFX.ui_button_click);
        CloseUI();
    }    
}
