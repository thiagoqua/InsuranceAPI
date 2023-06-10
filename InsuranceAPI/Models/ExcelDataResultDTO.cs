namespace InsuranceAPI.Models {
    public class ExcelDataResultDTO {
        public List<Insured> Interpreted { get; set; } = null;
        public List<int> NonInterpretedRows { get; set; } = null;

        public ExcelDataResultDTO(List<Insured> interpreted, List<int> nonInterpretedRows) {
            Interpreted = interpreted;
            NonInterpretedRows = nonInterpretedRows;
        }
    }
}
