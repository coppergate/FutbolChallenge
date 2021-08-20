using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Helpers {
	static public class TypeHelpers {
		public static MethodInfo FindMethod<T>(string genericMethodName, int parameterCount = 0) {
			MethodInfo[] methods = typeof(T).GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.IgnoreReturn);
			if (methods.Any()) {
				return methods.Where(n => string.Compare(n.Name, genericMethodName) == 0 && n.GetParameters().Count() == parameterCount).First();
			}
			return null;
		}

		public static object InvokeByName<T>(object target, string methodName, object[] parameters) {
			MethodInfo method = FindMethod<T>(methodName, parameters?.Length ?? 0);
			return InvokeFromType(target, method, parameters);
		}

		public static object InvokeGenericFromType(object target, MethodInfo genericDefinition, string assemblyName, string typeName, object[] parameters) {
			return InvokeGenericFromType(target, genericDefinition, Assembly.CreateQualifiedName(assemblyName, typeName), parameters);
		}

		public static object InvokeGenericFromType(object target, MethodInfo genericDefinition, string assemblyQualifiedTypeName, object[] parameters) {
			Type lookupType = Type.GetType(assemblyQualifiedTypeName);
			MethodInfo generic = genericDefinition.MakeGenericMethod(lookupType);
			return generic.Invoke(target, parameters);
		}

		public static object InvokeFromType(object target, MethodInfo method, object[] parameters) {
			return method.Invoke(target, parameters);
		}

		public static IEnumerable<PropertyInfo> AllInstancePublicProperties<T>() {
			PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			return props;
		}

		public static IEnumerable<PropertyInfo> AllInstancePublicProperties(Type t) {
			PropertyInfo[] props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			return props;
		}

		public static IEnumerable<PropertyInfo> FindPublicProperties(IEnumerable<string> excludedProperties, Type t) {
			PropertyInfo[] props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			return props.Where(c => !excludedProperties.Any(s => string.Compare(s, c.Name, true) == 0));
		}

		public static IEnumerable<PropertyInfo> FindProperties<T>(IEnumerable<string> excludedProperties, T objectOfType) {
			PropertyInfo[] props = objectOfType.GetType().GetProperties();
			return props.Where(c => !excludedProperties.Any(s => string.Compare(s, c.Name, true) == 0) && c.GetMethod.IsPublic);
		}

		public static IEnumerable<PropertyInfo> FindNonStaticProperties<T>(IEnumerable<string> excludedProperties, T objectType) {
			Type t = objectType.GetType();
			PropertyInfo[] props = objectType.GetType().GetProperties();
			return props.Where(c => !excludedProperties.Any(s => string.Compare(s, c.Name, true) == 0) && c.GetMethod.IsPublic && !c.GetMethod.IsStatic);
		}

		public static PropertyInfo FindPublicProperty<T>(string name, T objectType) {
			Type t = objectType.GetType();
			IEnumerable<PropertyInfo> props = FindPublicProperties(Enumerable.Empty<string>(), t);
			return props.FirstOrDefault(c => string.Compare(name, c.Name, true) == 0 && c.GetMethod.IsPublic);
		}

		public static PropertyInfo FindNestedProperty<T>(string name, T obj) {

			String[] strings = name.Split(new char[] { '.' }, 2);
			if (strings.Count() == 1) {
				return FindPublicProperty(name, obj);
			}
			PropertyInfo info = FindPublicProperty(strings[0], obj);
			if (info == null) {
				return null;
			}
			return FindNestedProperty(strings[1], info.GetValue(obj));
		}

		public static object GetPropertyValue(object source, string property) {
			PropertyInfo info = FindPublicProperty(property, source);
			return info.GetGetMethod().Invoke(source, null);
		}

		public static void SetPropertyValue<T>(object target, string property, T value) {
			PropertyInfo info = FindPublicProperty(property, target);
			info.GetSetMethod().Invoke(target, new object[] { value });
		}

		public static void SetPropertyValue<T>(object target, PropertyInfo info, T value) {
			MethodInfo methodInfo = info.GetSetMethod();
			if (methodInfo != null) {
				methodInfo.Invoke(target, new object[] { value });
			}
		}

		public static IEnumerable<PropertyInfo> FindPublicPropertiesWithAttribute<T>(object target) {
			PropertyInfo[] props = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			return props.Where(p => p.CustomAttributes.Any(c => c.AttributeType == typeof(T)));
		}

		public static IEnumerable<PropertyInfo> CopyObjectPropertyValues<T, V>(T destination, V source, IEnumerable<string> excludedProperties) {
			List<PropertyInfo> copiedProperties = new List<PropertyInfo>();
			foreach (PropertyInfo info in FindProperties(excludedProperties, destination)) {
				try {
					SetPropertyValue(destination, info.Name, GetPropertyValue(source, info.Name));
					copiedProperties.Add(info);
				}
				catch {

				}
			}
			return copiedProperties;
		}

		public static IEnumerable<Type> FindAllTypesOfBase(Type baseType, Assembly assembly) {
			return assembly.GetExportedTypes().Where(t => t.IsSubclassOf(baseType));
		}

		public static T ConvertTo<T>(object value, T def) {
			if (value == null) {
				return def;
			}

			try {
				T output;
				if (typeof(T).IsEnum) {
					output = (T)Enum.Parse(typeof(T), value.ToString());
					if (!Enum.IsDefined(typeof(T), output)) {
						output = def;
					}
				}
				else {
					output = (T)Convert.ChangeType(value, typeof(T));
				}
				return output;
			}
			catch {
				return def;
			}
		}
	}
}
