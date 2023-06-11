namespace Sandbox.Aws.Repositories;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Sandbox.Library.FSharp.Dtos;

internal class CustomerRepository
{
    private const string TableName = "customers";
    private readonly IAmazonDynamoDB dynamoDb;

    public CustomerRepository(IAmazonDynamoDB dynamoDb)
    {
        this.dynamoDb = dynamoDb;
    }

    public async Task<bool> CreateAsync(CustomerDto customer)
    {
        var customerAsJson = JsonSerializer.Serialize(customer);
        var customerAsAttributes = Document.FromJson(customerAsJson).ToAttributeMap();

        var createItemRequest = new PutItemRequest
        {
            TableName = TableName,
            Item = customerAsAttributes,
            ConditionExpression = "attribute_not_exist(pk) and attribute_not_exist(sk)",
        };

        var response = await this.dynamoDb.PutItemAsync(createItemRequest);

        return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }

    public async Task<CustomerDto?> GetByIdAsync(Guid id)
    {
        var getItemRequest = new GetItemRequest
        {
            TableName = TableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } },
            },
        };

        var response = await this.dynamoDb.GetItemAsync(getItemRequest);

        if (response.Item.Count == 0)
        {
            return null;
        }

        var itemAsDocument = Document.FromAttributeMap(response.Item);
        return JsonSerializer.Deserialize<CustomerDto>(itemAsDocument.ToJson());
    }

    /// <summary>
    /// Retrieves the item by scanning the entire cluster. Highly discouraged. Very expensive.
    /// </summary>
    /// <returns>The retrieved Customers.</returns>
    public async Task<IEnumerable<CustomerDto>> GetAll()
    {
        var scanRequest = new ScanRequest
        {
            TableName = TableName,
            Limit = 10, // Scan max 10 items
        };

        var response = await this.dynamoDb.ScanAsync(scanRequest);

        return response.Items.Select(x =>
        {
            var json = Document.FromAttributeMap(x).ToJson();
            return JsonSerializer.Deserialize<CustomerDto>(json);
        });
    }

    public async Task<bool> UpdateAsync(CustomerDto customer, DateTime requestStarted)
    {
        customer.UpdatedAt = DateTime.UtcNow;
        var customerAsJson = JsonSerializer.Serialize(customer);
        var customerAsAttributes = Document.FromJson(customerAsJson).ToAttributeMap();

        var updateItemRequest = new PutItemRequest
        {
            TableName = TableName,
            Item = customerAsAttributes,
            ConditionExpression = "UpdatedAt < :requestStarted",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":requestStarted", new AttributeValue { S = requestStarted.ToString("O") } },
            },
        };

        var response = await this.dynamoDb.PutItemAsync(updateItemRequest);

        return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var deleteItemRequest = new DeleteItemRequest
        {
            TableName = TableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } },
            },
        };

        var response = await this.dynamoDb.DeleteItemAsync(deleteItemRequest);

        return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }
}
