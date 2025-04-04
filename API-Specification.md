# FlowerShop API Specification

## API Information
- **Name**: FlowerShopAPI
- **Version**: 1.0
- **Description**: FlowerShopAPI is a RESTful API built with ASP.NET Core to manage customers, orders and products for an online flower shop. The API uses Entity Framework Core for database management.
- **Contact Information**: krystal.tt.nguyen@gmail.com

<br>

## Customer Endpoints

### Get All Customers
- **HTTP Method:** `GET`
- **Route:** `/api/customers`
- **Responses:**
  - **200 OK**: Returns a list of customers in the form of `CustomerDto`.

#### Example Request:
GET /api/customers

#### Example Response:
```
[
  {
    "id": 1,
    "firstName": "Anna",
    "lastName": "Johansson",
    "email": "anna.johansson@example.com",
    "phoneNumber": "+46 70 123 45 67",
    "streetAddress": "Sveavägen 12, Stockholm"
  },
  {
    "id": 2,
    "firstName": "Björn",
    "lastName": "Nilsson",
    "email": "bjorn.nilsson@example.com",
    "phoneNumber": "+46 70 234 56 78",
    "streetAddress": "Vasagatan 5, Göteborg"
  }
]
```

<br>

---

### Get Customer By ID
- **HTTP Method:** `GET`
- **Route:** `/api/customers{id}`
- **Responses:**
  - **200 OK**: Returns customer details in the form of `CustomerDto`.
  - **404 Not Found**: Returned when the specified customer does not exist.

#### Example Request:
GET /api/customers{1}

#### Example Response:
```
{
  "id": 1,
  "firstName": "Anna",
  "lastName": "Johansson",
  "email": "anna.johansson@example.com",
  "phoneNumber": "+46 70 123 45 67",
  "streetAddress": "Sveavägen 12, Stockholm"
}
```
<br>

---

### Get Customer By Email
- **HTTP Method:** `GET`
- **Route:** `/api/customers/email/{email}`
- **Responses:**
  - **200 OK**: Returns customer details in the form of `CustomerDto`.
  - **404 Not Found**: Returned when the specified customer does not exist.

#### Example Request:
GET /api/customers{jane.doe@example.com}

#### Example Response:
```
{
  "id": 4,
  "firstName": "Jane",
  "lastName": "Doe",
  "email": "jane.doe@example.com",
  "phoneNumber": "(+46) 123 456 78",
  "streetAddress": "Ebbe Lieberathsgatan 18C, Göteborg"
}
```
<br>

---

### Create New Customer
- **HTTP Method:** `POST`
- **Route:** `/api/customers`
- **Responses:**
  - **201 Created**: Returned when a new customer is successfully created. Return body contains a `CreateCustomerDto`. 
  - **400 Bad Request**: Returned when the request is invalid or contains incorrect data.
  - **409 Conflict**: Returned when a customer with the same email already. exists. 

#### Example Request:
POST /api/customers

Request body: application/json
```
{
    "firstName": "Carla",
    "lastName": "Eriksson",
    "email": "carla.eriksson@example.com",
    "phoneNumber": "+46 70 345 67 89",
    "streetAddress": "Götgatan 30, Malmö"
}
```
<br>

#### Example Response:
```
{
    "id": 3,
    "firstName": "Carla",
    "lastName": "Eriksson",
    "email": "carla.eriksson@example.com",
    "phoneNumber": "+46 70 345 67 89",
    "streetAddress": "Götgatan 30, Malmö"
}
```
<br>

---

### Update Customer
- **HTTP Method:** `PUT`
- **Route:** `/api/customers/{id}`
- **Responses:**
  - **200 OK**: Returned when the customer information is successfully updated. Return body contains a `UpdateCustomerDto`. 
  - **400 Bad Request**: Returned when the request is invalid or contains incorrect data.
  - **404 Not Found**: Returned when the customer with the specified id does not exist in the system.

#### Example Request:
PUT /api/customers/{2} - eg. Change street address

Request body: application/json
```
{
    "firstName": "Björn",
    "lastName": "Nilsson",
    "email": "bjorn.nilsson@example.com",
    "phoneNumber": "+46 70 234 56 78",
    "streetAddress": "Random adress, Göteborg"
}
```

#### Example Response:
```
{
    "id": 2,
    "firstName": "Björn",
    "lastName": "Nilsson",
    "email": "bjorn.nilsson@example.com",
    "phoneNumber": "+46 70 234 56 78",
    "streetAddress": "Vasagatan 5, Göteborg"
}
```
<br>


## Order Endpoints

### Get All Orders
- **HTTP Method:** `GET`
- **Route:** `/api/orders`
- **Responses:**
  - **200 OK**: Returned when the list of all orders is successfully retrieved. The response body contains a list of all orders and orderdetails in the system in form of `OrderDto` and `OrderItemDto`. . 
  - **404 Not Found**: Returned if there are no orders in the system.

#### Example Request:
GET /api/orders

