namespace Eva.Core.Domain.DTOs
{
    public record UpdateComplexNumberDto
    {
        public int Id { get; init; }
        public double Real { get; init; }
        public double Imaginary { get; init; }
    }
}
