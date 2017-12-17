using System.Collections.Generic;

namespace Shibari
{
    public abstract class UiHandler
    {
        public Dictionary<string, BindableHandlerInfo> ReflectedHandlers { get; set; }
    }
}