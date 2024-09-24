using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ConfirmType
{
    OK,
    OK_CANCEL,
}

public class ConfirmUIData : BaseUIData
{
    public ConfirmType confirmType;
    public string TitleTxt;
    public string DescTxt;
    public string OKBtnTxt;
    public Action OnClickOKBtn;
    public string CancelBtnTxt;
    public Action OnClickCancelBtn;
}

public class ConfirmUI : BaseUI
{
    public TextMeshProUGUI TitleTxt;
    public TextMeshProUGUI DescTxt;
    public Button OKBtn;
    public Button CancelBtn;
    public TextMeshProUGUI OKBtnTxt;
    public TextMeshProUGUI CancelBtnTxt;

    private ConfirmUIData m_ConfirmUIData;
    private Action m_OnClickOkBtn;
    private Action m_OnClickCancelBtn;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        m_ConfirmUIData = uiData as ConfirmUIData;

        TitleTxt.text = m_ConfirmUIData.TitleTxt;
        DescTxt.text = m_ConfirmUIData.DescTxt;
        OKBtnTxt.text = m_ConfirmUIData.OKBtnTxt;

        m_OnClickOkBtn = m_ConfirmUIData.OnClickOKBtn;
        CancelBtnTxt.text = m_ConfirmUIData.CancelBtnTxt;
        m_OnClickCancelBtn = m_ConfirmUIData.OnClickCancelBtn;

        OKBtn.gameObject.SetActive(true);
        CancelBtn.gameObject.SetActive(m_ConfirmUIData.confirmType == ConfirmType.OK_CANCEL);
    }

    public void OnClickOKBtn()
    {
        m_OnClickOkBtn?.Invoke();
        m_OnClickOkBtn = null;
        CloseUI();
    }

    public void OnClickCancelBtn()
    {
        m_OnClickCancelBtn?.Invoke();
        m_OnClickCancelBtn = null;
        CloseUI();
    }
}
