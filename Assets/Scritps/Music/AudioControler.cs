using UnityEngine;

public class AudioControler : MonoBehaviour
{

    [SerializeField] private AudioSource BackGround;
    [SerializeField] private AudioSource EffectMusic;
    [SerializeField]  private AudioClip effectButtonClick;
    private void Start()
    {
        BackGround.Play();
        BackGround.loop = true;
    }
    private void ButtonPressed()
    {
    }
}
