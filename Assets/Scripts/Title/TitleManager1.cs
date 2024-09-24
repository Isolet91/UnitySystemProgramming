using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager1 : MonoBehaviour
{
    // 로고
    public Animation LogoAnim;
    public TextMeshProUGUI LogoTxt;

    // 타이틀
    public GameObject Title;
    

    private void Awake()
    {
        LogoAnim.gameObject.SetActive(true);
        Title.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(LoadGameCo());
    }

    private IEnumerator LoadGameCo()
    {
        // 이 코루틴 함수는 게임의 로딩을 처음 시작하는 중요한 함수이기 때문에
        // 로그를 찍음
        // GetType() : 클래스 명을 출력
        // "타이틀 매니저에서 호출하는 로드게임코루틴 이라는 함수" 확인

        Logger.Log($"{GetType()}::LoadGameCo");

        LogoAnim.Play(); //로고 애니메이션 재생
        yield return new WaitForSeconds(LogoAnim.clip.length); //애니메이션 클립의 길이만큼 대기

        LogoAnim.gameObject.SetActive(false);
        Title.SetActive(true);

       /* m_AsyncOperation = SceneLoader.Instance.LoadSceneAsync(SceneType.Lobby);
        if (m_AsyncOperation == null)
        {
            Logger.Log("Lobby async loading error.");
            yield break;
        }

        m_AsyncOperation.allowSceneActivation = false;

        LoadingSlider.value = 0.5f;
        LoadingProgresssTxt.text = $"{((int)LoadingSlider.value * 100)}%";
        yield return new WaitForSeconds(0.5f);

        while (m_AsyncOperation.isDone) // 로딩 진행중
        {
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
       */
  
    }

    
       
}
