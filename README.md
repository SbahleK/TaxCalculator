# Tax Calculator

This solution contains both the API and a Razor Page UI

## To Review API 

You need to install .Net 8 to run this solution.
The solution can be reviewed using visual studio IDE.
Tests can be executed using test explore in visual studio.

The App uses a SQLLite db that is created when your initially start the application

The solution can also be exucuted in a docker container.

### How to execute the solution?
You can execute the solution from a VS IDE (select multiple startup projects(TaxCalculator.API and TaxCalculator.UI))

## API
A swagger interface will loaded to allow the user to interact easily with endpoints
![image](https://github.com/SbahleK/TaxCalculator.API/assets/26767857/e54901e2-471a-42df-95a3-c05296a63a59)

The following endpoint takes in a (postal code & annual income) and return the calculated tax value
![image](https://github.com/SbahleK/TaxCalculator.API/assets/26767857/9a524c4c-918d-4fc1-a244-9a528e2b1ada)

The following endpoint returns the list of caltculated taxes
![image](https://github.com/SbahleK/TaxCalculator.API/assets/26767857/dc483208-760a-40a9-9528-1c9aebd5c5f7)





