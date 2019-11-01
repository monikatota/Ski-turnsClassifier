using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfService3appTEST
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        OutputData DetectTurns();

        [OperationContract]
        void SendPackage(byte[] rawData, int numberOfpackages);
    }

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
