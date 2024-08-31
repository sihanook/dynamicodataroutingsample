# Dynamic OData Routing Sample with custom response type

This sample project currently produces responses like this:

- /api/one
- /api/two
- /api/one?$filter=id eq 2&$select=id,name&$count=true

The current API response looks some like below:

```json
{
  "@odata.context": "http://localhost/api/$metadata#one(Id,Name)",
  "@odata.count": 1,
  "value": [
    {
      "Id": "2",
      "Name": "My name 2"
    }
  ]
}
```

The goal for this project is to make it produce responses like below to the same routes as above:

```json
{
  "@odata.context": "http://localhost/api/$metadata#one(Id,Name)",
  "@odata.count": 1,
  "value": {
    "content": [
      {
        "Id": "2",
        "Name": "My name 2"
      }
    ],
    "additionalmetadata": {
      "someProperty1": "someValue 1",
      "someProperty2": "someValue 2"
    }
  }
}
```

Note that the "additionalmetadata" property is a dynamically generated property that is not part of the EDM Model.
