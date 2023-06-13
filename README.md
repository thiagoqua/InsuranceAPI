## The previous commits releated to the API are [here](https://github.com/thiagoqua/InsuranceWebApp/tree/aa9c2b126a7dc63996c4da5485a18d15355887a5)

## Structure of the Excel file to parse
| LICENSE[^5] | FOLDER | LIFE | CLIENT | BORN | ADDRESS | STATE | VTO | CITY | DNI | PHONES[^6] *description* | DESCRIPTION | CUIT | PRODUCER
| ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | ------- | 
| string | number or !SIN CARPETA | dd/mm-dd/mm | lastnames firstname *!(string)[^1]* | dd/mm/yyyy | street number *!P[^2] number* *!DTO[^3] number* | ACTIVA or ANULADA or EN JUICIO | number | string | !DNI number or LE number | number or number !(string)[^4] | *string* | *string* | TIKI or RICARDO or ZURDO or LEO
[^1]: the string between braces is the insured policy
[^2]: indicates the floor
[^3]: indicates the departament
[^4]: the string between braces is the description of the number phone
[^5]: the fill background color of this cell determines in which company the person is insured. It always need to be a color. In the case that the cell has blank color, verify that the background color is setted to <mark>white</mark> and it is not setted to <mark>no fill</mark>
[^6]: if there are more than one phone for an insured, they have to be separated by '/'.
The *italized text* means that that property is optional
The parts where **!** appears means that the parser expect literally that/those character/s.