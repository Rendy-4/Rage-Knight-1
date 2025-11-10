using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------------------Audio Source-------------------")]
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------------------Audio Clip-------------------")]
    public AudioClip Background;
    [Header("SFX")]
    public AudioClip ButtonClick;
    public AudioClip WalkOnGrass;
    public AudioClip WalkOnStone;
    public AudioClip Attack;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        BGMSource.clip = Background;
        BGMSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
