using System;
using System.Windows.Forms;
using WindowsFormsApp1.ServiceReference1;
using System.IO;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string filePath = string.Empty;
        int count;
        bool plotExists;
        Service1Client client;
        OutputData results;

        public Form1()
        {
            InitializeComponent();
            count = 0;
            plotExists = false;
            circularProgressBar.Visible = false;

            chartResults.Series[0].Enabled = false;
            chartResults.Series[1].Enabled = false;
            chartResults.Series[2].Enabled = false;
            chartResults.Series[3].Enabled = false;

            turnsInfo.Visible = false;

            chartResults.Series[0].Name = "Gyroscope X axis";
            chartResults.Series[1].Name = "Gyroscope Yaxis";
            chartResults.Series[2].Name = "Gyroscope Z axis";
            chartResults.Series[3].Name = "PREDICTED SKI-TURNS";

            chartResults.ChartAreas[0].AxisX.Title = "Sample";
            chartResults.ChartAreas[0].AxisY.Title = "Value";
        }

        private void DisplayResults()
        {
            circularProgressBar.Visible = false;
            timer1.Stop();

            chartResults.Series[0].Enabled = true;
            chartResults.Series[1].Enabled = true;
            chartResults.Series[2].Enabled = true;
            chartResults.Series[3].Enabled = true;

            for (int i = 0; i < results.responses.GetLength(0); i++)
            {
                chartResults.Series[0].Points.AddXY(i + 1, results.leftGyroX[i]);
                chartResults.Series[1].Points.AddXY(i + 1, results.leftGyroY[i]);
                chartResults.Series[2].Points.AddXY(i + 1, results.leftGyroZ[i]);
                chartResults.Series[3].Points.AddXY(i + 1, results.responses[i]);
            }

            turnsInfo.Text = "Detected ski-turns: " + results.turnsLeft + " turns left and " + results.turnsRight + " turns right, " //+ Environment.NewLine
            + "where positive values indicate turn left and negative values stand for turns right.";
            turnsInfo.Visible = true;

            plotExists = true;
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void temp()
        {
            // create client to call service's methods
            client = new Service1Client();

            // read data from selected file
            byte[] fileBytes = File.ReadAllBytes(@filePath);
            int arraySize = fileBytes.Length;

            // 45000 bytes are sent at a time
            int sizeOfPackage = 45000;
            int numberOfPackages = (int)(arraySize / sizeOfPackage) + 1;
            int sizeOfLastPackage = arraySize - (int)(arraySize / sizeOfPackage) * sizeOfPackage;

            FileStream readStream = new FileStream(@filePath, FileMode.Open);
            BinaryReader reader = new BinaryReader(readStream);

            // read and send each package to service
            for (int i = 1; i <= numberOfPackages; i++)
            {
                if (i < numberOfPackages)
                {
                    byte[] package = reader.ReadBytes(sizeOfPackage);
                    client.SendPackage(package, numberOfPackages);
                }
                else // for the last package the number of bytes is different
                {
                    byte[] package = reader.ReadBytes(sizeOfLastPackage);
                    client.SendPackage(package, numberOfPackages);
                }
            }
            results = client.DetectTurns();
            client.Close();
            reader.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (count == 0)
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    count++;
                }
                else
                    openFileDialog.InitialDirectory = filePath;

                openFileDialog.Filter = "(.data)|*.data";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }

            if (filePath == string.Empty)
            {
                MessageBox.Show("None file has been selected. Try again!");
            }
            else
            {
                textBox1.Text = filePath;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (filePath == String.Empty)
            {
                MessageBox.Show("Select your Sensors Data first, please.");
            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = false;

                if (plotExists == true)
                {
                    //remove existing points from plot
                    chartResults.Series[0].Points.Clear();
                    chartResults.Series[1].Points.Clear();
                    chartResults.Series[2].Points.Clear();
                    chartResults.Series[3].Points.Clear();

                    chartResults.Series[0].Enabled = false;
                    chartResults.Series[1].Enabled = false;
                    chartResults.Series[2].Enabled = false;
                    chartResults.Series[3].Enabled = false;
                    turnsInfo.Visible = false;

                    plotExists = false;
                }

                circularProgressBar.Visible = true;
                timer1.Start();

                Thread thr = new Thread(() =>
                {
                    temp();
                    Action action = new Action(DisplayResults);
                    this.BeginInvoke(action);
                });
                thr.Start();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            circularProgressBar.StartAngle += 5;
        }
    }
}
