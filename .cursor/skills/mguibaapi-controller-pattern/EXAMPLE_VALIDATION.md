# Example validation (no code changes)

Goal: Validate the templates in `SKILL.md` can produce controllers consistent with `Controllers/PATTERN/*`.

## Example requirement
Create a new authenticated controller named `RoomTypesController` under `Controllers/HTLPRE` that supports:

1) `GET helpv2/pages/{pageNo}` with `queryText` + `sortByName`
2) `POST query/{roomTypeIds}/{roomTypeIde}/pages/{pageNo}` with body `string[] statuses`
3) CRUD:
   - `POST` insert `MdHTLRoomType`
   - `PUT {roomTypeId}` update `MdHTLRoomType` and validate route key equals body key
   - `DELETE {roomTypeId}` delete

## Generated design (using templates)

- Base class: `GUIAppAuthController`
- Route: `[Route("htlpre/[controller]")]` (domain controllers typically do not use `pattern/` prefix; only PATTERN examples do)
  - If you intend it to be a PATTERN demo controller, use `[Route("pattern/[controller]")]`.

### Endpoints (shape)
- `GET htlpre/roomtypes/helpv2/pages/{pageNo}?queryText=...&sortByName=true`
- `POST htlpre/roomtypes/query/{roomTypeIds}/{roomTypeIde}/pages/{pageNo}` with JSON body `string[]`
- `POST htlpre/roomtypes`
- `PUT htlpre/roomtypes/{roomTypeId}`
- `DELETE htlpre/roomtypes/{roomTypeId}`

### Response conventions
- Use `HttpContext.Response.InsertSuccess/InsertFailed`
- Use `HttpContext.Response.UpdateSuccess/UpdateFailed`
- Use `HttpContext.Response.DeleteSuccess/DeleteFailed`
- Key mismatch: `UpdateFailedWhenKeyNotSame()`

## Notes
- The templates in the skill cover helpv2/query/CRUD/report/private-paged patterns.
- For domain controllers (e.g. `HTLPRE`), the only adaptation usually needed is the route prefix; the action patterns remain the same.
