using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfService
{
    [ServiceContract]
    public interface IService
    {
        // Send data from client to service
        [OperationContract]
        void SendPackage(byte[] rawData, int numberOfpackages);

        // Use MATLAB scripts to process the data and classify ski-turns
        [OperationContract]
        OutputData DetectTurns();
    }

    // Service send data to client in this format
    [DataContract]
    public class OutputData
    {
        [DataMember]
        public double[] leftGyroX;
        [DataMember]
        public double[] leftGyroY;
        [DataMember]
        public double[] leftGyroZ;
        [DataMember]
        public double[] responses;
        [DataMember]
        public double turnsRight;
        [DataMember]
        public double turnsLeft;
    }
}
