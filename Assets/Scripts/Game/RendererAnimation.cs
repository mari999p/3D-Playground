using DG.Tweening;
using NaughtyAttributes;
using Playground.Utils;
using Playground.Utils.Logger;
using Playground.Utils.Logger;
using UnityEngine;

namespace Playground.Game
{
    public class RendererAnimation : MonoBehaviour
    {
        #region Variables

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _colorStart = Color.white;
        [SerializeField] private Color _colorEnd = Color.cyan;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _delay = 0.3f;
        [SerializeField] private Ease _ease = Ease.Linear;
        [SerializeField] private int _loops = 1;
        [SerializeField] private LoopType _loopType;

        private Tween _tween;

        #endregion

        #region Private methods

        [Button]
        private void Animation()
        {
            _tween?.Kill();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_spriteRenderer.DOColor(_colorStart, _duration).SetEase(_ease));
            sequence.Insert(0, transform.DOMoveY(1, _duration).From(0));
            sequence.Append(_spriteRenderer.DOColor(_colorEnd, _duration));
            sequence.Insert(_duration, transform.DOMoveY(0, _duration));
            sequence.Insert(_duration, transform.DORotate(new Vector3(0, 0, 360), _duration, RotateMode.FastBeyond360));

            _tween = sequence;
            // _tween = _spriteRenderer
            //     .DOColor(_color, _duration)
            //     .SetDelay(_delay)
            //     .SetEase(_ease)
            //     .SetLoops(_loops, _loopType)
            _tween
                .OnStart(() => this.Error("OnStart"))
                .OnPlay(() => this.Error("OnPlay"))
                // .OnUpdate(() => this.Error("OnUpdate"))
                .OnComplete(() => this.Error("OnComplete"))
                .OnKill(() => this.Error("OnKill"));
        }

        #endregion
    }
}