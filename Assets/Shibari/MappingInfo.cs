using System;
using System.Reflection;
using UnityEngine;

namespace Shibari
{
    public class MappingInfo
    {
        public MethodInfo method;
        public BindableMapper owner;

        public Type returnType;
        public ParameterInfo[] signature;

        public object Invoke(params object[] o)
        {
            try
            {
                return method.Invoke(owner, o);
            }
            catch(Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }
    }
}