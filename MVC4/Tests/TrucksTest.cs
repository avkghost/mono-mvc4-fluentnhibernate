using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using MVC4.Generics;
using MVC4.Helpers;
using MVC4.Models;

namespace MVC4.Tests
{
	[TestFixture]
	public class TruckTest
	{
		[Test]
		public void Add_100_Trucks_With_1000_Location_Points_Each()
		{
			for (int i = 0; i < 100; i++)
			{
				// Notice the unit of work we are using is to commit
				//    one truck's data at a time.
				UnitOfWork unitOfWork = new UnitOfWork(NHibernateHelper.SessionFactory);

				Repository<Truck> repository = new Repository<Truck>(unitOfWork.Session);

				Truck truck = CreateTruck(string.Format("Truck {0}", i + 1), 1000);
				repository.Add(truck);

				unitOfWork.Commit();
				Console.WriteLine (truck.Name);
			}
		}

		private static Truck CreateTruck(string name, int numberOfLocations)
		{
			Truck truck = new Truck
			{
				Name = name,
				PlateNumber = string.Format("ABC-{0}", name),
				Type = string.Format("Type {0}", name)
			};
			Driver driver = new Driver
			{
				FirstName = "Bob",
				LastName = "Cravens"
			};
			truck.AddDriver(driver);

			for (int j = 0; j < numberOfLocations; j++)
			{
				Location location = new Location
				{
					Timestamp = DateTime.Now.AddMinutes(5*j),
					Latitude = 10.0f + j,
					Longitude = -10.0f - j
				};
				truck.AddLocation(location);
			}
			return truck;
		}

		[Test]
		public void Count_The_Number_Of_Locations_In_The_DB()
		{
			UnitOfWork unitOfWork = new UnitOfWork(NHibernateHelper.SessionFactory);
			Repository<Location> repository = new Repository<Location>(unitOfWork.Session);

			// This call uses LINQ to NHibernate to create an optimized SQL query.
			//    So instead of pulling all the entities from the DB and then using
			//    LINQ to count them, it instead pushes the counting to the DB by
			//    generating the appropriate SQL. Much much much faster!
			int count = repository.All().Count();
		}

		[Test]
		public void Given_A_Driver_Determine_The_Last_Known_Location()
		{
			UnitOfWork unitOfWork = new UnitOfWork(NHibernateHelper.SessionFactory);
			Repository<Driver> driverRepo = new Repository<Driver>(unitOfWork.Session);
			Driver driver = driverRepo.All().First();
			if(driver!=null)
			{
				// At this point LINQ to NHibernate has not loaded all the Location entities.
				//    Because of LINQ's delayed execution, the following query can be optimized
				//    to let the DB do the filtering.
				Location lastLocation = driver.Truck.Locations.OrderByDescending(x => x.Timestamp).First();
			}
		}

		[Test]
		public void Get_The_Last_10_Locations_Of_A_Given_Truck()
		{
			UnitOfWork unitOfWork = new UnitOfWork(NHibernateHelper.SessionFactory);
			Repository<Truck> repository = new Repository<Truck>(unitOfWork.Session);
			Truck truck = repository.All().First();
			if(truck != null)
			{
				// Again the power of LINQ to NHibernates optimized queries and delayed execution.
				IEnumerable<Location> pagedLocations = truck.Locations.OrderByDescending(x => x.Timestamp).Take(10);
			}
		}
	}
}

