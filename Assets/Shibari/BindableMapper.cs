using System.Collections.Generic;
using System.Linq;

namespace Shibari
{
    public abstract class BindableMapper
    {
        public Dictionary<string, MappingInfo> ReflectedMappings { get; protected set; }

        public void InitializeMappings()
        {
            ReflectedMappings = new Dictionary<string, MappingInfo>();

            var methods = GetType().GetMethods().Where(m =>
                m.IsPublic
                && m.ReturnType != typeof(void));

            foreach (var m in methods)
            {
                ReflectedMappings.Add(m.Name, new MappingInfo()
                {
                    signature = m.GetParameters(),
                    method = m,
                    owner = this,
                    returnType = m.ReturnType
                });
            }
        }
    }
}