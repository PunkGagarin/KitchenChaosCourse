using TMPro;
using UnityEngine;

public class TutorialKeyUI : MonoBehaviour
{

    [field: SerializeField]
    public KeybindType Type { get; private set; }

    [field: SerializeField]
    public TextMeshProUGUI Text { get; private set; }
}
