using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UIInput
{
    public class UIInputCounter : MonoBehaviour, UIInput
    {
        [SerializeField] TMP_InputField CountText;
        [SerializeField] int _characterLimits = 4;
        int count;
        // Start is called before the first frame update
        void Start()
        {
            count = 0;
            CountText.text = count.ToString();
            CountText.contentType = TMP_InputField.ContentType.DecimalNumber;
            CountText.characterLimit = _characterLimits;
        }
        public void OnSet()
        {
            count = int.Parse(CountText.text);
        }

        public void OnInc()
        {
            if (count + 1 >= Mathf.Pow(10, _characterLimits))
                return;

            count++;
            CountText.text = count.ToString();
        }
        public void OnDec()
        {
            if (count <= 0)
                return;

            count--;
            CountText.text = count.ToString();
        }

        public string GetINput()
        {
            return count.ToString();
        }
    }
}
