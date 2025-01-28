
//create a "products" variable here to include at least five Product instances. Give them appropriate ProductTypeIds.
List<Product> products = new List<Product>()
{
    new Product()
    {
        Name = "Trumpet",
        Price = 350.00M,
        ProductTypeId = 2
    },
    new Product()
    {
        Name = "Trombone",
        Price = 500.00M,
        ProductTypeId = 2
    },
    new Product()
    {
        Name = "French Horn",
        Price = 750.00M,
        ProductTypeId = 2
    },
    new Product()
    {
        Name = "Euphonium",
        Price = 450.00M,
        ProductTypeId = 2
    },
    new Product()
    {
        Name = "Tuba",
        Price = 1500.00M,
        ProductTypeId = 2
    }
};


//create a "productTypes" variable here with a List of ProductTypes, and add "Brass" and "Poem" types to the List. 
List<ProductType> productTypes = new List<ProductType>()
{
    new ProductType()
    {
        Title = "Poem",
        Id = 1
    },
    new ProductType()
    {
        Title = "Brass",
        Id = 2
    },
};


//put your greeting here
string greeting = @"Welcome to Brass&Poem!
Where I definitely have more brass than poem";

Console.WriteLine(greeting);


//implement your loop here
string choice = null;
while (choice != "0")
{
    DisplayMenu();
        choice = Console.ReadLine();
   
   if (choice == "1")
    {
        DisplayAllProducts(products, productTypes);
    }
    else if (choice == "2")
    {
        DeleteProduct(products, productTypes);
    }
    else if (choice == "3")
    {
        AddProduct(products, productTypes);
    }
    else if (choice == "4")
    {
        UpdateProduct(products, productTypes);
    }
    else if (choice == "5")
    {
        Console.WriteLine("Goodbye!");
        break;
    }
}
void DisplayMenu()
{
    Console.WriteLine(@"Choose an option:
                        1. Display all products
                        2. Delete a product
                        3. Add a new product
                        4. Update product properties
                        5. Exit");
}
void DisplayAllProducts(List<Product> products, List<ProductType> productTypes)
{
    if (products.Count == 0)
    {
        Console.WriteLine("No products available.");
    }
    else
    {
        for (int i = 0; i < products.Count; i++)
        {
            var product = products[i];

            var productType = productTypes.FirstOrDefault(pt => pt.Id == product.ProductTypeId);

            string productTypeTitle = productType != null ? productType.Title : "Unknown Type";

            Console.WriteLine($"{i + 1}. {product.Name} - {product.Price:C} ({productTypeTitle})");
        }
    }
}


void DeleteProduct(List<Product> products, List<ProductType> productTypes)
{
    DisplayAllProducts(products, productTypes);

    Console.WriteLine("Enter the number of the product you want to delete:");

    string input = Console.ReadLine();
    if (int.TryParse(input, out int index))
    {
        index = index - 1;

        if (index >= 0 && index < products.Count)
        {
            products.RemoveAt(index);

            Console.WriteLine("Product deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid product number. Please enter a valid index.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
}


void AddProduct(List<Product> products, List<ProductType> productTypes)
{
    Console.WriteLine("Enter product name:");
    string productName = Console.ReadLine();

    decimal productPrice = 0;
    bool validPrice = false;
    while (!validPrice)
    {
        Console.WriteLine("Enter the price of the new product:");
        string priceInput = Console.ReadLine();

        if (decimal.TryParse(priceInput, out productPrice) && productPrice > 0)
        {
            validPrice = true;
        }
        else
        {
            Console.WriteLine("Invalid price. Please enter a valid value.");
        }
    }

    Console.WriteLine("Select a product type by entering the corresponding number:");
    for (int i = 0; i < productTypes.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {productTypes[i].Title}");
    }

    int productTypeId = 0;
    bool validProductType = false;
    while (!validProductType)
    {
        string typeInput = Console.ReadLine();

        if (int.TryParse(typeInput, out productTypeId) && productTypeId >= 1 && productTypeId <= productTypes.Count)
        {
            validProductType = true;
            productTypeId = productTypes[productTypeId - 1].Id;
        }
        else
        {
            Console.WriteLine("Invalid selection. Please choose a valid product type number.");
        }
    }

    Product newProduct = new Product
    {
        Name = productName,
        Price = productPrice,
        ProductTypeId = productTypeId
    };

    products.Add(newProduct);

    Console.WriteLine($"The product '{newProduct.Name}' was added successfully.");
}


void UpdateProduct(List<Product> products, List<ProductType> productTypes)
{
    DisplayAllProducts(products, productTypes);

    Console.WriteLine("Enter the number of the product you want to update:");

    string input = Console.ReadLine();
    if (int.TryParse(input, out int index))
    {
        if (index >= 0 && index < products.Count)
        {
            Product selectedProduct = products[index];

            Console.WriteLine($"Current details of {selectedProduct.Name}:");
            Console.WriteLine($"Name: {selectedProduct.Name}");
            Console.WriteLine($"Price: {selectedProduct.Price:C}");
            Console.WriteLine($"Product Type: {productTypes.FirstOrDefault(pt => pt.Id == selectedProduct.ProductTypeId)?.Title}");

            Console.WriteLine("Do you want to update the name? (yes/no)");
            string updateName = Console.ReadLine();
            if (updateName.ToLower() == "yes")
            {
                Console.WriteLine("Enter the new name:");
                selectedProduct.Name = Console.ReadLine();
            }

            Console.WriteLine("Do you want to update the price? (yes/no)");
            string updatePrice = Console.ReadLine();
            if (updatePrice.ToLower() == "yes")
            {
                decimal newPrice = 0;
                bool validPrice = false;

                while (!validPrice)
                {
                    Console.WriteLine("Enter the new price:");
                    string priceInput = Console.ReadLine();

                    if (decimal.TryParse(priceInput, out newPrice) && newPrice > 0)
                    {
                        selectedProduct.Price = newPrice;
                        validPrice = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid price. Please enter a valid positive decimal value.");
                    }
                }
            }

            Console.WriteLine("Do you want to update the product type? (yes/no)");
            string updateType = Console.ReadLine();
            if (updateType.ToLower() == "yes")
            {
                Console.WriteLine("Select a new product type by entering the corresponding number:");
                for (int i = 0; i < productTypes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {productTypes[i].Title}");
                }

                int newTypeId = 0;
                bool validProductType = false;
                while (!validProductType)
                {
                    string typeInput = Console.ReadLine();

                    if (int.TryParse(typeInput, out newTypeId) && newTypeId >= 1 && newTypeId <= productTypes.Count)
                    {
                        validProductType = true;
                        selectedProduct.ProductTypeId = productTypes[newTypeId - 1].Id;
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please choose a valid product type number.");
                    }
                }
            }
        }
 
    }
}



// don't move or change this!
public partial class Program { }
