using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public enum BGM
{
    lobby,
    COUNT
}

public enum SFX
{
    chapter_clear,
    stage_clear,
    ui_button_click,
    COUNT
}

public class AudioManager : SingletonBehavior<AudioManager>
{
    // �� ������Ʈ�� ������ ����
    public Transform BGMTransform;
    public Transform SFXTransform;
    // ����������� �ε��� ���
    private const string AUDIO_PATH = "Audio";
    // ��� BGM ����� ���ҽ��� ������ �����̳�
    private Dictionary<BGM, AudioSource> m_BGMPlayer = new Dictionary<BGM, AudioSource>();
    // BGM �����ؼ� ���� ����ϰ� �ִ� ������ҽ� ������Ʈ
    private AudioSource m_CurrentBGMSource;
    // ��� SFX ����� ���ҽ��� ������ �����̳�
    private Dictionary<SFX, AudioSource> m_SFXPlayer = new Dictionary<SFX, AudioSource>();

    protected override void Init()
    {
        base.Init();

        LoadBGMPlayer();
        LoadSFXPlayer();
    }

    private void LoadBGMPlayer()
    {
        for(int i = 0; i< (int) BGM.COUNT; i++)
        {
            var audioName = ((BGM)i).ToString();
            var pathStr = $"{AUDIO_PATH}/{audioName}";
            var audioClip = Resources.Load(pathStr, typeof(AudioClip)) as AudioClip;
            if(!audioClip)
            {
                Logger.LogError($"{audioName} clip does not exist.");
                continue;
            }

            var newGo = new GameObject(audioName);
            var newAudioSource = newGo.AddComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            newAudioSource.loop = true;
            newAudioSource.playOnAwake = false;
            newGo.transform.parent = BGMTransform;

            // ��ųʸ��� �ش� Enum Ű������ ������ ����� �ҽ��� ����
            m_BGMPlayer[(BGM)i] = newAudioSource;
        }
    }

    // BGM�� ���� ����
    private void LoadSFXPlayer()
    {
        for (int i = 0; i < (int)SFX.COUNT; i++)
        {
            var audioName = ((SFX)i).ToString();
            var pathStr = $"{AUDIO_PATH}/{audioName}";
            var audioClip = Resources.Load(pathStr, typeof(AudioClip)) as AudioClip;
            if (!audioClip)
            {
                Logger.LogError($"{audioName} clip does not exist.");
                continue;
            }

            var newGo = new GameObject(audioName);
            var newAudioSource = newGo.AddComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            newAudioSource.loop = false;
            newAudioSource.playOnAwake = false;
            newGo.transform.parent = SFXTransform;

            m_SFXPlayer[(SFX)i] = newAudioSource;
        }
    }

    // BGM �÷��� �Լ�
    public void PlayBGM(BGM bgm)
    {
        if(m_CurrentBGMSource)
        {
            // ���� ����ǰ� �ִ� BGM �ҽ��� �ִٸ�
            // ����� ���߰� null������ �ʱ�ȭ
            m_CurrentBGMSource.Stop();
            m_CurrentBGMSource = null;
        }

        // ����ϰ� ���� BGM�� �����ϴ��� Ȯ��
        // �������� ������ ������ �߻���Ű����.
        if(m_BGMPlayer.ContainsKey(bgm) == false)
        {
            Logger.LogError($"Invalid clip name. {bgm}");
            return;
        }

        // �����Ѵٸ� �ش� ������ҽ� ������Ʈ�� ���������ְ�
        // ���
        m_CurrentBGMSource = m_BGMPlayer[bgm];
        m_CurrentBGMSource.Play();
    }

    // BGM �Ͻ�����
    public void PauseBGM()
    {
        if (m_CurrentBGMSource) m_CurrentBGMSource.Pause();
    }
    // BGM �����
    public void ResumeBGM()
    {
        if (m_CurrentBGMSource) m_CurrentBGMSource.UnPause();
    }
    // BGM ����
    public void StopBGM()
    {
        if (m_CurrentBGMSource) m_CurrentBGMSource.Stop();
    }
    // SFX �÷���(BGM ���� ����)
   
    // ȿ���� ���� (ȿ������ ª�� �ð��� ����ǰ� �����Ƿ� ���� �Ͻ����� ���� �ʿ� ����.
    public void PlaySFX(SFX sfx)
    {
        if(!m_SFXPlayer.ContainsKey(sfx))
        {
            Logger.LogError($"Invalid clip name. ({sfx})");
            return;
        }

        m_SFXPlayer[sfx].Play();
    }

    // ���Ұ�
    public void Mute()
    {
        foreach(var audioSourceItem in m_BGMPlayer)
        {
            audioSourceItem.Value.volume = 0f;
        }

        foreach(var audioSourceItem in m_SFXPlayer)
        {
            audioSourceItem.Value.volume = 0f;
        }
    }

    // ���Ұ� ����
    public void UnMute()
    {
        foreach(var audioSourceItem in m_BGMPlayer)
        {
            audioSourceItem.Value.volume = 1f;
        }

        foreach(var audioSourceItem in m_SFXPlayer)
        {
            audioSourceItem.Value.volume = 1f;
        }
    }

}
