using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour, IInputClickHandler {

    #region IInputClickHandler
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Button button = this.gameObject.GetComponent<Button>();
        button.onClick.Invoke();
    }
    #endregion IInputClickHandler
}
