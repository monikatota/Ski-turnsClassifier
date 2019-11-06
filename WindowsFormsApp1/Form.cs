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
        string filePath = "D:\\FILES\\STUDIA\\Snowcookie\\8FEE802E-202E-4963-BF6C-87878FF7FD1A_Daniel\\SensorsData.data"; // string.Empty;
        int count;
        bool plotExists;
        ServiceClient client;
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
            label1.Text = "1 - turn left" + System.Environment.NewLine + "-1 - turn right"
                + System.Environment.NewLine + "0 - no turns";
            label1.Visible = false;
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
            checkBox3.Text = "Gyroscope X axis";
            checkBox1.Text = "Gyroscope Y axis";
            checkBox2.Text = "Gyroscope Z axis";
            checkBox4.Text = "Gyroscope X axis";
            checkBox5.Text = "Gyroscope Y axis";
            checkBox6.Text = "Gyroscope Z axis";
            checkBox3.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox3.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox4.Visible = false;
            checkBox5.Visible = false;
            checkBox6.Visible = false;
        }

        private void GetResponses()
        {
            // create client to call service's methods
            client = new ServiceClient();

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
            timer1.Stop();

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
            checkBox3.Visible = true;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            checkBox4.Visible = true;
            checkBox5.Visible = true;
            checkBox6.Visible = true;

            turnsInfo.Text = results.turnsLeft + " turns left and " + results.turnsRight + " turns right are classified";
            turnsInfo.Visible = true;
            label1.Visible = true;

            plotExists = true;
            selectData_button.Enabled = true;
        }

        private void SelectData_button(object sender, EventArgs e)
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

        private void DetectTurns_button(object sender, EventArgs e)
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
                checkBox3.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox4.Visible = false;
                checkBox5.Visible = false;
                checkBox6.Visible = false;
                checkBox3.Checked = true;
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox4.Checked = true;
                checkBox5.Checked = true;
                checkBox6.Checked = true;
                label1.Visible = false;
            }

            selectData_button.Enabled = false;
            loadData_button.Enabled = false;
            
            // run progressbar
            circularProgressBar.Visible = true;
            timer1.Start();

            Thread thr = new Thread(() =>
            {
                GetResponses();
                Action action = new Action(DisplayResults);
                this.BeginInvoke(action);
            });
            thr.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            circularProgressBar.StartAngle += 5;
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
                chartGyroscope.Series[0].Enabled = true;
            else
                chartGyroscope.Series[0].Enabled = false;
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            //MessageBox.Show("You are in the Form.Resize event.");
            int X = chartGyroscope.Location.X + chartGyroscope.Size.Width;

            int Y0 = chartGyroscope.Location.Y + 3 * chartGyroscope.Size.Height / 35;
            checkBox3.Location = new System.Drawing.Point(X, Y0);

            int Y1 = chartGyroscope.Location.Y + 5 * chartGyroscope.Size.Height / 35;
            checkBox1.Location = new System.Drawing.Point(X, Y1);

            int Y2 = chartGyroscope.Location.Y + 7 * chartGyroscope.Size.Height / 35;
            checkBox2.Location = new System.Drawing.Point(X, Y2);

            int Y3 = chartGyroscope.Location.Y + 14 * chartGyroscope.Size.Height / 35 + 5;
            checkBox4.Location = new System.Drawing.Point(X, Y3);

            int Y4 = chartGyroscope.Location.Y + 16 * chartGyroscope.Size.Height / 35 + 5;
            checkBox5.Location = new System.Drawing.Point(X, Y4);

            int Y5 = chartGyroscope.Location.Y + 18 * chartGyroscope.Size.Height / 35 + 5;
            checkBox6.Location = new System.Drawing.Point(X, Y5);

            int Y6 = chartGyroscope.Location.Y + 25 * chartGyroscope.Size.Height / 35 + 5;
            label1.Location = new System.Drawing.Point(X, Y6);
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                chartGyroscope.Series[1].Enabled = true;
            else
                chartGyroscope.Series[1].Enabled = false;
        }
    }
}
