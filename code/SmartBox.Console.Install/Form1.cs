using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartBox.Console.Install
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            SmartBox.Console.Install.BO bo = new BO(this.tbDataSource, this.tbUID, this.tbPassword, this.tbSmartBox, this.tbSmartBoxApp, this.tbSmartBoxAppOut);
            
            bo.SetInitialData();
            bo.SetInitialSmartBoxAppData();
           
        }
    }
}
