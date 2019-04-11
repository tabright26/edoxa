﻿// Filename: ObjectExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

namespace eDoxa.Testing.MSTest.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetPrivateField(this object obj, string name)
        {
            var fieldInfo = obj.PrivateFieldInfo(name);

            return fieldInfo.GetValue(obj);
        }

        public static void SetPrivateField(this object obj, string name, object value)
        {
            var fieldInfo = obj.PrivateFieldInfo(name);

            try
            {
                fieldInfo.SetValue(obj, value);
            }
            catch (TargetInvocationException exception)
            {
                throw exception.InnerException;
            }
        }

        public static void SetProperty(this object obj, string name, object value)
        {
            var propertyInfo = obj.PropertyInfo(name);

            try
            {
                propertyInfo.SetValue(obj, value);
            }
            catch (TargetInvocationException exception)
            {
                throw exception.InnerException;
            }
        }

        private static PropertyInfo PropertyInfo(this object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "The assignment target cannot be null.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The property name cannot be null or empty.", nameof(name));
            }

            PropertyInfo propertyInfo = null;
            var type = obj.GetType();

            while (type != null)
            {
                propertyInfo = type.GetProperty(name);

                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    break;
                }

                type = type.BaseType;
            }

            if (propertyInfo == null)
            {
                throw new TargetException($"Property '{name}' not found in type hierarchy.");
            }

            return propertyInfo;
        }

        private static FieldInfo PrivateFieldInfo(this object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "The assignment target cannot be null.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The field name cannot be null or empty.", nameof(name));
            }

            FieldInfo fieldInfo = null;
            var type = obj.GetType();

            while (type != null)
            {
                fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);

                if (fieldInfo != null)
                {
                    break;
                }

                type = type.BaseType;
            }

            if (fieldInfo == null)
            {
                throw new TargetException($"Field '{name}' not found in type hierarchy.");
            }

            return fieldInfo;
        }
    }
}