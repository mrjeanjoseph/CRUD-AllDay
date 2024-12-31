using System;
using System.Collections.Generic;
using System.Linq;

namespace DEP.ServiceApplication {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class PatientService : IPatientService {
        public List<PatientInfo> GetAllPatientInfo() {

            using (var db = new DEPEntities()) {
                var patientInfo = db.PatientInfoes.ToList();
                return patientInfo;
            }
        }

        public PatientInfo GetPatientInfo(int id) {
            using(var db = new DEPEntities()) {
                var patientInfo = db.PatientInfoes.FirstOrDefault(p => p.PatientId == id);
                return patientInfo;
            }
        }
    }
}
