Project is built using:

    .NET 8.0
    Visual Studio 2022 17.9.6
    Entitiy Framework Core 8.0.4
    SQL Server Express LocalDB

Before first run:

    In Visual Studio open Tools -> NuGet Package Manager -> Package Manager Console
    Run command 'update-database'
    That will update database using migration wich will create database and add initial data

Testing API

    -web API listening port is: 5050

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
	
		HTTP method 
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

	For all employees:

		HTTP method:
			GET

		URL:
			http://localhost:5050/api/employees/reports?startTime=10-01-2024T00-00&endTime=10-01-2025T00-00
			
			-working hours for all employees
			-required query string parameters:
				startTime
				endTime
			-data time format is dd-MM-yyyyTHH-mm

	For single employee:
	
		HTTP method:
			GET

		URL:
			http://localhost:5050/api/employees/reports?employeeId={ID}&startTime=10-01-2024T00-00&endTime=10-01-2025T00-00
			
			-working hours for single employees
			-required query string parameters:
				employeeId
				startTime
				endTime
			-change {ID} with valid employee ID
			-data time format is dd-MM-yyyyTHH-mm

