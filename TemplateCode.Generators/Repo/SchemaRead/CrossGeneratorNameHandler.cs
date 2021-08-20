namespace TemplateCodeGenerator.SchemaRead {
    public interface ICrossGeneratorNameHandler {
		string ToDataModelName(string name);
		string ToDomainModelName(string name);
		string ToRepositoryName(string name);
		string ToControllerName(string name);


		string BaseNamespace { set; }
		string DataModelNamespace { get; }
		string DomainModelNamespace { get; }
		string RepositoryNamespace { get; }
		string ControllerNamespace { get; }

		string DataModelNamespaceSuffix { set; }
		string DomainModelNamespaceSuffix { set; }
		string RepositoryNamespaceSuffix { set; }
		string ControllerNamespaceSuffix { set; }

		string DataModelNameFormat { set; }
		string DomainModelNameFormat { set; }
		string RepositoryNameFormat { set; }
		string ControllerNameFormat { set; }

	}


	public class CrossGeneratorNameHandler : ICrossGeneratorNameHandler {
		public string BaseNamespace { set; private get; }

		public string DataModelNamespaceSuffix { set; private get; } = "Dto";
		public string DomainModelNamespaceSuffix { set; private get; } = "Model";
		public string RepositoryNamespaceSuffix { set; private get; } = "Repository";
		public string ControllerNamespaceSuffix { set; private get; } = "Controller";

		public string DataModelNameFormat { set; private get; } = "{0}Dto";
		public string DomainModelNameFormat { set; private get; } = "{0}Model";
		public string RepositoryNameFormat { set; private get; } = "{0}Repository";
		public string ControllerNameFormat { set; private get; } = "{0}Controller";

		public string DataModelNamespace => $"{BaseNamespace}.{DataModelNamespaceSuffix}";
		public string DomainModelNamespace => $"{BaseNamespace}.{DomainModelNamespaceSuffix}";
		public string RepositoryNamespace => $"{BaseNamespace}.{RepositoryNamespaceSuffix}";
		public string ControllerNamespace => $"{BaseNamespace}.{ControllerNameFormat}";

		public string ToControllerName(string name) {
			return string.Format(ControllerNameFormat, name);
		}

		public string ToDataModelName(string name) {
			return string.Format(DataModelNameFormat, name);
		}

		public string ToDomainModelName(string name) {
			return string.Format(DomainModelNameFormat, name);
		}

		public string ToRepositoryName(string name) {
			return string.Format(RepositoryNameFormat, name);
		}
	}
}
