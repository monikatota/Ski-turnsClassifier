using System;
using System.IO;
using System.ServiceModel;
using System.Reflection;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Service : IService
    {
        static MLApp.MLApp _matlab;
        const string fileName = "SensorsDataToClassify.data";
        static string newFilePath; //= "D:\\FILES\\STUDIA\\Snowcookie\\" + fileName;
        static string scriptDirectory;
        static FileStream writeStream;
        static BinaryWriter writer;
        static int countReceivedPackages = 0;

        public void SendPackage(byte[] rawData, int numberOfpackages)
        {
            countReceivedPackages++;

            if (countReceivedPackages == 1)
            {
                // Get directiories
                var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                string projectDirectory = currentDirectory.Parent.FullName;
                newFilePath = projectDirectory + "\\matlab_scripts\\" + fileName;
                scriptDirectory = projectDirectory + "\\matlab_scripts";

                // This will get the current WORKING directory (i.e. \bin\Debug)
                string workingDirectory = Environment.CurrentDirectory;
                // or: Directory.GetCurrentDirectory() gives the same result

                // This will get the current PROJECT directory
                string projectDirectory_ = Directory.GetParent(workingDirectory).Parent.FullName;
                string startupPath = Environment.CurrentDirectory;

                string temp = AppDomain.CurrentDomain.BaseDirectory;
                var startDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

                var projectDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                var currentDirectory_ = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                string projectDirectory__ = currentDirectory.Parent.FullName;


                // Create temporary file with input data
                writeStream = new FileStream(newFilePath, FileMode.Create);
                writer = new BinaryWriter(writeStream);
            }

            // Write data to file
            writer.Write(rawData);

            if(countReceivedPackages== numberOfpackages) // If this is last package, then close binary writer
            {
                writer.Close();
                countReceivedPackages = 0;
            }
        }

        public OutputData DetectTurns()
        {
            // Change to the directory where the scripts are located 
            string execute = "cd " + scriptDirectory;
            _matlab.Execute(@execute);

            // Define the output 
            object output = null;

            // Call the MATLAB function classify_turns
            _matlab.Feval("classify_turns", 6, out output, newFilePath);

            // Define result as object
            object[] result = output as object[];

            // Prepare data to send to client
            OutputData classifiedTurns = new OutputData();

            double[,] leftGyroX_ = (double[,])result[0];
            double[,] leftGyroY_ = (double[,])result[1];
            double[,] leftGyroZ_ = (double[,])result[2];
            double[,] response_ = (double[,])result[3];

            double[] leftGyroX = new double[leftGyroX_.Length];
            double[] leftGyroY = new double[leftGyroY_.Length];
            double[] leftGyroZ = new double[leftGyroZ_.Length];
            double[] response = new double[response_.Length];

            for(int i=0; i<response_.Length; i++)
            {
                leftGyroX[i] = leftGyroX_[i, 0];
                leftGyroY[i] = leftGyroY_[i, 0];
                leftGyroZ[i] = leftGyroZ_[i, 0];
                response[i] = response_[i, 0];
            }

            classifiedTurns.leftGyroX = leftGyroX;
            classifiedTurns.leftGyroY = leftGyroY;
            classifiedTurns.leftGyroZ = leftGyroZ;
            classifiedTurns.responses = response;

            double turnsLeft = (double)result[4];
            classifiedTurns.turnsLeft = turnsLeft;
            double turnsRight = (double)result[5];
            classifiedTurns.turnsRight = turnsRight;

            return classifiedTurns;
        }

        public static void Main()
        {
            // Create a ServiceHost for the Service type.  
            using (ServiceHost serviceHost =
                   new ServiceHost(typeof(Service)))
            {
                // cmd ?
                // http://localhost:52050/Service.svc

                // Create the MATLAB instance
                _matlab = new MLApp.MLApp();

                // Open the ServiceHost to create listeners   
                // and start listening for messages.  
                serviceHost.Open();

                // The service can now be accessed.  
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                serviceHost.Close();
            }
        }
    }
}
