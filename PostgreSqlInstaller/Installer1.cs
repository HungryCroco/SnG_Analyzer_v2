using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSqlInstaller
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }
        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
            // Your custom installation logic here

            Form form = new Form1();
            form.StartPosition = FormStartPosition.CenterScreen; // Set the starting position of the form
            form.TopMost = true; // Make the form topmost
            form.FormClosed += (sender, e) => { this.Context.Parameters["Message"] = "Custom action installed successfully!"; }; // Store a message to show after the form is closed
            System.Threading.Thread t = new System.Threading.Thread(() => Application.Run(form));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            t.Join(); // Wait for the form to be closed before continuing with the installation process

        }

    }
}
