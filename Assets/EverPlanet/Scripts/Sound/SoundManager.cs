using UnityEngine;

public enum BGMSound
{
    Main,
    Map1,
    TraingMap,
    Map2
}
public enum SFXSound
{
    Ready,
    ReadyGo,
    Motor,
    Drift,
    Booster
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] static GameObject _BGMManager;
    [SerializeField] static GameObject _SFXManager;

    static public SoundManager _sound;

    [SerializeField] AudioClip[] _bgmClip;
    [SerializeField] AudioClip[] _sfxClip;
    static AudioSource _audioBgmSource;
    static AudioSource _audioSfxSource;

    public float bgmVolume = 0.7f;
    public float sfxVolume = 0.7f;

    static void Init()
    {
        if (_sound == null)
        {
            GameObject gm = GameObject.Find("SoundManager");
            if (gm == null)
            {
                gm = new GameObject { name = "SoundManager" };

                gm.AddComponent<SoundManager>();
            }
            DontDestroyOnLoad(gm);
            _sound = gm.GetComponent<SoundManager>();

            _BGMManager = GameObject.Find("BGMManager");
            _SFXManager = GameObject.Find("SFXManager");
            _audioBgmSource = _BGMManager.GetComponent<AudioSource>();
            _audioSfxSource = _SFXManager.GetComponent<AudioSource>();
        }
    }
    void Awake()
    {
        Init();
    }

    //임시 로직
    private void Start()
    {
        //처음에는 리스항구에 있음
        PlayBGM(0);
    }

    void Update()
    {
        _audioBgmSource.volume = bgmVolume;
        //_audioSfxSource.volume = sfxVolume;
    }
    public void PlayBGM(int musicNum)
    {
        _audioBgmSource.clip = _bgmClip[musicNum];
        _audioBgmSource.volume = bgmVolume;//상시 조절되게 바깥으로 뺌
        _audioBgmSource.loop = true;
        _audioBgmSource.Play();
    }
    public void StopBGM(int musicNum)
    {
        _audioBgmSource.clip = _bgmClip[musicNum];
        if (_audioBgmSource.isPlaying)
        {
            _audioBgmSource.Stop();
        }
    }
    public void PlaySfx(int soundNum)
    {
        _audioSfxSource.volume = sfxVolume;//변수 값으로 조절
        _audioSfxSource.playOnAwake = true;
        _audioSfxSource.loop = false;
        _audioSfxSource.PlayOneShot(_sfxClip[soundNum]);//한개만 적용
    }

    /// <summary>
    /// 각 맵의 bgm재생
    /// 시점 : 맵 이동후
    /// </summary>
    public void MapBGMSetting(MapType mapType)
    {
        switch (mapType)
        {
            case MapType.None:
                PlayBGM(0);
                break;
            case MapType.TheRoom:
                PlayBGM(1);
                break;
            case MapType.Deongeon:
                PlayBGM(2);
                break;
            case MapType.DeongeonBoss:
                PlayBGM(3);
                break;
            default:
                break;
        }
    }
}
