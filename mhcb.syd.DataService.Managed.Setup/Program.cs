﻿using System;
using System.Windows.Forms;
using WixSharp;
using WixSharp.Forms;

//#error "DON'T FORGET to install NuGet package 'WixSharp' and remove this `#error` statement."
// NuGet console: Install-Package WixSharp
// NuGet Manager UI: browse tab

namespace mhcb.syd.DataService.Managed.Setup
{
	class Program
	{
		static void Main()
		{
			//var test = new WixSharp.WixProject;



			var project = new ManagedProject("mhcb.syd.DataService.Managed.Setup",
							 new Dir(@"%ProgramFiles%\My Company\My Product",
								 new File("Program.cs")));

			project.GUID = new Guid("6fe30b47-2577-43ad-9095-1861ba25889b");

			project.ManagedUI = ManagedUI.Empty;    //no standard UI dialogs
			project.ManagedUI = ManagedUI.Default;  //all standard UI dialogs

			//custom set of standard UI dialogs
			project.ManagedUI = new ManagedUI();

			project.ManagedUI.InstallDialogs.Add(Dialogs.Welcome)
											.Add(Dialogs.Licence)
											.Add(Dialogs.SetupType)
											.Add(Dialogs.Features)
											.Add(Dialogs.InstallDir)
											.Add(Dialogs.Progress)
											.Add(Dialogs.Exit);

			project.ManagedUI.ModifyDialogs.Add(Dialogs.MaintenanceType)
										   .Add(Dialogs.Features)
										   .Add(Dialogs.Progress)
										   .Add(Dialogs.Exit);

			project.Load += Msi_Load;
			project.BeforeInstall += Msi_BeforeInstall;
			project.AfterInstall += Msi_AfterInstall;

			//project.SourceBaseDir = "<input dir path>";
			//project.OutDir = "<output dir path>";

			project.BuildMsi();
		}

		static void Msi_Load(SetupEventArgs e)
		{
			if (!e.IsUISupressed && !e.IsUninstalling)
				MessageBox.Show(e.ToString(), "Load");
		}

		static void Msi_BeforeInstall(SetupEventArgs e)
		{
			if (!e.IsUISupressed && !e.IsUninstalling)
				MessageBox.Show(e.ToString(), "BeforeInstall");
		}

		static void Msi_AfterInstall(SetupEventArgs e)
		{
			if (!e.IsUISupressed && !e.IsUninstalling)
				MessageBox.Show(e.ToString(), "AfterExecute");
		}
	}
}