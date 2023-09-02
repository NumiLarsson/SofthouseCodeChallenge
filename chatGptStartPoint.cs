using System;
using System.IO;
using System.Xml.Linq;

/// <summary>
/// This is what ChatGPT gave me, and that i then modified to work :) 
/// </summary>

class Program
{
    static void Main(string[] args)
    {
        string csvFilePath = "your_input.csv";
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
                            if (parts.Length >= 3)
                            {
                                if (personElement != null)
                                {
                                    peopleElement.Add(personElement);
                                }

                                personElement = new XElement("person",
                                    new XElement("firstname", parts[1]),
                                    new XElement("lastname", parts[2])
                                );
                            }
                            break;
                        case "T":
                            if (parts.Length >= 3 && personElement != null)
                            {
                                personElement.Add(new XElement("phone",
                                    new XElement("mobile", parts[1]),
                                    new XElement("landline", parts[2])
                                ));
                            }
                            break;
                        case "A":
                            if (parts.Length >= 4 && personElement != null)
                            {
                                personElement.Add(new XElement("address",
                                    new XElement("street", parts[1]),
                                    new XElement("city", parts[2]),
                                    new XElement("zip", parts[3])
                                ));
                            }
                            break;
                        case "F":
                            if (parts.Length >= 3 && personElement != null)
                            {
                                personElement.Add(new XElement("family",
                                    new XElement("name", parts[1]),
                                    new XElement("born", parts[2])
                                ));
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
