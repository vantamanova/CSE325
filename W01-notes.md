# W01 Assignment Notes

## Part 1 - Web API

### GET Request
```http
HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Wed, 02 Sep 2026 08:18:13 GMT
Server: Kestrel
Transfer-Encoding: chunked

[
  {
    "id": 1,
    "name": "Classic Italian",
    "isGlutenFree": false
  },
  {
    "id": 2,
    "name": "Veggie",
    "isGlutenFree": true
  },
  {
    "id": 3,
    "name": "Margherita",
    "isGlutenFree": false
  }
]
```


### POST Request
```http
HTTP/1.1 201 Created
Connection: close
Content-Type: application/json; charset=utf-8
Date: Wed, 02 Sep 2026 08:22:45 GMT
Server: Kestrel
Location: http://localhost:5083/Pizza/4
Transfer-Encoding: chunked

{
  "id": 4,
  "name": "Hawaii",
  "isGlutenFree": false
}
```

### PUT Request
```http
HTTP/1.1 204 No Content
Connection: close
Date: Wed, 02 Sep 2026 08:23:20 GMT
Server: Kestrel
```

### GET Request
```http
HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Wed, 02 Sep 2026 08:23:57 GMT
Server: Kestrel
Transfer-Encoding: chunked

[
  {
    "id": 1,
    "name": "Classic Italian",
    "isGlutenFree": false
  },
  {
    "id": 2,
    "name": "Veggie",
    "isGlutenFree": true
  },
  {
    "id": 3,
    "name": "Margherita",
    "isGlutenFree": false
  },
  {
    "id": 4,
    "name": "Hawaiian",
    "isGlutenFree": false
  }
]
```

### DELETE Request
```http
HTTP/1.1 204 No Content
Connection: close
Date: Wed, 02 Sep 2026 08:24:41 GMT
Server: Kestrel
```

### GET Request
```http
HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Wed, 02 Sep 2026 08:25:10 GMT
Server: Kestrel
Transfer-Encoding: chunked

[
  {
    "id": 1,
    "name": "Classic Italian",
    "isGlutenFree": false
  },
  {
    "id": 2,
    "name": "Veggie",
    "isGlutenFree": true
  },
  {
    "id": 3,
    "name": "Margherita",
    "isGlutenFree": false
  }
]
```

## Part 2 - Sales Summary Function

```csharp
void GenerateSalesSummary(
    IEnumerable<string> salesFiles,
    double salesTotal,
    string salesTotalDir,
    string storesDirectory)
{
    StringBuilder report = new StringBuilder();

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {salesTotal:C}");
    report.AppendLine();
    report.AppendLine("Details:");

    foreach (var file in salesFiles)
    {
        // to remove salestotls
        if (Path.GetFileName(file) != "sales.json")
        {
            continue;
        }

        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        if (data != null)
        {
            string relativePath = Path.GetRelativePath(storesDirectory, file);
            report.AppendLine($"   {relativePath}: {data.Total:C}");
        }
    }

    File.WriteAllText(
        Path.Combine(salesTotalDir, "salesSummary.txt"),
        report.ToString()
    );
}
```