#### Example Response:
```
[
  {
    "id": 1000,
    "customerId": 1,
    "orderDate": "2025-03-21T00:27:55.39",
    "orderStatus": "Pending",
    "customer": {
      "id": 1,
      "firstName": "Anna",
      "lastName": "Johansson",
      "email": "anna.johansson@example.com",
      "phoneNumber": "+46 70 123 45 67",
      "streetAddress": "Sveavägen 12, Stockholm"
    },
    "orderItems": [
      {
        "id": 2042,
        "productId": 101,
        "productName": "Tulips",
        "quantity": 10,
        "unitPrice": 29.99,
        "totalPrice": 299.9
      },
      {
        "id": 2043,
        "productId": 103,
        "productName": "Freesias",
        "quantity": 7,
        "unitPrice": 24.99,
        "totalPrice": 174.93
      }
    ]
  },
  {
    "id": 1023,
    "customerId": 4,
    "orderDate": "2025-03-24T01:50:45.633",
    "orderStatus": "Pending",
    "customer": {
      "id": 4,
      "firstName": "Jane",
      "lastName": "Doe",
      "email": "jane.doe@example.com",
      "phoneNumber": "(+46) 123 456 78",
      "streetAddress": "Ebbe Lieberathsgatan 18C, Göteborg"
    },
    "orderItems": [
      {
        "id": 2057,
        "productId": 115,
        "productName": "Snake Plant",
        "quantity": 2,
        "unitPrice": 119.99,
        "totalPrice": 239.98
      }
    ]
  }
]
```
<br>

---

### Get Orders By Customer ID
- **HTTP Method:** `GET`
- **Route:** `/api/orders/customer/{customerId}`
- **Responses:**
  - **200 OK**: Returned when the list of `OrderDto` for the specified customer is successfully retrieved. 
  - **404 Not Found**: Returned if no orders are found for the specified customerId.

#### Example Request:
GET /api/orders/customer/{1}

#### Example Response:
```
[
  {
    "id": 1000,
    "customerId": 1,
    "orderDate": "2025-03-21T00:27:55.39",
    "orderStatus": "Pending",
    "customer": {
      "id": 1,
      "firstName": "Anna",
      "lastName": "Johansson",
      "email": "anna.johansson@example.com",
      "phoneNumber": "+46 70 123 45 67",
      "streetAddress": "Sveavägen 12, Stockholm"
    },
    "orderItems": [
      {
        "id": 2042,
        "productId": 101,
        "productName": "Tulips",
        "quantity": 10,
        "unitPrice": 29.99,
        "totalPrice": 299.9
      },
      {
        "id": 2043,
        "productId": 103,
        "productName": "Freesias",
        "quantity": 7,
        "unitPrice": 24.99,
        "totalPrice": 174.93
      }
    ]
  },
  {
    "id": 1030,
    "customerId": 1,
    "orderDate": "2025-04-04T18:44:44.727",
    "orderStatus": "Pending",
    "customer": {
      "id": 1,
      "firstName": "Anna",
      "lastName": "Johansson",
      "email": "anna.johansson@example.com",
      "phoneNumber": "+46 70 123 45 67",
      "streetAddress": "Sveavägen 12, Stockholm"
    },
    "orderItems": [
      {
        "id": 2067,
        "productId": 113,
        "productName": "Spider Plant",
        "quantity": 2,
        "unitPrice": 89.99,
        "totalPrice": 179.98
      }
    ]
  }
]
```
<br>

---

### Create New Order
- **HTTP Method:** `POST`
- **Route:** `/api/orders`
- **Responses:**
  - **201 Created**: Returned when a new order is successfully created. Return body contains a `OrderDto`. 
  - **400 Bad Request**: Returned when the request is invalid or contains incorrect data. This could be due to invalid customerId or incorrect order items.

#### Example Request:
POST /api/orders

Request body: application/json
```
{
  "customerId": 4,
  "orderDate": "2025-04-04T18:36:43.201Z",
  "orderItems": [
    {
      "productId": 115,
      "quantity": 2
    }
  ]
}
```

#### Example Response:
```
[
  {
    "id": 1023,
    "customerId": 4,
    "orderDate": "2025-03-24T01:50:45.633",
    "orderStatus": "Pending",
    "customer": {
      "id": 4,
      "firstName": "Jane",
      "lastName": "Doe",
      "email": "jane.doe@example.com",
      "phoneNumber": "(+46) 123 456 78",
      "streetAddress": "Ebbe Lieberathsgatan 18C, Göteborg"
    },
    "orderItems": [
      {
        "id": 2057,
        "productId": 115,
        "productName": "Snake Plant",
        "quantity": 2,
        "unitPrice": 119.99,
        "totalPrice": 239.98
      }
    ]
  }
]
```
<br>

## Product Categories Endpoints

### Get Product Categories
- **HTTP Method:** `GET`
- **Route:** `/api/categories`
- **Responses:**
  - **200 OK**: Returns a list of categories in the form of `ProductCategoriesDto`.

#### Example Request:
GET /api/categories

#### Example Response:
```
[
  {
    "id": 1,
    "categoryName": "Flowers"
  },
  {
    "id": 2,
    "categoryName": "Green Plants"
  },
  {
    "id": 3,
    "categoryName": "Floral Arrangements"
  },
  {
    "id": 4,
    "categoryName": "Flower Pots and Vases"
  },
  {
    "id": 5,
    "categoryName": "Unknown"
  }
]
```
<br>


