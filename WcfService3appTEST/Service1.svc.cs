using System;
using System.IO;
using System.ServiceModel;

namespace WcfService3appTEST
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Service1 : IService1
    {
        static MLApp.MLApp _matlab;
        const string fileName = "SensorsDataToClassify.data";
        string newFilePath = "D:\\FILES\\STUDIA\\Snowcookie\\" + fileName;
        static FileStream writeStream;
        static BinaryWriter writer;
        static int countReceivedPackages = 0;

        public void SendPackage(byte[] rawData, int numberOfpackages)
        {
            countReceivedPackages++;

            if (countReceivedPackages == 1)
            {
                writeStream = new FileStream(newFilePath, FileMode.Create);
                writer = new BinaryWriter(writeStream);
            }

            // write data to file
            writer.Write(rawData);

            if(countReceivedPackages== numberOfpackages) // if this is last package, then close binary writer
            {
                writer.Close();
                countReceivedPackages = 0;
            }
        }

        public OutputData DetectTurns()
        {
            // Change to the directory where the function is located 
            _matlab.Execute(@"cd D:\FILES\STUDIA\Snowcookie");

            // Define the output 
            object output = null;

            // Call the MATLAB function classify_turns
            _matlab.Feval("classify_turns", 6, out output, newFilePath);

            // Define result as object
            object[] result = output as object[];

            // prepare data to send to client
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

        // Host the service within this EXE console application.  
        public static void Main()
        {
            // Create a ServiceHost for the CalculatorService type.  
            using (ServiceHost serviceHost =
                   new ServiceHost(typeof(Service1)))
            {// netsh http add urlacl url=http://+:52050/Service1.svc user=everyone
             // http://localhost:52050/Service1.svc

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
