## The previous commits releated to the API are [here](https://github.com/thiagoqua/InsuranceWebApp/tree/aa9c2b126a7dc63996c4da5485a18d15355887a5)

## API's credentials 
`username`:`tiki`
<br/>
`password`:`admin`

# API overall explanation
The API has 5 controllers:
- The Insured controller
- The Company controller
- The Authentication controller
- The File controller
- The Producer controller

Unless the Authentication controller, the rest of them require a **Bearer Token authentication** to be accesed.
The function of each one is nested to its name, and you can see and test each endpoint from them using accessing the SwaggerUI url: `<host>:<port>/swagger/index.html`.

# Run guide
## API with DotNet desktop & database with Docker
1. go to the InsuranceDB directory
2. build the container: `docker build -t database:test .`
3. run the container: `docker run -p 1433:1433 --name insurance-db -d database:test`
4. go to the InsuranceAPI directory and run install the needed dependencies: `dotnet restore`
5. run the project: `dotnet run`

## the hole API in Docker with Docker-Compose, incluiding the database
1. go to the main directory
2. run the app: `docker-compose up`

## Structure of the Excel file to parse
| LICENSE | COMPANY |FOLDER | LIFE | CLIENT | BORN | ADDRESS | STATE | VTO | CITY | DNI | PHONES[^6] *description* | DESCRIPTION | CUIT | PRODUCER
| ------- | ------- |------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | 
| string | string[^5] | number | dd/mm-dd/mm | lastnames firstname *!(string)[^1]* | dd/mm/yyyy | street number *!P[^2] number* *!DTO[^3] number* | ACTIVA or ANULADA or EN JUICIO | number | string | !DNI number or LE number | number or number !(string)[^4] | *string* | *string* | string[^7]
[^1]: the string between braces is the insured policy
[^2]: indicates the floor
[^3]: indicates the departament
[^4]: the string between braces is the description of the number phone
[^5]: an abbreviation of the company's name. in the case, could be 'COOP' or 'FEDPAT'.
[^6]: if there are more than one phone for an insured, they have to be separated by '/'.
[^7]: this string contains the producer's name, and it needs to match with one of the corresponding producers stored in the database.
The *italized text* means that that property is optional
The parts where **!** appears means that the parser expect literally that/those character/s.
