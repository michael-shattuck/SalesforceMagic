# Salesforce Magic

While dealing with Salesforce I realized that there aren't any decent libraries that can communicate using the various APIs. This library is intended to be a simple, solid, and small library that encompasses and provides access to the SOAP, REST and Bulk API's for Salesforce. The magic lies in the library's ability to dynamically generate SOQL/SOSL queries based on generic types and also the dynamic generation of the SOAP wsdl. You will never have to replace or update a wsdl again.

### Installation
[Nuget](https://www.nuget.org/packages/SalesforceMagic) => PM> Install-Package SalesforceMagic

### Documentation
* [Getting Started](https://github.com/clamidity/SalesforceMagic/wiki/Getting-Started)
* [Setting up DTOs](https://github.com/clamidity/SalesforceMagic/wiki/Setting-up-DTOs)
* [Using the SOAP API](https://github.com/clamidity/SalesforceMagic/wiki/Using-the-SOAP-API)
* [Using the Bulk API](https://github.com/clamidity/SalesforceMagic/wiki/Using-the-Bulk-API)


#### Currently Implemented Features
* Dynamic SOQL/SOSL query generation based on generic type interpretation
* Expression and predicate visitation allowing users to create both simple and complicated where conditions using powerful lambda expressions.
* SOAP Login and Session ID Retrieval (Can also auth using OAuth 2 access token)
* SOAP Query/Search based on generic types.
* SOAP Retrieve, Insert, Upsert, Update, and Delete based on generic types.
* Re-implementable storage for reusing session details.
