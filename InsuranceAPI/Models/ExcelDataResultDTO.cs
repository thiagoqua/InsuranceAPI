namespace InsuranceAPI.Models {
    public class ExcelDataResultDTO {
        public List<Insured> Interpreted { get; set; } = null;
        public List<string> NonInterpreted { get; set; } = null;

        public ExcelDataResultDTO(List<Insured> interpreted, List<string> nonInterpretedRows) {
            Interpreted = interpreted;
            NonInterpreted = nonInterpretedRows;
        }
    }
}
