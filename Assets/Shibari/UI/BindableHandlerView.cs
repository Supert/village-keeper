using UnityEngine;

namespace Shibari.UI
{
    public abstract class BindableHandlerView : MonoBehaviour
    {
        [SerializeField]
        private string additionalData = "";

        [SerializeField]
        private PathToHandler pathToHandler = new PathToHandler();

        protected void Invoke()
        {
            Model.RootNode.InvokeHandlerByPath(pathToHandler.value.Split('/'), this, additionalData);
        }
    }
}