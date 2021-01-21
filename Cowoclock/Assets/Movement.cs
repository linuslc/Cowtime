using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int _currentUfoIndex = 0;
    public GameObject[] _ufoObject;

    private void Start()
    {
        foreach (GameObject g in _ufoObject)
        {
            g.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _ufoObject[_currentUfoIndex].SetActive(false);
            _currentUfoIndex++;

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _ufoObject[_currentUfoIndex].SetActive(false);
            _currentUfoIndex--;
        }
        if (_currentUfoIndex < 0) _currentUfoIndex = _ufoObject.Length - 1;

        _currentUfoIndex %= _ufoObject.Length;
        _ufoObject[_currentUfoIndex].SetActive(true);
    }

}
