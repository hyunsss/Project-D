using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    UI_Boss_Text uiBossText;
    private void Awake()
    {
        uiBossText = GetComponent<UI_Boss_Text>();

    }
}
