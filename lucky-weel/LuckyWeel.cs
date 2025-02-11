using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyWeel : MonoBehaviour
{
    public int ROUND_MIN = 1;
    public int SECOND_SPINNING = 3;
    public delegate void RewardDelete(int index);
    [System.Serializable]
    public enum Direction
    {
        Up, Left, Right, Down
    }
    [SerializeField] private int _itemCount;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _spacing;
    [SerializeField] private Direction _begin;
    [SerializeField] private RewardDelete _rewardDelegate;
    [SerializeField] private AnimationCurve curve;
    public RewardDelete OnRewardReceive => _rewardDelegate;

    private GameObject[] _items;
    private Sprite[] _itemSprites;
    private (float, float)[] _position;
    private int i;
    private float Step => -360 / _itemCount;

    private void Start()
    {
        _items = new GameObject[_itemCount];
        _itemSprites = new Sprite[_itemCount];
        _position = new (float, float)[_itemCount];
    }

    [Button]
    public void Init()
    {
        float crrAngle = 0;
        float step = Step;
        float halfStep = step / 2;
        switch (_begin)
        {
            case Direction.Down: crrAngle = 180; break;
            case Direction.Up: crrAngle = 0; break;
            case Direction.Left: crrAngle = -90; break;
            case Direction.Right: crrAngle = 90; break;
        }
        for (i = 0; i < _itemCount; i++)
        {
            var item = GenItem(_prefab, crrAngle);
            //_items[i] = item;
            _position[i] = (crrAngle + halfStep, crrAngle - halfStep);
            item.GetComponentInChildren<TMPro.TMP_Text>().text = i.ToString();
            var spacing = GenItem(_spacing, crrAngle - halfStep);
            crrAngle += step;
        }

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
        if (index < 0 || index >= _itemCount)
        {
            index = Random.Range(0, _itemCount);
        }
        Debug.Log("Index Result: " + index);
        float angle = ROUND_MIN * 360;
        float angleAddFrom = Mathf.Abs(_position[index].Item1);
        float angleAddTo = Mathf.Abs(_position[index].Item2);
        angle += Random.Range(angleAddFrom, angleAddTo);
        // move to zero
        var crrAngle = _content.transform.rotation.eulerAngles.z;
        if (crrAngle < 0)
            angle += 360 - crrAngle;
        else
            angle -= crrAngle;
        Debug.Log("Angle to from: " + angleAddFrom);
        Debug.Log("Angle to to: " + angleAddTo);
        Debug.Log("Crr Angle: " + crrAngle);
        Debug.Log("Angle to Spin: " + angle);
        StartCoroutine(Spinning(angle));
    }

    private IEnumerator Spinning(float angle)
    {
        float secondSpining = SECOND_SPINNING;
        float speed = angle / secondSpining * Time.deltaTime;
        var rotation = _content.transform.rotation;
        float crrAngle = rotation.eulerAngles.z;
        Debug.Log("Speed: " + speed);
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        while (crrAngle <= angle)
        {
            rotation.eulerAngles = crrAngle * Vector3.forward;
            _content.transform.rotation = rotation;
            crrAngle += speed;
            yield return wait;
        }
        Debug.Log("End");
    }
}
