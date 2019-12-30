using System;
using System.Windows.Forms;
using WindowsFormsApp.ServiceReference;
using System.IO;
using System.Threading;
using System.Drawing;

namespace WindowsFormsApp
{
    public partial class Form : System.Windows.Forms.Form
    {
        string filePath = string.Empty;
        int count;
        bool plotExists;
        OutputData results;

        public Form()
        {
            InitializeComponent();
            count = 0;
            plotExists = false;
            circularProgressBar.Visible = false;

            loadData_button.Enabled = false;

            turnsInfo.Visible = false;

            // chart settings
            chartGyroscope.Visible = false;
            //chartGyroscope.Series[0].Enabled = false;
            //chartGyroscope.Series[1].Enabled = false;
            //chartGyroscope.Series[2].Enabled = false;
            //chartGyroscope.Series[3].Enabled = false;
            chartGyroscope.Series[0].Name = "gyroscope X axis";
            chartGyroscope.Series[1].Name = "gyroscope Y axis";
            chartGyroscope.Series[2].Name = "gyroscope Z axis";
            turnsLegend.Text = " 1 - left turn" + System.Environment.NewLine + "-1 - right turn"
                + System.Environment.NewLine + " 0 - no turn";
            turnsLegend.Visible = false;
            chartGyroscope.Series[4].Name = "gyroscope X axis ";
            chartGyroscope.Series[5].Name = "gyroscope Y axis ";
            chartGyroscope.Series[6].Name = "gyroscope Z axis ";
            chartGyroscope.ChartAreas[0].AxisX.Title = "Sample";
            chartGyroscope.ChartAreas[0].AxisY.Title = "Value";
            chartGyroscope.ChartAreas[1].AxisX.Title = "Sample";
            chartGyroscope.ChartAreas[1].AxisY.Title = "Value";
            chartGyroscope.ChartAreas[2].AxisX.Title = "Sample";
            chartGyroscope.ChartAreas[2].AxisY.Title = "Value";
            chartGyroscope.Titles[0].Text = "Gyroscope signals from left ski";
            chartGyroscope.Titles[1].Text = "Gyroscope signals from right ski";
            chartGyroscope.Titles[2].Text = "Detected ski-turns";
            leftGyroX.Text = "Gyroscope X axis";
            leftGyroY.Text = "Gyroscope Y axis";
            leftGyroZ.Text = "Gyroscope Z axis";
            rightGyroX.Text = "Gyroscope X axis";
            rightGyroY.Text = "Gyroscope Y axis";
            rightGyroZ.Text = "Gyroscope Z axis";
            leftGyroX.Checked = true;
            leftGyroY.Checked = true;
            leftGyroZ.Checked = true;
            rightGyroX.Checked = true;
            rightGyroY.Checked = true;
            rightGyroZ.Checked = true;
            leftGyroX.Visible = false;
            leftGyroY.Visible = false;
            leftGyroZ.Visible = false;
            rightGyroX.Visible = false;
            rightGyroY.Visible = false;
            rightGyroZ.Visible = false;
        }

        private void GetResponses()
        {
            // create client to call service's methods
            ServiceClient client = new ServiceClient();

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

        private void DisplayResults()
        {
            circularProgressBar.Visible = false;
            timer.Stop();

            chartGyroscope.Visible = true;

            // display gyroscope left
            for (int i = 0; i < results.responses.GetLength(0); i++)
            {
                chartGyroscope.Series[0].Points.AddXY(i + 1, results.leftGyroX[i]);
                chartGyroscope.Series[1].Points.AddXY(i + 1, results.leftGyroY[i]);
                chartGyroscope.Series[2].Points.AddXY(i + 1, results.leftGyroZ[i]);
            }

            // display gyroscope right
            for (int i = 0; i < results.responses.GetLength(0); i++)
            {
                chartGyroscope.Series[4].Points.AddXY(i + 1, results.rightGyroX[i]);
                chartGyroscope.Series[5].Points.AddXY(i + 1, results.rightGyroY[i]);
                chartGyroscope.Series[6].Points.AddXY(i + 1, results.rightGyroZ[i]);
            }

            // display labels
            for (int i = 0; i < results.responses.GetLength(0); i++)
            {
                chartGyroscope.Series[3].Points.AddXY(i + 1, results.responses[i]);
            }

            // display legend
            leftGyroX.Visible = true;
            leftGyroY.Visible = true;
            leftGyroZ.Visible = true;
            rightGyroX.Visible = true;
            rightGyroY.Visible = true;
            rightGyroZ.Visible = true;

            turnsInfo.Text = results.turnsLeft + " left turns and " + results.turnsRight + " right turns are classified";
            turnsInfo.Visible = true;
            turnsLegend.Visible = true;

            plotExists = true;
            selectData_button.Enabled = true;
        }

        private void SelectDataButton(object sender, EventArgs e)
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
                selectedFile.Text = filePath;
                // resize textbox containing the path to fit in
                Size size = TextRenderer.MeasureText(selectedFile.Text, selectedFile.Font);
                selectedFile.Width = size.Width;
                loadData_button.Enabled = true;
            }
        }

