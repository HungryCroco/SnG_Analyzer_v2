using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;

[RunInstaller(true)]
public partial class PostgreSqlInstaller : Installer
{
    public PostgreSqlInstaller()
    {
        InitializeComponent();
    }

    public override void Install(IDictionary savedState)
    {
        base.Install(savedState);
        // Your custom installation logic here
        MessageBox.Show("Custom action installed successfully!");
    }

    // Add other override methods as needed, e.g., Uninstall, Commit, Rollback.
}
