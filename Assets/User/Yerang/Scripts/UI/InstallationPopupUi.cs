using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstallationPopupUi : MonoBehaviour
{
    public Installation installation;

    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI contentText;

    [SerializeField]
    private Button spawnButton;
    private int spawnCount;

    private void OnEnable()
    {
        SetText();
    }

    public void SetText()
    {
        if (installation == null)
        {
            this.nameText.text = "Name: ";
            this.contentText.text = "Workers: ";
            return;
        }

        this.nameText.text = $"name: {installation.name}";

        StringBuilder sb = new StringBuilder();
        sb.Append("Workers: ");
        foreach (WorkerUnit workerUnit in installation.Workers)
        {
            sb.Append($"{workerUnit.name}, ");
        }
        sb.Remove(sb.Length - 1, 1);

        this.contentText.text = sb.ToString();
    }

    public void SetButton()
    {
        
    }
}
