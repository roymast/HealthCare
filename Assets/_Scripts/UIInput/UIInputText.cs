using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UIInput
{
    [System.Serializable]
    public class UIInputText : UIInput
    {
        [SerializeField] TextMeshProUGUI _label;
        [SerializeField] TMP_InputField _InputField;
        public override string GetINput()
        {
            return _InputField.text;
        }

        public void Init(string label, string oldInput = "")
        {
            _label.text = label;
            if (oldInput != string.Empty)
                _InputField.text = oldInput;
        }

        public override void SetInput(string input)
        {
            _InputField.text = input;
        }

        public override void SetLabel(string label)
        {
            _label.text = label;
        }
    }
}
