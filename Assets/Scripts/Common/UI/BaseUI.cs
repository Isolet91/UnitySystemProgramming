using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

// 함수를 담을 수 있는 변수라고 생각
// 동일한 UI화면에 대해서도 어떤 상황에서는 A라는 기능을 실행해줘야하고
// 어떤 상황에서는 B라는 기능을 실행해줘야 할 때가 있음
// 그렇기 때문에 UI화면 클래스 안에서 이런 OnShow나 OnClose를 정의하는 것보다
// 그 화면을 열겠다고 UI매니저를 호출할 때 어떤 행위를 해줘야 할지 정의해서
// 넘겨주는 것이 더 유연하게 원하는 기획 내용을 구현할 수 있다.
public class BaseUIData
{
    public Action OnShow;         // UI 화면을 열었을 때 해주고 싶은 행위를 정의
    public Action OnClose;        // UI 화면을 닫으면서 실행해야 하는 기능 정의
}

public class BaseUI : MonoBehaviour
{
    //UI 열어줄 때 재생할 애니메이션 변수
    public Animation m_UIOpenAnim;

    private Action m_OnShow;
    private Action m_OnClose;
    // 이 변수들은 화면을 열 때 매개변수로 넘어온 UIData 클래스에
    // 정의된 OnShow와 OnClose 그대로 BaseUI 클래스에 있는 m_OnShow와 OnClose에
    // m_OnShow = uiData.Onshow; 이런 식으로

    public virtual void Init(Transform anchor)
    {
        Logger.Log($"{GetType()}::Init");

        m_OnShow = null; 
        m_OnClose = null;

        transform.SetParent(anchor); // anchor : UI 캔버스 컴포넌트의 트랜스폼

        var rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.offsetMin = Vector3.zero;
        rectTransform.offsetMax = Vector3.zero;
    }

    // UI화면에 UI요소를 세팅해주는 함수
    public virtual void SetInfo(BaseUIData uiData)
    {
        Logger.Log($"{GetType()}::SetInfo");

        m_OnShow = uiData.OnShow;
        m_OnClose = uiData.OnClose;
    }

    // UI화면을 실제로 열어서 화면에 표시해 주는 함수
    public virtual void ShowUI()
    {
        if(m_UIOpenAnim)
        {
            m_UIOpenAnim.Play();
        }

        m_OnShow?.Invoke(); // m_OnShow가 null이 아니라면 m_Onshow 실행
                            // ? 키워드를 사용한 예외처리
                            // _action?.Invoke(3); // if(_action!=null) 를 ? 키워드를 사용하여 null임을 체크
        m_OnShow = null;    // 실행 후 널값으로 초기화
    }

    // 화면을 닫는 함수
    public virtual void CloseUI(bool isCloseAll = false)
    {
        // isCloseAll : 씬을 전환하거나 할 때 열려있는 화면을
        // 전부 다 닫아 줄 필요가 있을 때
        // true를 넘겨줘서 화면을 닫을 때 필요한 처리들을
        // 다 무시하고 화면만 닫아주기 위해서 사용할 것임.
        if(!isCloseAll)
        {
            m_OnClose?.Invoke();
        }
        m_OnClose = null;

        UIManager.Instance.CloseUI(this);
    }

    // 닫기 버튼을 눌렀을 때 실행하는 함수
    // 거의 대부분 UI에는 닫기 버튼이 있으므로
    // 여기서 아예 닫기 버튼 기능을 구현
    public virtual void OnClickloseButton()
    {
        AudioManager.Instance.PlaySFX(SFX.ui_button_click);
        CloseUI();
    }    
}
