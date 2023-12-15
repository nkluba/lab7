using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private readonly char[] correctAnswers = { 'B', 'D', 'A', 'A', 'C', 'A', 'B', 'A', 'C', 'D', 'B', 'C', 'A', 'A', 'D', 'D', 'C', 'C', 'B', 'A' };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] studentAnswers = File.ReadAllText(openFileDialog.FileName)
                        .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(line => line.Trim().ToUpper())
                        .ToArray();

                    if (studentAnswers.Length == correctAnswers.Length)
                    {
                        int correctCount = 0;
                        int incorrectCount = 0;
                        string incorrectQuestions = "";

                        for (int i = 0; i < correctAnswers.Length; i++)
                        {
                            if (studentAnswers[i].Length > 0 && studentAnswers[i][0] == correctAnswers[i])
                            {
                                correctCount++;
                            }
                            else
                            {
                                incorrectCount++;
                                incorrectQuestions += (i + 1) + " ";
                            }
                        }

                        MessageBox.Show($"Results:\n\nCorrect Answers: {correctCount}\nIncorrect Answers: {incorrectCount}\nIncorrect Question Numbers: {incorrectQuestions}\n\n{(correctCount >= 15 ? "Pass" : "Fail")}", "Exam Grading Results");
                    }
                    else
                    {
                        MessageBox.Show("Invalid number of answers in the file. Please make sure there are 20 answers.", "Error");
                    }
                }
            }
        }
    }
}
