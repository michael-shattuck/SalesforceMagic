# Salesforce Magic

While dealing with Salesforce I realized that there aren't any decent libraries that can communicate using the various APIs. This library is intended to be a simple, solid, and small library that encompasses and provides access to the SOAP, REST and Bulk API's for Salesforce. The magic lies in the library's ability to dynamically generate SOQL queries based on generic types and also the dynamic generation of the SOAP wsdl. You will never have to replace or update a wsdl again.

### Installation ###
[https://www.nuget.org/packages/SalesforceMagic/](Nuget) => PM> Install-Package SalesforceMagic

### Currently Implemented Features ###
* Dynamic SOQL query generation based on generic type interpretation
* Expression and predicate visitation allowing users to create both simple and complicated where conditions using powerful lambda expressions.
* SOAP Login and Session ID Retrieval
* SOAP Query based on generic types.
* Re-implementable storage for reusing session details.

### Example Usage ###

Start by setting up the configuration and Salesforce client:

```csharp
public void Main()
{
    var config = new SalesforceConfig
    {
        Username = "salesforceUsername",
        Password = "salesforcePassword",
        IsSandbox = true
    }

    using (SalesforceClient client = new SalesforceClient(config))
    {
        // Salesforce logic
    }     
}
```

Let's grab the session id:

```csharp
string sessionId = client.GetSessionId();
```

We can use the session id if we want, but the method automatically uses the last session id retrieved as long as it is not stale. For example, let's grab 5 vAttachments. We'll start by creating a C# class to represent out vAttachment. SalesforceMagic provides custom attributes that allow you to have pretty and simple classes while mapping to custom field names in Salesforce:

```csharp
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

```csharp
var attachments = client.Query<vAttachment>(x => x.S3Id != null, limit: 5);
```

Let's also go over the use of CRUD operations using both the SOAP and Bulk apis.
First let's create a list of objects we can use.

```csharp
vAttachment[] attachments = new []
{
    new vAttachment
    {
        FileName = "Test.pdf",
        S3Id = "123456789"
    },
    new vAttachment
    {
        FileName = "Test-2.pdf",
        S3Id = "abcdefghij"
    },
    ...
}
```

SOAP API: Using the SOAP api for CRUD operations is very simple. It can easily deal with individual sObjects or an array.

```csharp
SalesforceResponse response = client.PerformCrudOperation(attachments, CrudOperations.Insert);
```

BULK API: Interaction with the bulk api is slightly different. In order to use the bulk api you'll need to start a data load job:

```csharp
JobInfo jobInfo = client.CreateBulkJob<vAttachment>(new JobConfig
{
    ConcurrencyMode = ConcurrencyMode.Parallel,
    Operation = BulkOperations.Insert
});
```

Starting a job will return a JobInfo object:

```csharp
public class JobInfo
{
    public string Id { get; set; }
    public string Operation { get; set; }
    public string Object { get; set; }
    public string CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
    public JobState State { get; set; }
    public ConcurrencyMode ConcurrencyMode { get; set; }
    public string ContentType { get; set; }
    public int NumberBatchedQueued { get; set; }
    public int NumberBatchedInProgress { get; set; }
    public int NumberBatchedCompleted { get; set; }
    public int NumberBatchedFailed { get; set; }
    public int NumberBatchedTotal { get; set; }
    public int NumberBatchedProcessed { get; set; }
    public int NumberRetries { get; set; }
    public string ApiVersion { get; set; }
}
```

The most important part of the object is the Id. This is used to add batches to the job:

```csharp
BatchInfo batchInfo = client.AddBatch(attachments, jobInfo.Id);
```

This will queue a batch operation in the specified job. It returns a BatchInfo object:

```csharp
public class BatchInfo
{
    public string Id { get; set; }
    public string JobId { get; set; }
    public string State { get; set; }
    public DateTime CreatedDate { get; set; }
    public int NumberRecordsProcessed { get; set; }
}
```

Once you have added the necessary batches to the job you will need to close it:

```csharp
SalesforceResponse jobCloseResponse = client.CloseBulkJob(jobInfo.Id);
```

That's it! This will return an IEnumerable of 5 vAttachment objects. You can add more fields, remove fields, add more complex conditions. The sky is the limit!
