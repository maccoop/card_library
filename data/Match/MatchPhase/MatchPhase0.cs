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
        public float TIME = 0.7f;
        public float DURATION = 0.1f;
        public float DELAY = 0.05f;
        int NUMBER => Mathf.CeilToInt(TIME / DELAY);

        public List<RectTransform> prefabs;
        public bool[] actives;
        public RectTransform target;
        public Ease ease;
        private Vector3 position;

        private void Start()
        {
            position = prefabs[0].position;
            actives = new bool[52];
        }

        public override void DoAnimation()
        {
            StartCoroutine(CoAnimation());
        }
        int i = 0;
        private IEnumerator CoAnimation()
        {
            int count = 0;
            WaitForSeconds wait = new WaitForSeconds(DELAY);
            while (count < NUMBER)
            {
                RectTransform prefab = null;
                int index = 0;
                for (i = 1; i < prefabs.Count; i++)
                {
                    if (!actives[i])
                    {
                        prefab = prefabs[i];
                        index = i;
                        break;
                    }
                }
                if (prefab == null)
                {
                    prefabs.Add(Instantiate<RectTransform>(prefabs[0], prefabs[0].parent));
                    prefab = prefabs[prefabs.Count - 1];
                    index = prefabs.Count - 1;
                }
                prefab.gameObject.SetActive(true);
                prefab.transform.position = position;
                prefab.SetAsLastSibling();
                prefab.GetComponent<CanvasGroup>().alpha = 1;
                actives[index] = true;
                prefab.DOMove(target.position, DURATION).SetEase(ease).OnComplete(() =>
                {
                    prefab.gameObject.SetActive(false);
                    target.gameObject.SetActive(true);
                    actives[index] = false;
                    target.GetComponent<CanvasGroup>().alpha = 1;
                });
                prefab.DORotate(target.eulerAngles, DURATION).SetEase(ease).OnComplete(() =>
                {
                    prefab.transform.position = position;
                });
                ++count;
                yield return wait;
            }
            yield return new WaitForSeconds(DURATION);
            foreach (var e in prefabs)
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
