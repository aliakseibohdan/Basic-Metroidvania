using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private AudioClip _invisibilityClip;
    [SerializeField] private AudioClip _coinCollectClip;
    [SerializeField] private AudioClip _explosionClip;

    private SpriteRenderer[] _spriteRenderers;
    private Material[] _materials;

    private Coroutine _invisibilityCoroutine;
    private Coroutine _effectCollectCoroutine;

    public bool IsChanging { get; private set; }

    private int _negativeBool = Shader.PropertyToID("_Negative");
    private int _negativeAmount = Shader.PropertyToID("_NegativeAmount");

    private int _hitEffect = Shader.PropertyToID("_HitEffect");
    private int _hitEffectBlend = Shader.PropertyToID("_HitEffectBlend");
    private int _hitEffectColor = Shader.PropertyToID("_HitEffectColor");
    private bool _isCollectEffecting;

    private void Awake()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        _materials = new Material[_spriteRenderers.Length];
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i] = _spriteRenderers[i].material;
        }

        if (_materials[0].GetFloat(_negativeBool) == 0)
        {
            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetFloat(_negativeBool, 1f);
            }
        }

        if (_materials[0].GetFloat(_hitEffect) == 0)
        {
            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetFloat(_hitEffect, 1f);
            }
        }
    }

    #region Invisibility



    #endregion

    #region Collection Effects

    public void PlayCollectionEffect(float time, Color color, AudioClip clip)
    {
        if (_isCollectEffecting)
        {
            StopCoroutine(_effectCollectCoroutine);
            _isCollectEffecting = false;

            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetColor(_hitEffectColor, color);
            }

            _effectCollectCoroutine = StartCoroutine(CollectionEffect(_materials[0].GetFloat(_hitEffectBlend), 1f, time));

            //AudioManager.PlayClip(clip, 0.65f);
        }
    }

    private IEnumerator CollectionEffect(float startValue, float endValue, float time)
    {
        _isCollectEffecting = true;

        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            float lerpedAmount = Mathf.Lerp(startValue, endValue, (elapsedTime / time));

            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetFloat(_hitEffectBlend, lerpedAmount);
            }

            yield return null;
        }

        elapsedTime = 0f;
        while(elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            float lerpedAmount = Mathf.Lerp(endValue, 0f, (elapsedTime / time));

            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetFloat(_hitEffectBlend, lerpedAmount);
            }

            yield return null;
        }

        _isCollectEffecting = false;
    }

    #endregion
}
