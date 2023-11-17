
# Pet Request

## Structure

`PetRequest`

## Fields

| Name | Type | Tags | Description |
|  --- | --- | --- | --- |
| `Id` | `long?` | Optional | - |
| `Category` | [`Category1`](../../doc/models/category-1.md) | Optional | - |
| `Name` | `string` | Required | - |
| `PhotoUrls` | `List<string>` | Required | - |
| `Tags` | [`List<Tag>`](../../doc/models/tag.md) | Optional | - |
| `Status` | [`StatusEnum?`](../../doc/models/status-enum.md) | Optional | - |

## Example (as JSON)

```json
{
  "id": 198,
  "category": {
    "id": 232,
    "name": "name2"
  },
  "name": "name4",
  "photoUrls": [
    "photoUrls9"
  ],
  "tags": [
    {
      "id": 26,
      "name": "name0"
    },
    {
      "id": 26,
      "name": "name0"
    },
    {
      "id": 26,
      "name": "name0"
    }
  ],
  "status": "pending"
}
```

