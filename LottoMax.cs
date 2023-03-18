﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalProject
{
    public partial class LottoMax : Form
    {
        public LottoMax()
        {
            InitializeComponent();
        }

        private void maxExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit?", "Exit Lotto Max", MessageBoxButtons.YesNo).ToString() == "Yes")
            {
                this.Close();
            }
        }

        private void maxGenerate_Click(object sender, EventArgs e)
        {
            Random labelRandom = new Random();
            int labelRandomNumber = 0;
            List<int> labelUniqueNumbers = new List<int>();
            string labelNumbers = "";
            while (labelUniqueNumbers.Count < 7)
            {
                labelRandomNumber = labelRandom.Next(0, 10);
                if (!labelUniqueNumbers.Contains(labelRandomNumber))
                {
                    labelUniqueNumbers.Add(labelRandomNumber);
                    labelNumbers += labelRandomNumber;
                }
            }
            maxLabel.Text = labelNumbers;


            Random boxRandom = new Random();
            int boxRandomNumber = 0;
            List<int> boxUniqueNumbers = new List<int>();
            string boxNumbers = "";
            while (boxUniqueNumbers.Count < 8)
            {
                boxRandomNumber = boxRandom.Next(1, 50);
                if (!boxUniqueNumbers.Contains(boxRandomNumber))
                {
                    boxUniqueNumbers.Add(boxRandomNumber);
                    boxNumbers += boxRandomNumber.ToString() + "\t\t";
                }
            }
            maxTextbox.Text = boxNumbers;
        }
    }
}
