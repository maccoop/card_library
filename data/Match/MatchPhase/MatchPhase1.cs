using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace thirdparty.card_library.data.Match
{
    public class MatchPhase1 : IMatchUI
    {
        public int amount = 5;
        private const float DURATION = 0.2f;
        public RectTransform[] Targets;
        public RectTransform[] prefabs;
        public RectTransform[] BotTargets;
        public RectTransform[] BotPrefabs;
        public Ease curve;
        private Vector3 basePosition;
        private Vector3 baseRotation;
        private Vector3 sizeDelta;
        float timeDelay = DURATION;
        int i = 0;

        private void Start()
        {
            basePosition = prefabs[0].transform.position;
            sizeDelta = prefabs[0].sizeDelta;
            baseRotation = prefabs[0].transform.rotation.eulerAngles;
        }

        [Button]
        public override void DoAnimation()
        {
            float time = 0;
            for (i = 0; i < amount; i++)
            {
                var prefab = prefabs[i];
                var target = Targets[i];
                time = DoAnimationPrefab(time, prefab, target);
            }
            time = timeDelay / 2;
            for (i = 0; i < BotPrefabs.Length; i++)
            {
                var prefab = BotPrefabs[i];
                var target = BotTargets[i];
                time = DoAnimationPrefab(time, prefab, target);
            }

            float DoAnimationPrefab(float time, RectTransform prefab, RectTransform target)
            {
                prefab.gameObject.SetActive(true);
                prefab.GetComponent<CanvasGroup>().alpha = 1;
                target.gameObject.SetActive(true);
                target.GetComponent<CanvasGroup>().alpha = 0;
                prefab.transform.DORotate(target.rotation.eulerAngles, DURATION).SetEase(curve).SetDelay(time);
                prefab.transform.DOMove(target.position, DURATION, true).SetEase(curve).SetDelay(time).OnComplete(() =>
                {
                    target.GetComponent<CanvasGroup>().alpha = 1;
                    prefab.gameObject.SetActive(false);
                    prefab.transform.position = basePosition;
                    prefab.transform.eulerAngles = baseRotation;
                    if (i == prefabs.Length - 1) this.OnEnd();
                }).OnStart(() =>
                {
                    prefabs[i].gameObject.SetActive(true);
                });
                prefab.DOSizeDelta(target.sizeDelta, DURATION).SetEase(curve).SetDelay(time).OnComplete(() =>
                {
                    prefab.sizeDelta = sizeDelta;
                });
                time += timeDelay;
                return time;
            }
        }

        public override object[] SelectCards()
        {
            throw new System.NotImplementedException();
        }
    }
}
