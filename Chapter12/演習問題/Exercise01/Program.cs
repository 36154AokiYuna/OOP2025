using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Runtime.CompilerServices;

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

            //12.1.2
            Employee[] employees = [
                new () {
                    Id = 123,
                    Name = "山田太郎",
                    HireDate = new DateTime(2018, 10, 1),
                },
                new () {
                    Id = 198,
                    Name = "田中華子",
                    HireDate = new DateTime(2020, 4, 1),
                },
            ];
            Serialize("employees.json", employees);

            //12.1.3
            var empdata = Deserialize_f("employees.json");
            foreach (var empd in empdata)
                Console.WriteLine(empd);
        }

        //12.1.1
        static string Serialize(Employee emp) {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, //←正しくはこれ
            };
            string jsonString = JsonSerializer.Serialize(emp, options);
            return jsonString;
        }

        //自分の回答
        //static Employee? Deserialize(string jsonString) {
        //    var employee = JsonSerializer.Deserialize<Employee>(jsonString);
        //    return employee;
        //}

        //模範解答
        static Employee? Deserialize(string text) {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var emp = JsonSerializer.Deserialize<Employee>(text, options);
            return emp;
        }

        //12.1.2
        //シリアル化してファイルへ出力する
        //自分の回答
        static void Serialize(string filePath, IEnumerable<Employee> employees) {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            string jsonString = JsonSerializer.Serialize(employees, options);
            File.WriteAllText(filePath, jsonString);
        }

        //模範解答
        //optionsの中に PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 追加
        //byte[] utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(employees, options);
        //File.WriteAllBytes(CallerFilePathAttribute,utf8Bytes);

        //12.1.3
        static Employee[] Deserialize_f(string filePath) {
            var text = File.ReadAllText(filePath);
            var empd = JsonSerializer.Deserialize<Employee[]>(text);
            return empd;
        }
    }

    public record Employee {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HireDate { get; set; }
    }
}
