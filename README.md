# TaxCalculator

My additions:

- added unit tests to verify that the project works as expected.
- added vehicle types as classes to Vehicle folder
- removed old enum , and added isExempted property to each vehicle class
- added new enum "vehicles"
- fixed a bug in GetTax function. Related to this line : (long diffInMillies = date.Millisecond - intervalStart.Millisecond;)
- raised an exception if captured dates are spaned on more than one day.
This exception is better to be in the API level. To let the exception optional.
- added web api to consume the library using HTTP.
- added swagger (openAPI) for documenting the API : https://localhost:44353/swagger/index.html
- added ITaxCalculator , in case we wanted to add more tax calculators in the future
- added VehicleFactory, to create vehicles based on VehicleType.
- Bounus Scenario: added sqllite file in TaxCalculator.API project "SwedenTaxes.db"
and I read city parameters from it.
