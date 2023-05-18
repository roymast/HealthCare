using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIInput
{
    public abstract class UIInput : MonoBehaviour
    {
        public abstract string GetINput();
        public abstract void SetInput(string input);
        public abstract void SetLabel(string label);
    }
}
