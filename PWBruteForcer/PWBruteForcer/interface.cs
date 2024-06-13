using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PWBruteForcer
{
    public partial class Interface : Form
    {
        private string executablePath = AppDomain.CurrentDomain.BaseDirectory;
        private string batchFilePath = String.Empty;
        private string passwordsListPath = String.Empty;
        public Interface()
        {
            InitializeComponent();
        }

        private void attemptLoginBtn_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Please enter a username.", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxUsername.Focus();
                return;
            }

            // Regex for IPv4: Matches exactly four octets (0-255), separated by dots. Anchored to start (^) and end ($) of string.
            string ipPattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBoxIP.Text.Trim(), ipPattern))
            {
                MessageBox.Show("Please enter a valid IP address.", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxIP.Focus();
                return;
            }
            string command = $@"cmd.exe /c ""{batchFilePath} {textBoxIP.Text.Trim()} {passwordsListPath} {textBoxUsername.Text.Trim()}""";
            System.Diagnostics.Process.Start("cmd.exe", command);
        }

        private void Interface_Load(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = Directory.GetParent(executablePath); // Up one level to Debug
            for (int i = 0; i < 4; i++) // Repeat three more times to go up five levels in total
            {
                directoryInfo = directoryInfo?.Parent;
            }
            // Ensure the directoryInfo is not null before accessing its FullName
            if (directoryInfo != null)
            {
                string fileParentPath = directoryInfo.FullName;
                batchFilePath = Path.Combine(fileParentPath, "brute-forcer.bat");
                passwordsListPath = Path.Combine(fileParentPath, "list.txt");
            }
            else
            {
                MessageBox.Show("Error: Could not find the batch file path. Please ensure the batch file is in the correct location.","Error.",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
