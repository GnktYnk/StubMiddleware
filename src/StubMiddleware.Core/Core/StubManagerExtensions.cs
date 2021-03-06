﻿using System;
using System.Reflection;

namespace StubGenerator.Core
{
    public static class StubManagerExtensions
    {
        private static Type LoadType(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentException("message", nameof(typeName));
            }
            var result = Type.GetType(typeName);
            if (result == null)
                throw new TypeLoadException($"The type '{typeName}' couldn't be loaded");
            return result;
        }

        public static object InvokeCreateNew(this IStubManager stubManager, string typeName)
        {
            if (stubManager == null)
            {
                throw new ArgumentNullException(nameof(stubManager));
            }

            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentException("message", nameof(typeName));
            }

            var type = LoadType(typeName);


            MethodInfo method = typeof(IStubManager).GetMethod("CreateNew");
            MethodInfo genericMethod = method.MakeGenericMethod(type);
            return genericMethod.Invoke(stubManager, null);
        }

        public static object InvokeCreateListOfSize(this IStubManager stubManager, string typeName, int size)
        {
            if (stubManager == null)
            {
                throw new ArgumentNullException(nameof(stubManager));
            }

            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "List Size must be positive number!");
            }

            var type = LoadType(typeName);
            MethodInfo method = typeof(IStubManager).GetMethod("CreateListOfSize");
            MethodInfo genericMethod = method.MakeGenericMethod(type);
            return genericMethod.Invoke(stubManager, parameters: new object[] { size });
        }
    }
}
