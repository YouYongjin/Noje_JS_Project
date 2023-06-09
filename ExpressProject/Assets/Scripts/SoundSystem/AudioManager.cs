using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioManager() { }
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource buttonSource;

    public float soundVolume = 1.0f;
    public float bgmVolume = 1.0f;

    private bool fadeInMusicflag = false;

    private void Awake()                                    // �̱����̱� ������ ó�����ִ� �͵�
    {
        if(Instance != null)
        {
            Destroy(gameObject);                            //���� ��ü ����
            return;
        }
        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);                  //�� �̵��� �ı� ���� �ʰ� �ϱ� ���ؼ�
        }
    }
    public void PlayMusic(AudioClip clip)
    {
        if (!clip) return;
        musicSource.clip = clip;
        musicSource.volume = bgmVolume;
        musicSource.Play();
    }
    public void PlayOneShot(AudioClip clip)
    {
        if (!clip) return;
        musicSource.clip = clip;
        musicSource.volume = bgmVolume;
        musicSource.PlayOneShot(sfxSource.clip);
    }
    public void PlayOneShotButton(AudioClip clip)
    {
        if (!clip) return;
        musicSource.clip = clip;
        musicSource.volume = soundVolume;
        musicSource.PlayOneShot(buttonSource.clip);
    }

    public IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float startVolume = 0.0f;
        audioSource.volume = startVolume;
        audioSource.Play();

        while(audioSource.volume < bgmVolume)
        {
            audioSource.volume += bgmVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
    }
    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume < bgmVolume)
        {
            audioSource.volume += bgmVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
    }

    public void FadeInMusic(AudioClip newMusic, float fadeTime)
    {
        if (!newMusic) return;
        if (fadeInMusicflag) return;

        StartCoroutine(FadeInMusicroutine(newMusic, fadeTime));
    }

    public IEnumerator FadeInMusicroutine(AudioClip newMusic, float fadeTime)
    {
        fadeInMusicflag = true;
        //���� ���� ���̵� �ƿ�
        yield return StartCoroutine(FadeOut(musicSource, fadeTime));
        // ���ο� ������ ���̵���
        musicSource.clip = newMusic;
        yield return StartCoroutine(FadeIn(musicSource, fadeTime));

        fadeInMusicflag = false;
    }
}

