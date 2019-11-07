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

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fileName = textBox3.Text;
            string files = textBox1.Text;
            var isChecked = checkBox1.Checked;
            var option = SearchOption.TopDirectoryOnly;
            if (isChecked)
            {
                option = SearchOption.AllDirectories;
            }
            string[] filenames = Directory.GetFiles(files, "*", option);
            var filterCount = 0.8;
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                var isDouble = double.TryParse(textBox2.Text, out filterCount);
                if (!isDouble || filterCount > 1)
                {
                    filterCount = 0.8;
                }
            }
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                filenames = filenames.Where(p => p.Contains(fileName)).ToArray();
            }
            FilterSimilar filter = new FilterSimilar();
            List<string> lists = new List<string>();
            for (int i = 0; i < filenames.Length; i++)
            {
                if (i == filenames.Length - 1)
                    break;
                var file1 = Path.GetFileNameWithoutExtension(filenames[i]);
                var file2 = Path.GetFileNameWithoutExtension((filenames[i + 1]));
                var fileName1 = Path.GetFileName(filenames[i]);
                var fileName2 = Path.GetFileName((filenames[i + 1]));
                var similarVal = filter.getSimilarityRatio(file1, file2);
                if (similarVal >= filterCount)
                {
                    if (!lists.Contains(fileName1))
                        lists.Add(fileName1);
                    if (!lists.Contains(fileName2))
                        lists.Add(fileName2);
                }
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = (from s in lists
                                        select new
                                        {
                                            fileName = s
                                        })
                                        .ToList();
        }
    }
}
