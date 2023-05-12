using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UIInput
{
    public class UIInputText : MonoBehaviour, UIInput
    {
        [SerializeField] TextMeshProUGUI _label;
        [SerializeField] TMP_InputField _InputField;
        public string GetINput()
        {
            return _InputField.text;
        }

        public void Init(string label, string oldInput = "")
        {
            _label.text = label;
            if (oldInput != string.Empty)
                _InputField.text = oldInput;
        }
    }
}
