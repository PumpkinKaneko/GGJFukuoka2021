using UnityEngine;
using UnityEngine.UI;

public class LogMenu : MonoBehaviour
{
    [SerializeField]
    private Text m_textUI = null;

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Debug").Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void DebugLog(string msg)
    {
        m_textUI.text += "・" + msg + "\n";
    }

}