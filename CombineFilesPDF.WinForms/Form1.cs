using CombineFilesPDF.WinForms.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace CombineFilesPDF.WinForms
{
    public partial class Form1 : Form
    {
        string _folderName;
        ICollection<string> _files;
        MenuItem folderMenuItem;        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            FolderBrowserDialogExampleForm();

            InitializeOpenFileDialog();
        }

        private void FolderBrowserDialogExampleForm()
        {
            this.folderMenuItem = new System.Windows.Forms.MenuItem();            

            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();

            this.folderMenuItem.Text = "Select Directory...";
            this.folderMenuItem.Click += new System.EventHandler(this.button3_Click);            
        }        

        private void InitializeOpenFileDialog()
        {
            // Set the file dialog to filter for graphics files.
            this.openFileDialog1.Filter = "Files (*.PDF;*pdf)|*.PDF;*pdf";
                      

            // Allow the user to select multiple images.
            this.openFileDialog1.Multiselect = true;

            this.openFileDialog1.Title = "Files";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // Read the files
                foreach (String file in openFileDialog1.FileNames)
                {
                    if (_files == null)
                    {
                        _files = new List<string>();
                    }

                    _files.Add(file);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = textBox1.Text;

                if (String.IsNullOrWhiteSpace(fileName))
                {
                    MessageBox.Show("Informe o nome para o arquivo!");

                    return;
                }

                if(String.IsNullOrWhiteSpace(_folderName))
                {
                    MessageBox.Show("Informe a pasta de destino!");

                    return;
                }

                if (_files == null || !_files.Any())
                {
                    MessageBox.Show("Selecione o(s) arquivo(s)");

                    return;
                }

                new CombineFilesExportToPDF().Combine(String.Concat(_folderName, @"\", textBox1.Text), _files);

                MessageBox.Show(String.Format("O {0} arquivo foi salvo com sucesso!", fileName));

                textBox1.Text = String.Empty;

                _files.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Ops... Algo inesperado aconteceu!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                _folderName = folderBrowserDialog1.SelectedPath;

                textBox2.Text = _folderName;
            }
        }        
    }
}
