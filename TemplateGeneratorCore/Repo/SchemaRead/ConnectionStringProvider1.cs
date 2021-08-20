using Microsoft.VisualStudio.TextTemplating;

namespace TemplateCodeGenerator.SchemaRead {
	internal class ConnectionStringProvider {
		public ConnectionStringProvider(string connectionStringName, ITextTemplatingEngineHost host) {
			ConnectionStringName = connectionStringName;
			Host = host;
		}

		public string ConnectionStringName { get; }
		public ITextTemplatingEngineHost Host { get; }
		public string ConnectionString { get; internal set; }
		public string ProviderName { get; internal set; }
	}
}