using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSV_Reader
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            txtCout.Clear();
            String[] arrOut;
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "CSV Files|*.csv";
            open.FilterIndex = 1;
            if (DialogResult.OK == open.ShowDialog())
            {
                try
                {
                    using (TextFieldParser tfp = new TextFieldParser(open.FileName))
                    {
                        tfp.TextFieldType = FieldType.Delimited;
                        tfp.SetDelimiters(";");

                        while (!tfp.EndOfData)
                        {
                            arrOut = tfp.ReadFields();

                            int countColumn = 0;
                            foreach (String data in arrOut)
                            {
                                txtCout.Text += data;
                                if (countColumn < arrOut.Length - 1)
                                {
                                    countColumn++;
                                    txtCout.Text += ";";
                                }
                                else
                                    txtCout.Text += "\r\n";
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV Files|*.csv";
            sfd.FilterIndex = 1;
            if(DialogResult.OK == sfd.ShowDialog() && txtCout.Text != "")
            {
                try
                {
                    File.WriteAllText(sfd.FileName, txtCout.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
