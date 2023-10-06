//using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    Image _background;
    [SerializeField] List<TextMeshProUGUI> _text;
    float _alpha = 1;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        foreach(TextMeshProUGUI t in transform.GetComponentsInChildren<TextMeshProUGUI>())
            _text.Add(t);
        _background = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_alpha > 0 && Time.timeSinceLevelLoad > 2)
        {
            _alpha -= Time.deltaTime;
            foreach (TextMeshProUGUI t in _text)
                t.color = new Color(t.color.r, t.color.g, t.color.b, _alpha);
            _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, _alpha); 
        }
    }

    public void EndScene()
    {
        foreach (TextMeshProUGUI t in _text)
            t.color = new Color(t.color.r, t.color.g, t.color.b, 1);
        _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, 1);
    }
}
