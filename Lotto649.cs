﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalProject
{
    public partial class Lotto649 : Form
    {
        public Lotto649()
        {
            InitializeComponent();
        }

        private void l649Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit?", "Exit Lotto649", MessageBoxButtons.YesNo).ToString() == "Yes")
            {
                this.Close();
            }
        }

        string path = @".\files\LottoNbrs.txt";
        private void l649Generate_Click(object sender, EventArgs e)
        {
            Random labelRandom = new Random();
            int labelRandomNumber = 0;
            List<int> labelUniqueNumbers = new List<int>();
            string labelNumbers = "";
            while (labelUniqueNumbers.Count < 7)
            {
                labelRandomNumber = labelRandom.Next(0, 10);
                labelUniqueNumbers.Add(labelRandomNumber);
                labelNumbers += labelRandomNumber;
                
            }
            l649Label.Text = labelNumbers;


            Random boxRandom = new Random();
            int boxRandomNumber = 0;
            List<int> boxUniqueNumbers = new List<int>();
            string boxNumbers = "";
            while (boxUniqueNumbers.Count < 7)
            {
                boxRandomNumber = boxRandom.Next(1, 49);
                if (!boxUniqueNumbers.Contains(boxRandomNumber))
                {
                    boxUniqueNumbers.Add(boxRandomNumber);
                    boxNumbers += boxRandomNumber.ToString() + "\t\t";
                }
            }
            l649Textbox.Text = boxNumbers;


            //Text File
            string dir = @".\files\";
            FileStream fileStream = null;
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fileStream);
                string lotteryName = "649";
                int bonusNumber = boxUniqueNumbers[6];
                string dateTimeString = DateTime.Now.ToString("yyyy/MM/dd h:mm:ss tt");
                writer.Write(lotteryName + ", " + dateTimeString + ", ");
                for (int i = 0; i < boxUniqueNumbers.Count - 1; i++)
                {
                    writer.Write(boxUniqueNumbers[i]);
                    if (i != boxUniqueNumbers.Count - 1)
                    {
                        writer.Write(",");
                    }
                }
                writer.WriteLine(" Bonus " + bonusNumber);

                writer.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured, try again. \n" + ex.Message);
            }
            finally
            {
                if (fileStream != null) fileStream.Close();
            }
        }

        private void l649Read_Click(object sender, EventArgs e)
        {
            FileStream fileStream = null;
            StreamReader reader = null;
            string textToPrint = "";
            int counter = 0;
            string title = "Lottery Numbers by Kathleen Forgiarini";
            try
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fileStream);

                while (reader.Peek() != -1)
                {
                    textToPrint += reader.ReadLine() + "\n";
                    counter++;

                    if (counter == 10)
                    {
                        MessageBox.Show(textToPrint, title);
                        textToPrint = "";
                        counter = 0;
                    }
                }

                if (counter != 0)
                {
                    MessageBox.Show(textToPrint, title);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured, try again. \n" + ex.Message);
            }
            finally
            {
                if (fileStream != null) fileStream.Close();
            }
        }
    }
}
