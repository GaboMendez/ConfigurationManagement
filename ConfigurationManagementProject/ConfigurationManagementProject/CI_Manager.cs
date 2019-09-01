using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementProject
{
    public class CI_Manager
    {
        public void AddConfigurationItem()
        {
            Console.WriteLine("Agregando Nuevo Configuration Item!");
            Console.WriteLine("1. Escribir el Nombre del CI");
            string name = Console.ReadLine();
            Console.WriteLine("2. Escribir la Descripcion del CI");
            string description = Console.ReadLine();
            Console.WriteLine("3. Escribir la Version del CI");
            string version = Console.ReadLine();

            using (var db = new ConfigurationManagmentEntities())
            {
                var Obj = new ConfigurationItem();
                Obj.CIName = name;
                Obj.CIDescripton = description;
                Obj.CIVersion = version;
                Obj.States = "A";

                db.ConfigurationItems.Add(Obj);
                db.SaveChanges();
                Console.WriteLine("Se ha Agregado el Configuration Item");

            }

        }

        public void AddDependecy()
        {
            Console.WriteLine("Agregando Nueva Dependencia!");

        }
        public void UpdateConfigurationItem()
        {
            Console.WriteLine("Actualizando Configuration Item!");

        }

        public void ListConfigurationItem()
        {
            Console.WriteLine("Listar Configuration Item's!");

        }

        public void RiskAnalysis()
        {
            Console.WriteLine("Analisis de Configuration Item y Dependencias!");

        }
    }
}
