using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

namespace thirdparty.card_library.data.Match
{
    public class MatchPhase0 : IMatchUI
    {
        const float DURATION = 0.3f;
        const float DELAY = 0.1f;
        const int NUMBER = 52;

        public List<RectTransform> prefabs;
        public RectTransform target;
        public Ease ease;

        private Vector3 position;

        private void Start()
        {
            position = prefabs[0].position;
        }

        public override void DoAnimation()
        {
            StartCoroutine(CoAnimation());
        }

        private IEnumerator CoAnimation()
        {
            int count = 0;
            WaitForSeconds wait = new WaitForSeconds(DELAY);
            while(count < NUMBER)
            {
                var prefab = prefabs.Find(x => x.gameObject.activeSelf);
                prefab.gameObject.SetActive(true);
                prefab.DOMove(target.position, DURATION).SetEase(ease).OnComplete(() =>
                {
                    prefab.transform.position = position;
                    target.gameObject.SetActive(true);
                });
                prefab.DORotate(target.eulerAngles, DURATION).SetEase(ease).OnComplete(() =>
                {
                    prefab.transform.position = position;
                });
                ++count;
                yield return wait;
            }
            yield return wait;
            yield return wait;
            foreach(var e in prefabs)
            {
                e.position = target.position;
            }
        }

        public override object[] SelectCards()
        {
            throw new System.NotImplementedException();
        }
    }
}
