using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementProject
{
    public class CI_Manager
    {
        public bool not_item { get; set; }

        public CI_Manager()
        {
            using (var db = new ConfigurationManagmentEntities())
            {
                var Obj = db.ConfigurationItems.SqlQuery("select * from ConfigurationItem").ToList();
                if (Obj.Count() > 0)
                    not_item = false;
                else
                    not_item = true;
            }
        }
        public void AddConfigurationItem()
        {
            //INITIAL COMMIT
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
            if (this.not_item)
            {
                Console.WriteLine("No se ha añadido ningun Configuration Item!");
            }
            else
            {
                Console.WriteLine("Agregando Nueva Dependencia!");
                Console.WriteLine();
                this.ListConfigurationItem();
                Console.WriteLine();
                Console.WriteLine("-Escriba el ID del CI al que le quiere añadir Dependencia: ");
                int myIDBase = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-Escriba el ID de la Dependencia del CI");
                int myIDDependecy = Convert.ToInt32(Console.ReadLine());

                using (var db = new ConfigurationManagmentEntities())
                {
                    var Obj = new DependencyItem();
                    Obj.IDBaseCI = myIDBase;
                    Obj.IDDependencyCI = myIDDependecy;
                    Obj.States = "A";
                    db.DependencyItems.Add(Obj);

                    var baseCI = db.ConfigurationItems.SqlQuery($"select * from ConfigurationItem where id = '{myIDBase}'").ToList().SingleOrDefault();
                    string nameBase = baseCI.CIName;
                    var dependecyCI = db.ConfigurationItems.SqlQuery($"select * from ConfigurationItem where id = '{myIDDependecy}'").ToList().SingleOrDefault();
                    string nameDependecy = dependecyCI.CIName;

                    Console.WriteLine($"Se ha añadido la Dependencia '{nameDependecy}' al Configuration Item '{nameBase}'");
                    db.SaveChanges();
                }
            }

        }
        public void UpdateConfigurationItem()
        {
            Console.WriteLine("Actualizando Configuration Item!");

        }

        public void ListConfigurationItem()
        {
            using (var db = new ConfigurationManagmentEntities())
            {
                var items = db.ConfigurationItems.SqlQuery("select * from ConfigurationItem").ToList();
                if (this.not_item)
                {
                    Console.WriteLine("No se ha añadido ningun Configuration Item!");
                }
                else
                {
                    Console.WriteLine("Configuration Items Agregados:");
                    int count = 1;
                    foreach (var item in items)
                    {
                        string state = "";
                        if (item.States == "A")
                        {
                            state = "Activo";
                        }
                        else
                        {
                            state = "No Activo";
                        }
                        Console.WriteLine($"{count}- {item.CIName} | V:{item.CIVersion} | Estado:{state}");
                        count++;
                    }

                }
            }
        }

        

        public void RiskAnalysis()
        {
            Console.WriteLine("Analisis de Configuration Item y Dependencias!");

        }
    }
}
