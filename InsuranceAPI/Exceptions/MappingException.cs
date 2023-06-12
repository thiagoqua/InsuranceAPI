namespace InsuranceAPI.Exceptions {
    public class MappingException : Exception {
        public MappingException(string errorCell) : base() {
            ErrorCell = errorCell;
        }

        public MappingException(string errorCell, int errorRow)
            : base(errorRow.ToString() + " - " + errorCell) {
            ErrorCell = errorCell;
            ErrorRow = errorRow;
        }

        public string ErrorCell { get; }

        public int ErrorRow { get; set; } 
    }
}
