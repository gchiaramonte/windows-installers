﻿using Elastic.Installer.Domain.Elasticsearch.Model.Tasks;
using Elastic.Installer.Domain.Session;
using Elastic.Installer.Msi.CustomActions;
using Microsoft.Deployment.WindowsInstaller;
using WixSharp;

namespace Elastic.Installer.Msi.Elasticsearch.CustomActions.Install
{
	public class InstallPluginsAction : CustomAction<ElasticsearchProduct>
	{
		public override string Name => nameof(InstallPluginsAction);
		public override int Order => (int)ElasticsearchCustomActionOrder.InstallPlugins;
		public override Condition Condition => Condition.NOT_Installed;
		public override Return Return => Return.check;
		public override Sequence Sequence => Sequence.InstallExecuteSequence;
		public override When When => When.After;
		public override Step Step => new Step(nameof(InstallJvmOptionsAction));
		public override Execute Execute => Execute.deferred;

		[CustomAction]
		public static ActionResult InstallPlugins(Session session) =>
			session.Handle(() => new InstallPluginsTask(session.ToSetupArguments(), session.ToISession()).Execute());
	}
}
