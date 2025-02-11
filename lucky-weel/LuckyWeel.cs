using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyWeel : MonoBehaviour
{
    public delegate void RewardDelete(int index);
    [System.Serializable]
    public enum Direction
    {
        Up, Left, Right, Down
    }
    [SerializeField] private int _itemCount;
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _spacing;
    [SerializeField] private Direction _begin;
    [SerializeField] private RewardDelete _rewardDelegate;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private int ROUND_MIN = 1;
    [SerializeField] private int SECOND_SPINNING = 3;
    public RewardDelete OnRewardReceive => _rewardDelegate;

    private GameObject[] _items;
    private Sprite[] _itemSprites;
    private (float, float)[] _angles;
    private int i;
    private float Step => -360 / _itemCount;
    private bool isSpinning = false;

    private void Start()
    {
        _items = new GameObject[_itemCount];
        _itemSprites = new Sprite[_itemCount];
        _angles = new (float, float)[_itemCount];
    }

    [Button]
    public void Init()
    {
        float crrAngle = 0;
        float step = Step;
        float halfStep = step / 2;
        for (i = 0; i < _itemCount; i++)
        {
            var item = GenItem(_prefab, crrAngle);
            //_items[i] = item;
            _angles[i] = (crrAngle + halfStep, crrAngle - halfStep);
            item.GetComponentInChildren<TMPro.TMP_Text>().text = i.ToString();
            var spacing = GenItem(_spacing, crrAngle - halfStep);
            crrAngle += step;
        }
        float parentAngle = 0;
        switch (_begin)
        {
            case Direction.Down: parentAngle = 180; break;
            case Direction.Up: crrAngle = 0; break;
            case Direction.Left: parentAngle = 90; break;
            case Direction.Right: parentAngle = -90; break;
        }
        var parentRotation = _parent.rotation;
        parentRotation.eulerAngles = parentAngle * Vector3.forward;
        _parent.rotation = parentRotation;

        GameObject GenItem(GameObject prefab, float crrAngle)
        {
            var item = Instantiate(prefab, _content);
            item.gameObject.SetActive(true);
            //item.transform.position = Vector3.zero;
            var rotation = item.transform.rotation;
            rotation.eulerAngles = crrAngle * Vector3.forward;
            item.transform.rotation = rotation;
            return item;
        }
    }

    [Button]
    public void StartSpinning(int index = -1)
    {
        if (isSpinning)
            return;
        if (index < 0 || index >= _itemCount)
        {
            index = Random.Range(0, _itemCount);
        }
        Debug.Log("Index Result: " + index);
        float angle = ROUND_MIN * 360;
        //MoveToZero();
        // move to 0
        var rotation = _content.transform.localRotation;
        var crrAngle = rotation.eulerAngles.z;
        if (crrAngle > 0)
            crrAngle += 360 - crrAngle;
        else
            crrAngle = Mathf.Abs(crrAngle);
        //end move to 0
        angle += crrAngle;
        var center = Mathf.Abs((_angles[index].Item1 + _angles[index].Item2) / 2);
        angle += center + Random.Range(Mathf.CeilToInt(Step / 2), Mathf.FloorToInt(-Step / 2));
        StartCoroutine(Spinning(angle, index));
        //rotation.eulerAngles = angle * Vector3.forward;
        //_content.transform.rotation = rotation;
    }

    private IEnumerator Spinning(float angle, int index)
    {
        isSpinning = true;
        float secondSpining = SECOND_SPINNING;
        float speed = (angle / secondSpining) * Time.deltaTime;
        var rotation = _content.transform.localRotation;
        float crrAngle = rotation.eulerAngles.z;
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        float value = 0;
        while (crrAngle <= angle)
        {
            rotation.eulerAngles = crrAngle * Vector3.forward;
            _content.transform.localRotation = rotation;
            value = curve.Evaluate(crrAngle / angle);
            crrAngle += speed * value;
            yield return wait;
        }
        rotation.eulerAngles = angle * Vector3.forward;
        _content.transform.localRotation = rotation;
        Debug.Log("End");
        OnRewardReceive?.Invoke(index);
        isSpinning = false;
    }
}
