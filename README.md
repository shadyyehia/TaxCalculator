# TaxCalculator


Please consider checking the solution as follows:
- Start with the swagger URL : https://localhost:7259/swagger/index.html
- Try it out to make sure that all scenarios are covered.
- In `congestion-tax-calculator-net-core` project, Check the bug fix in GetTax function  (long diffInMillies = date.Millisecond - intervalStart.Millisecond;)
- Check the unit tests in `congestion-tax-calculator-net-core.tests` , it covered most of scenarios including exceptions.
- Check the bounus scenario. I connect to an sqlLite file. It could be anything else. ( AWS DynamoDB, or RDS, or any on primeses DB) and check `DBAccess` project.
- Check the API project `TaxCalculator.API`


My additions:
- added vehicle types as classes to Vehicle folder
- removed old enum , and added isExempted property to each vehicle class
- added new enum "vehicles"
- raised an exception if captured dates are spaned on more than one day.
- raised an exception if captured dates are empty array
- added web api to consume the library using HTTP.
- added swagger (openAPI) for documenting the API : https://localhost:7259/swagger/index.html
- added ITaxCalculator , in case we wanted to add more tax calculators in the future
- added VehicleFactory, to create vehicles based on VehicleType.
- added IDB_Manager as an interface and the class SQLite_Manager for connecting to the external data store.
- Bounus Scenario: added sqllite file in TaxCalculator.API project "SwedenTaxes.db"
and I read city parameters from it.
- added DBAccess project to handle the external data store.
- DB connection string is configurable in app.settings.


