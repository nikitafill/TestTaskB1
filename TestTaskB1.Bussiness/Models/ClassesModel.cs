using System.ComponentModel.DataAnnotations.Schema;

public class ClassModel
{
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(MAX)")]
    public string Name { get; set; }
    public ICollection<Account> Accounts { get; set; } = new List<Account>();

    public decimal IncomingBalance { get; set; } // Входящее сальдо
    public decimal IncomingPassive { get; set; } // Входящее сальдо Пассив
    public decimal Debit { get; set; } // Обороты Дебет
    public decimal Credit { get; set; } // Обороты Кредит
    public decimal OutgoingBalance { get; set; } // Исходящее сальдо
    public decimal OutgoingPassive { get; set; } // Исходящее сальдо Пассив
}