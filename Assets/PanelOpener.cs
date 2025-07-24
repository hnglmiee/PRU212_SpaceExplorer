using UnityEngine;
using UnityEngine.EventSystems;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject playButton;

    private void Update()
    {
        if (Panel != null && Panel.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Panel.SetActive(false);
            }
        }
    }

    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }

    public void HidePanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(false);
        }
    }
}
