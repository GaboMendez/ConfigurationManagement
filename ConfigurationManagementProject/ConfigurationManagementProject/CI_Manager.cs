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
            Console.WriteLine("_________________________________");
            this.ListConfigurationItem();
            Console.WriteLine("-Escriba el ID del CI al que quiere Actualizar la Version: ");
            int updatingCI = Convert.ToInt32(Console.ReadLine());

            using (var db = new ConfigurationManagmentEntities())
            {
                var myCI = db.ConfigurationItems.SqlQuery($"select * from ConfigurationItem where id = '{updatingCI}'").ToList().SingleOrDefault();
                if (myCI.States != "A")
                {
                    Console.WriteLine("El Configuration Item No Esta Activo!");
                    return;
                }
                else
                {
                    string[] oldVersion = myCI.CIVersion.Split('.');
                    Console.WriteLine("***Escriba la nueva Version del CI en el Formato MAJOR.MINOR.PATCH");
                    string newVersion = Console.ReadLine();
                    string[] versions = newVersion.Split('.');

                    bool yesDependecy = hasDependecies(updatingCI);

                    if(versions[0] != oldVersion[0])
                    {
                        if (yesDependecy)
                        {
                            Console.WriteLine("ALERT: Este CI Tiene Dependencia y estas Actualizando su MAJOR!!!");
                            Console.WriteLine("     No se puede Actualizar la Version del CI!");
                            return;
                        }
                        else
                        {
                            myCI.CIVersion = newVersion;
                            Console.WriteLine("Se ha Actualizado la Version del Configuration Item!");
                        }
                    }
                    else
                    {
                        myCI.CIVersion = newVersion;
                        Console.WriteLine("Se ha Actualizado la Version del Configuration Item!");
                    }
                    db.SaveChanges();
                }
            }

        }
        bool hasDependecies(int ID)
        {
            using (var db = new ConfigurationManagmentEntities())
            {
                var myItem = db.DependencyItems.SqlQuery($"select * from DependencyItem where IDBaseCI = '{ID}'").ToList();
                if (myItem.Count() > 0)
                    return true;
                else
                    return false;
            }
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

            if (this.not_item)
            {
                Console.WriteLine("No se ha añadido ningun Configuration Item!");
            }
            else
            {
                using (var db = new ConfigurationManagmentEntities())
                {
                    var ConfigurationItems = db.ConfigurationItems.SqlQuery("select * from ConfigurationItem").ToList();
                    foreach (var item in ConfigurationItems)
                    {
                        var baseCI = db.ConfigurationItems.SqlQuery($"select * from ConfigurationItem where ID = '{item.ID}'").ToList().SingleOrDefault();
                        var depedenciesCI = db.DependencyItems.SqlQuery($"select * from DependencyItem where IDBaseCI = '{baseCI.ID}'").ToList();

                        Console.WriteLine($"***ID: {item.ID} | CI: '{baseCI.CIName}' | V: {baseCI.CIVersion}");
                        Console.WriteLine("         Dependencias: ");
                        int count = 1;
                        foreach (var dep in depedenciesCI)
                        {
                            var myDep = db.ConfigurationItems.SqlQuery($"select * from ConfigurationItem where ID = '{dep.IDDependencyCI}'").ToList().FirstOrDefault();
                            Console.WriteLine($"        {count}) {myDep.CIName}     | V: {myDep.CIVersion}");
                            count++;
                        }
                        Console.WriteLine();
                    }
                }
            }

        }
    }
}
