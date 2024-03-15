using System.ComponentModel.DataAnnotations;

namespace Ch25_ModelValidation.Infrastructure {
    public class MustBeTrueAttribute :ValidationAttribute {
        //public override bool IsValid(object value) => value is bool v && v;

        public override bool IsValid(object value) {
            //return value is bool && (bool)value;

            return value is bool v && v;
        }
    }
}