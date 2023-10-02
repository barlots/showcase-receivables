## Showcase project with Receivables
For recrutation purposes.

Overall working time: ~3.5h


Application should create database and apply migrations automatically on start.

#### Built using:
* .NET 7
* EntityFramework Core 7
* MediatR
* SQLite
* NUnit, Moq


#### Endpoints:
* `POST https://localhost:7176/receivables`
> Adding data with given structure
> ```
>[ 
>  {
>    "reference": "INV12329",
>    "currencyCode": "USD",
>    "issueDate": "2023-09-21",
>    "openingValue": 12333.56,
>    "paidValue": 12333.56,
>    "dueDate": "2023-10-21",
>    "closedDate": "2023-10-15",
>    "cancelled": false,
>    "debtorName": "John Doe",
>    "debtorReference": "JD2023",
>    "debtorAddress1": "123 Elm St",
>    "debtorAddress2": "Apt 4B",
>    "debtorTown": "Springfield",
>    "debtorState": "IL",
>    "debtorZip": "62701",
>    "debtorCountryCode": "US",
>    "debtorRegistrationNumber": "REG123456"
>  },
>]

* `GET https://localhost:7176/receivables/statistics`
> Obtaining statistics with value of open/closed invoices and total amount of invoices in database. 
> Value of invoices is  grouped by currency

#### Notes & possible improvements:
* Application designed and written with the assumption there would be more business logic around `Receivable`
* SQLite doesnt support SUM of `decimal` in database, so its done in memory
* EF could be replaced with Dapper for more performant statistics query
* Transactions could be in command handler decorator
* Separate class for controler requests (currently we're using command related classes directly)
* Time is NOT ignored in requests date fields
* Prettier HTTP responses for unhappy path scenarios