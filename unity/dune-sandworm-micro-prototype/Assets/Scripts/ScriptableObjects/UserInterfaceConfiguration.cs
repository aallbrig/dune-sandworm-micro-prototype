using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "SOD/new UI Configuration", order = 1)]
    public class UserInterfaceConfiguration : ScriptableObject
    {
        public Color primaryColor = Color.black;
        public Color hintColor = Color.black;
        public float tipDismissTime = 1.0f;
    }
}