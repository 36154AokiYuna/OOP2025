using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var emp = new Employee {
                Id = 123,
                Name = "山田太郎",
                HireDate = new DateTime(2018, 10, 1),
            };

            var jsonString = Serialize(emp);
            Console.WriteLine(jsonString);
            var obj = Deserialize(jsonString);
            Console.WriteLine(obj);
        }

        static string Serialize(Employee emp) {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            string jsonString = JsonSerializer.Serialize(emp, options);
            return jsonString;
        }

        static Employee? Deserialize(string jsonString) {
            var employee = JsonSerializer.Deserialize<Employee>(jsonString);
            return employee;
        }
    }

    public record Employee {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HireDate { get; set; }
    }
}
