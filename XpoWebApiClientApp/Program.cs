using BIT.Xpo;
using BIT.Xpo.Providers.WebApi.Client;
using DevExpress.Xpo;
using System;

namespace XpoWebApiClientApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //Register XpoWebApiProvider 
            XpoWebApiProvider.Register();

            XpoInitializer DataStore002 = GetInitializer("002");

            XpoInitializer DataStore003 = GetInitializer("003");

            CreateData(DataStore002, "Jose Manuel Ojeda Melgar", "Rocco Ojeda Melgar");

            CreateData(DataStore003, "Javier", "Rafael");

            Console.ReadKey();


        }

        private static void CreateData(XpoInitializer DataStore, string Person1Name, string Person2Name)
        {
            var UoW = DataStore.CreateUnitOfWork();
            Person Person1 = new Person(UoW) { Name = Person1Name };
            Person Person2 = new Person(UoW) { Name = Person2Name };

            if (UoW.InTransaction)
                UoW.CommitChanges();




            var UoWRead = DataStore.CreateUnitOfWork();
            XPCollection<Person> people = new XPCollection<Person>(UoWRead);

            foreach (Person person in people)
            {
                Console.WriteLine(person.Name);
            }
        }

        private static XpoInitializer GetInitializer(string DataStoreId)
        {
            var XpoWebApiAspNetCore = XpoWebApiProvider.GetConnectionString("https://localhost:44359", "/XpoWebApi", string.Empty, DataStoreId);
            XpoInitializer xpoInitializer = new XpoInitializer(XpoWebApiAspNetCore, typeof(Person));

            xpoInitializer.InitSchema();
            return xpoInitializer;
        }
    }
}