## Product Endpoints

### Get All Products
- **HTTP Method:** `GET`
- **Route:** `/api/products`
- **Responses:**
  - **200 OK**: Returns a list of products in the form of `ProductDto`.

#### Example Request:
GET /api/products

#### Example Response:
```
[
  {
    "id": 101,
    "productNumber": "P1002",
    "productName": "Tulips",
    "description": "Fresh tulips available in a variety of colors for springtime occasions.",
    "price": 29.99,
    "stockQuantity": 150,
    "productCategoryId": 1,
    "productCategoryName": "Flowers",
    "isDiscontinued": false
  },
  {
    "id": 102,
    "productNumber": "P1003",
    "productName": "Lilies",
    "description": "Elegant lilies perfect for any formal occasion or as a decorative flower.",
    "price": 39.99,
    "stockQuantity": 100,
    "productCategoryId": 1,
    "productCategoryName": "Flowers",
    "isDiscontinued": true
  },
  {
    "id": 103,
    "productNumber": "P1004",
    "productName": "Freesias",
    "description": "Fragrant flowers in vibrant colors, ideal for weddings or bouquets.",
    "price": 24.99,
    "stockQuantity": 80,
    "productCategoryId": 1,
    "productCategoryName": "Flowers",
    "isDiscontinued": false
  }
]
```
<br>

---

### Get Product by ID
- **HTTP Method:** `GET`
- **Route:** `/api/products/{id}`
- **Responses:**
  - **200 OK**: Returns a product with specified ID in the form of `ProductDto`.
  - **404 Not Found**: Returned when no product is found for the specified id.

#### Example Request:
GET /api/products/{119}

#### Example Response:
```
{
  "id": 119,
  "productNumber": "P3002",
  "productName": "Birthday Bouquet",
  "description": "A cheerful bouquet of mixed flowers for a birthday celebration.",
  "price": 149.99,
  "stockQuantity": 50,
  "productCategoryId": 3,
  "productCategoryName": "Floral Arrangements",
  "isDiscontinued": false
}
```
<br>

---

### Create New Product
- **HTTP Method:** `POST`
- **Route:** `/api/products`
- **Responses:**
  - **201 Created**: Returned when a new product is successfully created. The response body is in the form `ProductDto` and contains the details of the newly created product.
  - **400 Bad Request**: Returned when the request is invalid, such as missing required fields (e.g., product name or price) or invalid data format.
  - **409 Conflict**: Returned when a product with the same product SKU already exists in the system, leading to a conflict.

#### Example Request:
POST /api/products

Request body: application/json
```
{
    "productNumber": "P1001",
    "productName": "Roses (Red)",
    "description": "Fresh red roses, ideal for gifting or home decoration.",
    "price": 49.99,
    "stockQuantity": 120,
    "productCategoryId": 1,
    "productCategoryName": "Flowers",
    "isDiscontinued": false
}
```

#### Example Response:
```
{
    "id": 133,
    "productNumber": "P1001",
    "productName": "Roses (Red)",
    "description": "Fresh red roses, ideal for gifting or home decoration.",
    "price": 49.99,
    "stockQuantity": 120,
    "productCategoryId": 1,
    "productCategoryName": "Flowers",
    "isDiscontinued": false
}
```
<br>

---

### Update Product
- **HTTP Method:** `PUT`
- **Route:** `/api/products/{id}`
- **Responses:**
  - **200 OK**: Returned when a product with the specified ID is successfully updated. The response body contains the updated product details.
  - **400 Bad Request**: Returned when the request is invalid, such as missing required fields or invalid input data.
  - **404 Not Found**: Returned if no product is found for the specified id.

#### Example Request:
PUT /api/products/{102} - eg. Mark product as Discontinued

Request body: application/json
```
{
    "id": 102,
    "productNumber": "P1003",
    "productName": "Lilies",
    "description": "Elegant lilies perfect for any formal occasion or as a decorative flower.",
    "price": 39.99,
    "stockQuantity": 100,
    "productCategoryId": 1,
    "productCategoryName": "Flowers",
    "isDiscontinued": false
}
```

#### Example Response:
```
{
    "id": 102,
    "productNumber": "P1003",
    "productName": "Lilies",
    "description": "Elegant lilies perfect for any formal occasion or as a decorative flower.",
    "price": 39.99,
    "stockQuantity": 100,
    "productCategoryId": 1,
    "productCategoryName": "Flowers",
    "isDiscontinued": true
}
```
<br>

---

### Delete Product
- **HTTP Method:** `DELETE`
- **Route:** `/api/products/{id}`
- **Responses:**
  - **204 No Content**: Returned when the product with the specified id is successfully deleted. The response body is empty
  - **404 Not Found**: Returned if no product is found for the specified id.

#### Example Request:
DELETE /api/products/{130}

#### Example Request:
{
  "error": "esponse status is 404",
  "message": "No product found with given ID: 130."
}
