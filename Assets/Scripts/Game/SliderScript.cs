using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField inputField;
    public Slider slider;

    public void OnValueChanged(){
        //Debug.Log("OnValueChanged");
      
        // Component[] components = inputField.GetComponents(typeof(Component));
        // foreach(Component component in components) {
        //     Debug.Log(component.ToString());
        // }

        inputField.text = (slider.value).ToString();
        var text = inputField.GetComponent<TMP_InputField>().text;
        //Debug.Log(text);

    }

    public void OnEndEdit(){
        //Debug.Log("OnEndEdit");
        slider.value = float.Parse(inputField.GetComponent<TMP_InputField>().text);
    }
    public void OnButtonClick(){
        Debug.Log("OnButtonClick");
        //render number object

    }

}