        private void DetectTurnsButton(object sender, EventArgs e)
        {            
            if (plotExists == true)
            {
                //remove existing points from plots
                chartGyroscope.Series[0].Points.Clear();
                chartGyroscope.Series[1].Points.Clear();
                chartGyroscope.Series[2].Points.Clear();
                chartGyroscope.Series[3].Points.Clear();
                chartGyroscope.Series[4].Points.Clear();
                chartGyroscope.Series[5].Points.Clear();
                chartGyroscope.Series[6].Points.Clear();

                // hide chart
                chartGyroscope.Visible = false;

                turnsInfo.Visible = false;
                plotExists = false;

                // hide legend
                leftGyroX.Visible = false;
                leftGyroY.Visible = false;
                leftGyroZ.Visible = false;
                rightGyroX.Visible = false;
                rightGyroY.Visible = false;
                rightGyroZ.Visible = false;
                leftGyroX.Checked = true;
                leftGyroY.Checked = true;
                leftGyroZ.Checked = true;
                rightGyroX.Checked = true;
                rightGyroY.Checked = true;
                rightGyroZ.Checked = true;
                turnsLegend.Visible = false;
            }

            selectData_button.Enabled = false;
            loadData_button.Enabled = false;
            
            // run progressbar
            circularProgressBar.Visible = true;
            timer.Start();

            Thread thr = new Thread(() =>
            {
                GetResponses();
                Action action = new Action(DisplayResults);
                this.BeginInvoke(action);
            });
            thr.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            circularProgressBar.StartAngle += 5;
        }


        private void Form_Resize(object sender, EventArgs e)
        {
            //MessageBox.Show("You are in the Form.Resize event.");
            int X = chartGyroscope.Location.X + chartGyroscope.Size.Width;

            int Y0 = chartGyroscope.Location.Y + 3 * chartGyroscope.Size.Height / 35;
            leftGyroX.Location = new System.Drawing.Point(X, Y0);

            int Y1 = chartGyroscope.Location.Y + 5 * chartGyroscope.Size.Height / 35;
            leftGyroY.Location = new System.Drawing.Point(X, Y1);

            int Y2 = chartGyroscope.Location.Y + 7 * chartGyroscope.Size.Height / 35;
            leftGyroZ.Location = new System.Drawing.Point(X, Y2);

            int Y3 = chartGyroscope.Location.Y + 14 * chartGyroscope.Size.Height / 35 + 5;
            rightGyroX.Location = new System.Drawing.Point(X, Y3);

            int Y4 = chartGyroscope.Location.Y + 16 * chartGyroscope.Size.Height / 35 + 5;
            rightGyroY.Location = new System.Drawing.Point(X, Y4);

            int Y5 = chartGyroscope.Location.Y + 18 * chartGyroscope.Size.Height / 35 + 5;
            rightGyroZ.Location = new System.Drawing.Point(X, Y5);

            int Y6 = chartGyroscope.Location.Y + 25 * chartGyroscope.Size.Height / 35 + 5;
            turnsLegend.Location = new System.Drawing.Point(X, Y6);
        }

        private void leftGyroX_CheckedChanged(object sender, EventArgs e)
        {
            if (leftGyroX.Checked == true)
                chartGyroscope.Series[0].Enabled = true;
            else
                chartGyroscope.Series[0].Enabled = false;
        }

        private void leftGyroY_CheckedChanged(object sender, EventArgs e)
        {
            if (leftGyroY.Checked == true)
                chartGyroscope.Series[1].Enabled = true;
            else
                chartGyroscope.Series[1].Enabled = false;
        }

        private void leftGyroZ_CheckedChanged(object sender, EventArgs e)
        {
            if (leftGyroZ.Checked == true)
                chartGyroscope.Series[2].Enabled = true;
            else
                chartGyroscope.Series[2].Enabled = false;
        }

        private void rightGyroX_CheckedChanged(object sender, EventArgs e)
        {
            if (rightGyroX.Checked == true)
                chartGyroscope.Series[4].Enabled = true;
            else
                chartGyroscope.Series[4].Enabled = false;
        }

        private void rightGyroY_CheckedChanged(object sender, EventArgs e)
        {
            if (rightGyroY.Checked == true)
                chartGyroscope.Series[5].Enabled = true;
            else
                chartGyroscope.Series[5].Enabled = false;
        }

        private void rightGyroZ_CheckedChanged(object sender, EventArgs e)
        {
            if (rightGyroZ.Checked == true)
                chartGyroscope.Series[6].Enabled = true;
            else
                chartGyroscope.Series[6].Enabled = false;
        }
    }
}
