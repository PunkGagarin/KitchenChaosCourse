using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindPanelUI : MonoBehaviour
{

    [field: SerializeField]
    public KeybindType KeybindType { get; private set; }

    [field: SerializeField]
    public TextMeshProUGUI Text { get; private set; }

    [field: SerializeField]
    public Button Button { get; private set; }

    private void Awake()
    {
        Assert.IsNotNull(KeybindType);
        Assert.IsNotNull(Text);
        Assert.IsNotNull(Button);
    }
}