﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace finalProject
{
    public partial class Temperature : Form
    {
        public Temperature()
        {
            InitializeComponent();
        }

        private void cTof_CheckedChanged(object sender, EventArgs e)
        {
            if (cTof.Checked)
            {
                labelFrom.Text = "C";
                labelTo.Text = "F";
            }
            else
            {
                labelFrom.Text = "F";
                labelTo.Text = "C";
            }
            fromTextbox.Text = "";
            toTextbox.Text = "";
            message.Text = "";
        }

        string path = @".\files\TempConv.txt";
        private void convert_Click(object sender, EventArgs e)
        {
            string valueStr = fromTextbox.Text.Trim();
            double result = 0;
            double inTemperature;
            string outMessage = "";
            string dir = @".\files\";

            if (Regex.IsMatch(valueStr, @"^-?\d{1,3}(\.\d{1,2})?(°[CF])?$"))
            {
                try
                {
                    inTemperature = Convert.ToDouble(fromTextbox.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid temperature.");
                    return;
                }

                if (cTof.Checked)
                {
                    result = inTemperature * 9 / 5 + 32;
                    switch (inTemperature)
                    {
                        case 100:
                            message.ForeColor = Color.Red;
                            outMessage = "Water boils";
                            break;
                        case 40:
                            message.ForeColor = Color.Red;
                            outMessage = "Hot Bath";
                            break;
                        case 37:
                            message.ForeColor = Color.Green;
                            outMessage = "Body temperature";
                            break;
                        case 30:
                            message.ForeColor = Color.Green;
                            outMessage = "Beach weather";
                            break;
                        case 21:
                            message.ForeColor = Color.Green;
                            outMessage = "Room remperature";
                            break;
                        case 10:
                            message.ForeColor = Color.Blue;
                            outMessage = "Cool Day";
                            break;
                        case 0:
                            message.ForeColor = Color.Blue;
                            outMessage = "Freezing point of water";
                            break;
                        case -18:
                            message.ForeColor = Color.Blue;
                            outMessage = "Very Cold Day";
                            break;
                        case -40:
                            message.ForeColor = Color.Blue;
                            outMessage = "Extremely Cold Day \n(and the same number!)";
                            break;
                        default:
                            outMessage = "";
                            break;
                    }

                    FileStream fileStream = null;

                    try
                    {
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
                        StreamWriter writer = new StreamWriter(fileStream);
                        string dateTimeString = DateTime.Now.ToString("yyyy/MM/dd h:mm:ss tt");
                        writer.WriteLine($"{inTemperature} Celsius = {Math.Round(result, 1)} Fahrenheit, {dateTimeString} {Regex.Replace(outMessage, @"\s+", " ")}");
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
                else
                {
                    result = (inTemperature - 32) * 5 / 9;
                    switch (inTemperature)
                    {
                        case 212:
                            message.ForeColor = Color.Red;
                            outMessage = "Water boils";
                            break;
                        case 104:
                            message.ForeColor = Color.Red;
                            outMessage = "Hot Bath";
                            break;
                        case 98.6:
                            message.ForeColor = Color.Green;
                            outMessage = "Body temperature";
                            break;
                        case 86:
                            message.ForeColor = Color.Green;
                            outMessage = "Beach weather";
                            break;
                        case 70:
                            message.ForeColor = Color.Green;
                            outMessage = "Room remperature";
                            break;
                        case 50:
                            message.ForeColor = Color.Blue;
                            outMessage = "Cool Day";
                            break;
                        case 32:
                            message.ForeColor = Color.Blue;
                            outMessage = "Freezing point of water";
                            break;
                        case 0:
                            message.ForeColor = Color.Blue;
                            outMessage = "Very Cold Day";
                            break;
                        case -40:
                            message.ForeColor = Color.Blue;
                            outMessage = "Extremely Cold Day \n(and the same number!)";
                            break;
                        default:
                            outMessage = "";
                            break;
                    }

                    FileStream fileStream = null;
                    try
                    {
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
                        StreamWriter writer = new StreamWriter(fileStream);
                        string dateTimeString = DateTime.Now.ToString("yyyy/MM/dd h:mm:ss tt");
                        writer.WriteLine($"{inTemperature} Fahrenheit = {Math.Round(result, 1)} Celsius, {dateTimeString} {Regex.Replace(outMessage, @"\s+", " ")}");
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

                toTextbox.Text = Math.Round(result,2).ToString();
                message.Text = outMessage;
                fromTextbox.Focus();
            }
            else
            {
                MessageBox.Show("Invalid value entered. Please enter a valid temperature using dot as decimal separator.");
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit the temperature converter?", "Exit ?", MessageBoxButtons.YesNo).ToString() == "Yes")
            {
                this.Close();
            }

        }

        private void read_Click(object sender, EventArgs e)
        {
            string message = "";
            FileStream fileStream = null;
            StreamReader reader = null;
            string textToPrint = "";
            int counter = 0;
            string title = "Temperature Converter by Kathleen Forgiarini";
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
