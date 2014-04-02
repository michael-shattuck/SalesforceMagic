# Salesforce Magic

While dealing with Salesforce I realized that there aren't any decent libraries that can communicate using the various APIs. This library is intended to be a simple, solid, and small library that encompasses and provides access to the SOAP, REST and Bulk API's for Salesforce. The magic lies in the libraries ability to dynamically generate SOQL queries based on generic types and also the dynamic generation of the SOAP wsdl. You will never have to replace or update a wsdl again.

### Currently Implemented Features ###
* Dynamic SOQL query generation based on generic type interpretation
* Expression and predicate visitation allowing users to create both simple and complicated where conditions using powerful lambda expressions.
* SOAP Login and Session ID Retrieval
* SOAP Query based o generic types.

### Example Usage ###

Start by setting up the configuration and Salesforce client:

```
#!c#
public void Main()
{
    var config = new SalesforceConfig
    {
        Username = "corpsysapi@vivint.com.uat",
        Password = "PdzD1oPtJso3Jeo",
        IsSandbox = true
    }

    using (SalesforceClient client = new SalesforceClient(config))
    {
        // Salesforce logic
    }     
}
```

Let's grab the session id:

```
#!c#
string sessionId = client.GetSessionId();
```

We can use the session id if we want, but the method automatically uses the last session id retrieved as long as it is not stale. For example, let's grab 5 vAttachments. We'll start by creating a C# class to represent out vAttachment. SalesforceMagic provides custom attributes that allow you to have pretty and simple classes while mapping to custom field names in Salesforce:

```
#!c#
[SalesforceName("vAttachment__c")]
public class vAttachment : SObject
{
    public string Id { get; set; }

    [SalesforceName("Filename__c")]
    public string FileName { get; set; }

    [SalesforceName("s3Id__c")]
    public string S3Id { get; set; }
}
```

Now let's actually perform the query, we'll grab 5 vAttachments and set a condition to ensure that the records we retrieve have the S3Id field set:

```
#!c#
var attachments = client.Query<vAttachment>(x => x.S3Id != null, 5);
```

That's it! This will return an IEnumerable of 5 vAttachment objects. You can add more fields, remove fields, add more complex conditions. The sky is the limit!
