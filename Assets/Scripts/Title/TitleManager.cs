using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // 로고
    public Animation LogoAnim;
    public TextMeshProUGUI LogoTxt;

    // 타이틀
    public GameObject Title;
    public Slider LoadingSlider;
    public TextMeshProUGUI LoadingProgresssTxt;

    private AsyncOperation m_AsyncOperation;

    private void Awake()
    {
        LogoAnim.gameObject.SetActive(true);
        Title.SetActive(false);
    }

    private void Start()
    {
        // 유저 데이터 로드
        UserDataManager.Instance.LoadUserData();

        // 저장된 유저 데이터가 없으면 기본값으로 세팅 후 저장
        if(!UserDataManager.Instance.ExistsSavedData)
        {
            UserDataManager.Instance.SetDefaultUserData();
            UserDataManager.Instance.SaveUserData();
        }

        var confirmUIData = new ConfirmUIData();
        confirmUIData.confirmType = ConfirmType.OK;
        confirmUIData.TitleTxt = "UI Test";
        confirmUIData.DescTxt = "This is UI Test";
        confirmUIData.OKBtnTxt = "OK";
        UIManager.Instance.OpenUI<ConfirmUI>(confirmUIData);

        //StartCoroutine(LoadGameCo());
    }

    private IEnumerator LoadGameCo()
    {

        // 이 코루틴 함수는 게임의 로딩을 처음 시작하는 중요한 함수이기 때문에
        // 로그를 찍음
        // GetType() : 클래스 명을 출력
        // "타이틀 매니저에서 호출하는 로드게임코루틴 이라는 함수" 확인
        Logger.Log($"{GetType()}::LoadGameCo");

        LogoAnim.Play();                                                                 // 로고 애니메이션 재생
        yield return new WaitForSeconds(LogoAnim.clip.length);                           // 애니메이션 클립의 길이만큼 대기

        LogoAnim.gameObject.SetActive(false);
        Title.SetActive(true);

        m_AsyncOperation = SceneLoader.Instance.LoadSceneAsync(SceneType.Lobby);
        if(m_AsyncOperation == null)
        {
            Logger.Log("Lobby async loading error.");
            yield break;
        }

        m_AsyncOperation.allowSceneActivation = false;

        // 로딩 시간이 짧은 경우 로딩 슬라이더 변화가 너무 빨라 보이지 않을 수 있다.
        // 일부러 몇 초간 50%로 보여줌으로써 시각적으로 더 자연스럽게 처리한다
        LoadingSlider.value = 0.5f;
        LoadingProgresssTxt.text = $"{((int)LoadingSlider.value * 100)}%";
        yield return new WaitForSeconds(0.5f);

        while(m_AsyncOperation.isDone) // 로딩이 진행중일 때
        {
            // 로딩 슬라이더 업데이트
            LoadingSlider.value = m_AsyncOperation.progress < 0.5f ? 0.5f : m_AsyncOperation.progress;
            LoadingProgresssTxt.text = $"{((int)LoadingSlider.value * 100)}%";

            // 씬 로딩이 완료 되었다면 로비로 전환하고 코루틴 종료
            if (m_AsyncOperation.progress >= 0.9f) // 유니티에서 이렇게 만듦. progress가 0.9에서 멈춤
            {
                m_AsyncOperation.allowSceneActivation = true;
                yield break;
            }

            yield return null;
        }

        // 씬 로딩이 완료 되었다면 로비로 전환하고 코루틴 종료
        if (m_AsyncOperation.progress >= 0.9f) // 유니티에서 이렇게 만듦. progress가 0.9에서 멈춤
        {
            LoadingSlider.value = m_AsyncOperation.progress < 0.5f ? 0.5f : m_AsyncOperation.progress;
            LoadingProgresssTxt.text = $"{((int)LoadingSlider.value * 100)}%";
            m_AsyncOperation.allowSceneActivation = true;
            yield break;
        }
    }
}
