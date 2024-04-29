Project is built using

    .NET 8.0
    Visual Studio 2022 17.9.6
    Entitiy Framework Core 8.0.4
    SQL Server Express LocalDB

Before the first run

    Open project in Visual Studio
    From Visual Studio menu open Tools -> NuGet Package Manager -> Package Manager Console
    Run command 'update-database' from Package Manager Console
    Command will update database using migration wich will create database and add initial data. App is ready for the first run.

Testing API

    After running project from Visual Studio, web API will listen on port 5050

1) Managing employees

	Get all employees:

		HTTP method:
			GET
   
		URL:
			http://localhost:5050/api/employees

	Get employee by id:

		HTTP method:
			GET
   
		URL:
			http://localhost:5050/api/employees/{ID}
			
			-change {ID} with valid employee ID


	Create new employee:
	
		HTTP method:
			POST
			
		URL:
			http://localhost:5050/api/employees

		HTTP body:
		{
			"firstName": "test",
			"lastName": "post"
		}


	Update employee's first or last name:

		HTTP method:
			PUT

		URL:
			http://localhost:5050/api/employees/{ID}

			-change {ID} with valid employee ID

		HTTP body:

		{
			"firstName": "test",
			"lastName": "put"
		}

	Delete employee:

		HTTP method:
			DELETE

		URL:
			http://localhost:5050/api/employees/{ID}

			-change {ID} with valid employee ID


3) Tracking working time

	Start tracking time:

		HTTP method:
			PUT

		URL:
			http://localhost:5050/api/employees/{ID}/tracking?type=start
			
			-change {ID} with valid employee ID			
			-required query string parameter
				type				
			-valid parameter values = start|stop

	Stop tracking time:
		
		HTTP method:
			PUT
			
		URL:
			http://localhost:5050/api/employees/{ID}/tracking?type=stop
			
			-change {ID} with valid employee ID			
			-required query string parameter
				type				
			-valid parameter values = start|stop

3) Geting reports

	Working hours for all employees:

		HTTP method:
			GET

		URL:
			http://localhost:5050/api/employees/reports?startTime=10-01-2024T00-00&endTime=10-01-2025T00-00
			
			-required query string parameters:
				startTime
				endTime
			-datetime format is dd-MM-yyyyTHH-mm

	Working hours for single employee:
	
		HTTP method:
			GET

		URL:
			http://localhost:5050/api/employees/reports?employeeId={ID}&startTime=10-01-2024T00-00&endTime=10-01-2025T00-00
			
			-required query string parameters:
				employeeId
				startTime
				endTime
			-change {ID} with valid employee ID
			-datetime format is dd-MM-yyyyTHH-mm

