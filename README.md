## SmartProcCaller

Offers a way to read data from a data source in to a data set and, for each item in that data set, execute a stored procedure passing the data to its parameters. 

The .net classes DataTable and DataRow (found in system.data namespace) are used extensively as a way to pass data from one class/property/method to another. This allows for passing unspecific sets of data/types and the receiving code can check it contains the required data using the DataTable column name property as a reference to particular data.

“SmartProcCaller.SpecReader” namespace contains classes for reading specifications for the data to be read and the procedure to be called.

“SmartProcCaller.DataReader” namespace contains classes for reading data from a data source based of specifications that are passed in.

“SmartProcCaller.Procedure” namespace contains classes that define and perform functions for a procedure that the retrieved data it to be passed to for execution.

the 3 logical units of functionality in the above namespacse are accessed using interfaces for each wich allows different types of each to be utilised in the same way. each of the 3 contain a "...builer" static class that all have buil methods which take a key value a return a new instance of the object relating to the key as the relevant interface.

"SmartProcCaller.Common" namespace contains classes used classes across multiple namespaces in the application.
Whithin the Common namespace there are the classes for the External Data functionality which includes:
1)ExternDataAttribute field attribute that can be used to attribute fields in any type that are to have data passed to them from a datarow object.  
2)IExterData interface that is to be implemeted by any type that contains fields attributted with the above ExterDataAttribute.  
3)GetExternValsFromDataRow extention method that allows any type that has implemented the IExterData interface to pass in a data row and for each field in the type that are Attributed with the ExterDataAttribute a value is searched for in the data row and concerted to the field type. An error is thrown if either no value is found in the data row or the value found could not be converted to the correct type.
 