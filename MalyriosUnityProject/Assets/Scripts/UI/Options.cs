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
            if (optionPanel.activeSelf)
            {
                var i = Random.Range(0, SoundHolder.Instance.openButton.Length);
                SoundHolder.Instance.openButton[i].Play();
            }
            else
            {
                SoundHolder.Instance.closeButton.Play();
            }
        }
        
    }
}
