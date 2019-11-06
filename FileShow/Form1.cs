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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FileShow
{
    public partial class FileShow : Form
    {
        public FileShow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog filename = new OpenFileDialog();
            //filename.InitialDirectory = Application.StartupPath;
            //filename.Filter = "Folders|\n";
            //filename.AddExtension = false;
            //filename.CheckFileExists = false;
            //filename.DereferenceLinks = true;
            //filename.Multiselect = false;
            //filename.RestoreDirectory = true;
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                textBox1.Text = dialog.FileName.ToString();//获得完整路径在textBox1中显示
                //StreamReader sr = new StreamReader(filename.FileName, Encoding.Default);//将/选/中的文件在textBox2中显示
                //textBox2.Text = sr.ReadToEnd();
                //sr.Close();
                //string[] filenames = Directory.GetFiles(dialog.FileName.ToString(), "*", SearchOption.AllDirectories);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fileName = textBox3.Text;
            string files = textBox1.Text;
            string[] filenames = Directory.GetFiles(files, "*", SearchOption.AllDirectories);
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                filenames = filenames.Where(p => p.Contains(fileName)).ToArray();
            }
            //string filenames_Enter = string.Join("\r\n", filenames);
            //textBox2.Text = filenames_Enter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = (from s in filenames
                                        select new
                                        {
                                            fileName = Path.GetFileName(s)
                                        })
                                        .ToList();
        }
    }
}
