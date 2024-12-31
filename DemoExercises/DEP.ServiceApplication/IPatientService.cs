using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace DEP.ServiceApplication {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPatientService {
        [OperationContract, WebGet]
        List<PatientInfo> GetAllPatientInfo();

        [OperationContract, WebGet]
        PatientInfo GetPatientInfo(int id);
    }
}
