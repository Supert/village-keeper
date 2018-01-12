using System;
using System.Reflection;
using UnityEngine;

namespace Shibari
{
    public class MappingInfo
    {
        public MethodInfo Method { get; private set; }
        public BindableMapper Owner { get; private set; }
        public ParameterInfo[] Parameters { get; private set; }

        public MappingInfo(MethodInfo method, BindableMapper owner)
        {
            Method = method;
            Owner = owner;
            Parameters = method.GetParameters();
        }

        public object Invoke(params object[] parameters)
        {
            try
            {
                return Method.Invoke(Owner, parameters);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                if (Method.ReturnType.IsValueType)
                    return Activator.CreateInstance(Method.ReturnType);
                return null;
            }
        }
    }
}