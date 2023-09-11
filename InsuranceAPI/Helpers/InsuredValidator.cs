using InsuranceAPI.Models;

namespace InsuranceAPI.Helpers {
    public class InsuredValidator {
        public static void EnsureValidInsured(Insured insured) {
            //invalidate the company and producer insertion
            insured.CompanyNavigation = null;
            insured.ProducerNavigation = null;

            if(!ValidLife(insured.Life))
                throw new Exception("invalid life");
        }

        private static bool ValidLife(string life) {
            string[] splitted = life.Split('-');
            if(splitted.Length != 2)
                return false;

            foreach(string date in splitted) {
                string[] days = date.Split('/');
                foreach(string day in days) {
                    if(day.Length <= 0 || day.Length >= 3)
                        return false;
                    try {
                        //if it is a number it doesnt fail
                        int.Parse(day);
                    } catch(Exception) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
