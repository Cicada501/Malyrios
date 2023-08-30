using UnityEngine;

namespace UI
{
    public class Options : MonoBehaviour
    {

        [SerializeField] private GameObject optionPanel;

        private void Start()
        {
            optionPanel.SetActive(false);
        }

        public void ToggleOptionPanel()
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
        }
        
    }
}
