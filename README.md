# CarStockApp
## Installation instructions:
1. First make sure .NET 6.0 is installed to allow application to run: [.NET downloads](https://dotnet.microsoft.com/en-us/download)
2. Download code, go to terminal and change directory to ./CarStockApp from root folder.
3. Run the code below to ensure NuGet packages are installed and application is built, before then running it:
<p><code>dotnet restore</code></p>
<p><code>dotnet build</code></p>
<p><code>dotnet run</code></p>
4. Code should be working now. Now go to the localhost port the app is running on, and add <code>/swagger</code> to the end to use the Swagger version. Make sure to use the 'https' link, NOT 'http'. For instance, if the app is on <code>https://localhost:7142</code> then change address to: <code>https://localhost:7142/swagger</code>

## Logging-in instructions:
All routes except for /Login route do not work without JWT token. In order to use any of the other routes, you can login using details for 2 different example dealers:

Dealer 1:
* username: <code>dealer1</code>
* password: <code>pass1</code>

Dealer 2:
* username: <code>dealer2</code>
* password: <code>pass2</code>

Then, after logging in with one of these dealers, you'll be given a JWT token in the response. You need to supply the JWT Bearer token to 'Authorize' at the top-right of the page. For instance, if you're given the response:

<code>eyJh . . . hGPM</code>

then the value you must supply upon clicking 'Authorize' should be: 

<code>Bearer eyJh . . . hGPM</code>

This should allow you to use the other routes for the dealer you logged in. Note, you'll only be able to perform CRUD operations for that dealer's cars, so if you want to do the same for the other dealer's cars, you'll need to do the same process with that new dealer.

## Car routes:

### /SearchByMakeOnly 

#### Dealer 1 has the following makes: 
* <pre>Audi</pre>
* <pre>Toyota</pre>
* <pre>VW</pre>

#### Dealer 2 has the following makes:
* <pre>BMW</pre>
* <pre>Toyota</pre>

All of these makes give all of the cars in stock for each of the dealers, so they can be used to show what car types CRUD operations can be performed on for a given dealer.

However, if you want to see the list of all cars for both dealers, here is the list, with make model year and stock:
#### Dealer 1:
* <code>"Audi", "A4", 2023, 100</code>
* <code>"Audi", "A4", 2022, 50</code>
* <code>"Audi", "A6", 2023, 50</code>
* <code>"Audi", "A8", 2023, 10</code>
* <code>"Toyota", "Camry", 2023, 300</code>
* <code>"Toyota", "Camry", 2022, 250</code>
* <code>"Toyota", "Corolla", 2023, 200</code>
* <code>"VW", "Golf", 2023, 150</code>
* <code>"VW", "Polo", 2023, 70</code>

#### Dealer 2:
* <code>"Toyota", "Camry", 2022, 150</code>
* <code>"BMW", "M3", 2023, 90</code>
* <code>"BMW", "M3", 2022, 40</code>

### /SearchByModel

This route requires both the make (brand) and model as inputs, and it will give that car model for all its years (such as Audi A4 2022, Audi A4 2023).

### /SearchByYear

This route requires an integer input for cars of a given year.

### /SearchByEverything

This route is similar to '/SearchByModel', but is more specific, and requires year in addition to make and model values. This should then give just one car type.

### /UpdateCarStock

This route is used to change the number of stock for an existing car type. It requires the make model and year to get the right car, then the new stock number integer for that car. If the car does not exist however, an error will be returned.

### /AddCarStock

This route adds another car. It requires all the same inputs as 'UpdateCarStock'. If the car already exists, there'll be an error.

### /DeleteCarStock

This route deletes an existing car and all of its stock. It requires only make model and year as inputs to identify the car to be deleted. If the car doesn't exist, an error will be returned.

  
