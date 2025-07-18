using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Xml;
using System.Xml.Serialization;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var employees = Deserialize("employees.json");
            ToXmlFile(employees);
        }

        static Employee[] Deserialize(string filePath) {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };
            var text = File.ReadAllText(filePath);
            var employees = JsonSerializer.Deserialize<Employee[]>(text, options);
            return employees;
            //return employees ?? [];
        }

        //xmlファイルに変換　模範解答
        static void ToXmlFile(Employee[] employees) {
            using (var writer = XmlWriter.Create("employees.xml")) {

                XmlRootAttribute xRout = new XmlRootAttribute {
                    ElementName = "Employees"
                };

                var serializer = new XmlSerializer(typeof(Employee[]));
                //または var serializer = new XmlSerializer(employees.GetType(), xRoot);
                serializer.Serialize(writer, employees);
            } 
        }
}

    public record Employee {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime HireDate { get; set; }
    }
}
