using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rotator), typeof(SpriteRenderer), typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeChangeDuration;
    [SerializeField] private Range _volumeRange;

    private Rotator _rotator;
    private SpriteRenderer _renderer;
    private AudioSource _audioSource;
    private Coroutine _currentCoroutine;
    private bool _isEnabled = false;

    public void Switch()
    {
        StopCurrentCoroutine();

        if (_isEnabled)
            Disable();
        else
            Enable();
    }

    private void Awake()
    {
        _rotator = GetComponent<Rotator>();
        _renderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        _rotator.enabled = false;
        _renderer.enabled = false;
    }

    private void Enable()
    {
        _currentCoroutine = StartCoroutine(ChangeVolume(_volumeRange.Min, _volumeRange.Max));
        _rotator.enabled = true;
        _renderer.enabled = true;
        _isEnabled = true;
    }

    private void Disable()
    {
        _currentCoroutine = StartCoroutine(ChangeVolume(_volumeRange.Max, _volumeRange.Min));
        _rotator.enabled = false;
        _renderer.enabled = false;
        _isEnabled = false;
    }

    private void StopCurrentCoroutine()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }

    private IEnumerator ChangeVolume(float from, float to)
    {
        float startTime = Time.time;

        while (Time.time < startTime + _volumeChangeDuration)
        {
            _audioSource.volume = Mathf.Lerp(from, to, (Time.time - startTime) / _volumeChangeDuration);
            yield return null;
        }

        _audioSource.volume = to;
    }
}

[System.Serializable]
public class Range
{
    public float Min;
    public float Max;
}
