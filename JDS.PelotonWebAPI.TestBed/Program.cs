using JDS.PelotonWebAPI.Domain;
using JDS.PelotonWebAPI.Domain.Enums;
using JDS.PelotonWebAPI.Domain.Repositories;
using JDS.PelotonWebAPI.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDS.PelotonWebAPI.Domain.Wrappers;

namespace JDS.PelotonWebAPI.TestBed
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);

            var mainFolder = @"C:\Peloton\WellView";

            var assembly = Assembly.LoadFrom(mainFolder + @"\system\bin\Peloton.AppFrame.IO.dll");
            var library = Assembly.LoadFrom(mainFolder + @"\system\bin\Peloton.AppFrame.Library.dll");

            var io = new IOEngine(mainFolder + @"\system", mainFolder + @"\custom", mainFolder + @"\user", "All Data", "", "US");
            io.Connect(DBMS.SQLCompact, mainFolder + @"\user\database\wv10.0 sample.sdf", "", "", "", true);

            var dt = io.Library.GetDataTable("libcascomp");

            var firstEntity = io.Search("SELECT idwell from wvwellheader", new List<object>()).FirstOrDefault();

            var td = io.Tables["wvjob"].GetData(firstEntity);

            var tdrc = td.Records;

            var f = tdrc[0];

            var lastRecordId = tdrc.Last().UniqueId;

            var tdr = tdrc[lastRecordId];

            var doesContain = tdrc.Contains(lastRecordId);

            var removedRecord = tdrc.Remove(lastRecordId);

            td.Update(true);

            Console.WriteLine("Done!");

        }

        static Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
        }
    }
}
