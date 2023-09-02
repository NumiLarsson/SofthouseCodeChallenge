using System;
using System.IO;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        string csvFilePath = "test.csv";
        string xmlFilePath = "output.xml";

        XElement peopleElement = new XElement("people");
        XElement personElement = null;

        using (StreamReader reader = new StreamReader(csvFilePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');
                if (parts.Length > 0)
                {
                    string type = parts[0];

                    switch (type)
                    {
                        case "P":
                            if (personElement != null)
                            {
                                peopleElement.Add(personElement);
                            }
                            personElement = new XElement("person");
                            if (parts.Length >= 2){
                                personElement.Add(new XElement("firstname", parts[1]));
                            }
                            if (parts.Length >= 3){
                                personElement.Add(new XElement("lastname", parts[2]));
                            }
                            break;
                        case "T":
                            if (personElement != null)
                            {
                                var phoneElement = new XElement("phone");
                                if (parts.Length >= 2){
                                    phoneElement.Add(new XElement("mobile", parts[1]));
                                }
                                if (parts.Length >= 3){
                                    phoneElement.Add(new XElement("landline", parts[2]));
                                }
                                personElement.Add(phoneElement);
                            }
                            break;
                        case "A":
                            if (personElement != null)
                            {
                                var addressElement = new XElement("address");
                                if (parts.Length >= 2){
                                    addressElement.Add(new XElement("street", parts[1]));
                                }
                                if (parts.Length >= 3){
                                    addressElement.Add(new XElement("city", parts[2])); 
                                }
                                if (parts.Length >= 4){
                                    addressElement.Add(new XElement("zip", parts[3]));
                                }

                                personElement.Add(addressElement);
                            }
                            break;
                        case "F":
                            if (parts.Length >= 3 && personElement != null)
                            {
                                var familyElement = new XElement("family");
                                if(parts.Length >= 2){
                                    familyElement.Add(new XElement("name", parts[1]));
                                }
                                if(parts.Length >= 3){
                                    familyElement.Add(new XElement("born", parts[2]));
                                }
                                
                                personElement.Add(familyElement);
                            }
                            break;
                        default:
                            // Handle unsupported types or errors
                            break;
                    }
                }
            }
        }

        // Add the final personElement to peopleElement
        if (personElement != null)
        {
            peopleElement.Add(personElement);
        }

        XDocument xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), peopleElement);
        xmlDocument.Save(xmlFilePath);

        Console.WriteLine("XML document has been created.");
    }
}
