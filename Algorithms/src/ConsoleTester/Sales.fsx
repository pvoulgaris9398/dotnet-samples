open System

// Sample data - simple sales records
type SalesRecord = { Date: DateTime; Product: string; Amount: decimal; Region: string }

// Sample dataset
let sales = [
    { Date = DateTime(2023, 1, 15); Product = "Laptop"; Amount = 1200m; Region = "North" }
    { Date = DateTime(2023, 2, 3);  Product = "Phone";  Amount = 800m;  Region = "South" }
    { Date = DateTime(2023, 1, 20); Product = "Tablet"; Amount = 400m;  Region = "North" }
    { Date = DateTime(2023, 2, 18); Product = "Laptop"; Amount = 1250m; Region = "East" }
    { Date = DateTime(2023, 1, 5);  Product = "Phone";  Amount = 750m;  Region = "West" }
    { Date = DateTime(2023, 2, 12); Product = "Tablet"; Amount = 450m;  Region = "North" }
    { Date = DateTime(2023, 1, 28); Product = "Laptop"; Amount = 1150m; Region = "South" }
]

// Quick analysis pipeline
let salesSummary =
    sales
    |> List.groupBy (fun s -> s.Product)                          // Group by product
    |> List.map (fun (product, items) ->                          // Transform each group
        let totalSales = items |> List.sumBy (fun s -> s.Amount)
        let avgSale = totalSales / decimal (List.length items)
        let topRegion =
            items
            |> List.groupBy (fun s -> s.Region)                   // Nested grouping
            |> List.maxBy (fun (_, regionItems) ->
                regionItems |> List.sumBy (fun s -> s.Amount))
            |> fst

        (product, totalSales, avgSale, topRegion))
    |> List.sortByDescending (fun (_, total, _, _) -> total)      // Sort by total sales

// Display results
salesSummary
|> List.iter (fun (product, total, avg, region) ->
    printfn "%s: $%M total, $%M avg, top region: %s"
        product total avg region)