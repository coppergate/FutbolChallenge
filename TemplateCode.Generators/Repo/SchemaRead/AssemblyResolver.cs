using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TemplateCodeGenerator.SchemaRead {
    public static class AssemblyResolver {

		public static void Enable() {
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
			// Ignore missing resources
			if (args.Name.Contains(".resources"))
				return null;

			// check for assemblies already loaded
			Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
			if (assembly != null)
				return assembly;

			// Try to load by filename - split out the filename of the full assembly name
			// and append the base path of the original assembly (ie. look in the same dir)
			var thisAssmPath = new FileInfo(typeof(AssemblyResolver).Assembly.Location);
			var filename = args.Name.Split(',')[0] + ".dll".ToLower();

			var asmFile = Path.Combine(thisAssmPath.Directory.FullName, filename);

			try {
				return System.Reflection.Assembly.LoadFrom(asmFile);
			}
			catch (Exception ) {
				return null;
			}
		}
	}
}
