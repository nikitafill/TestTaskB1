public class Account
{
    public int Id { get; set; }
    public int ClassId { get; set; }
    public string AccountNumber { get; set; }
    public decimal IncomingBalance { get; set; } // Входящее сальдо Актив
    public decimal IncomingPassive { get; set; } // Входящее сальдо Пассив
    public decimal Debit { get; set; } // Обороты Дебет
    public decimal Credit { get; set; } // Обороты Кредит
    public decimal OutgoingBalance { get; set; } // Исходящее сальдо Актив
    public decimal OutgoingPassive { get; set; } // Исходящее сальдо Пассив
}