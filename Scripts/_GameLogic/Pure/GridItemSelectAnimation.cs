using _Game.Scripts._GameLogic.Grid;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts._GameLogic.Pure
{
    public class GridItemSelectAnimation : MonoBehaviour , ISelectActions
    {
        public void OnSelect()
        {
            transform.DOKill();
            float currentY = transform.localPosition.y;
            float targetY = Mathf.Min(0.5f, currentY + (0.5f - currentY));
            if (currentY >= 0.5f)
                return;
            transform.DOLocalMoveY(targetY, 0.2f)
                .SetLink(gameObject)
                .SetEase(Ease.Linear);
        }

        public void OnDeselect()
        {
            transform.DOKill();
            float currentY = transform.localPosition.y;
            float targetY = Mathf.Max(0f, currentY - currentY);
            if (currentY <= 0f)
                return;
            transform.DOLocalMoveY(targetY, 0.2f)
                .SetLink(gameObject)
                .SetEase(Ease.Linear);
        }
    }
}
