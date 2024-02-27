using Microsoft.AspNetCore.Http.HttpResults;

namespace todoAPI.Apis
{
    public static class TodoAPI
    {
        public static List<Todo> Todos = new List<Todo>(){
        new Todo(){Id = 1,Label ="Label 1",IsDone = true,CreatedDateTime = new DateTime(2024, 2, 26),UpdatedDate = new DateTime(2024, 2, 27)},
        new Todo(){Id = 2,Label ="Label 2",IsDone = false,CreatedDateTime = new DateTime(2024, 1, 26),UpdatedDate = new DateTime(2024, 1, 27)},
        new Todo(){Id = 3,Label ="Label 3",IsDone = false,CreatedDateTime = new DateTime(2024, 1, 20),UpdatedDate = new DateTime(2024, 1, 26)},
    };

        public static void Map(WebApplication app)
        {
            app.MapGet("/Todos", (int limit, int offset) =>
            {
                return Todos.OrderBy(t => t.Id).Skip(offset).Take(limit).ToList();
            });

            app.MapGet("/Todos/{id}", Results<Ok<Todo>, NotFound> (int id) =>
            {
                var todo = Todos.SingleOrDefault(t => t.Id == id);
                if (todo == null)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.Ok(todo);
            });

            app.MapGet("/Todos/{id}/IsDone", (int id) =>
            {
                var todo = Todos.SingleOrDefault(t => t.Id == id);
                if (todo == null)
                {
                    return TypedResults.NotFound();
                }
                return Results.Json(new { id = todo.Id, isDone = todo.IsDone });
            });

            app.MapPost("/Todos", (Todo todo) =>
            {
                todo.Id = Todos.Max(todo => todo.Id) + 1;
                todo.Label = "Label " + todo.Id;
                todo.IsDone = false;
                todo.CreatedDateTime = DateTime.UtcNow;
                todo.UpdatedDate = DateTime.UtcNow;
                Todos.Add(todo);
                return TypedResults.Created($"/Todos/{todo.Id}", todo);
            });

            app.MapPut("/Todos/{id}", Results<Ok<Todo>, NotFound> (int id, string label, bool isDone) =>
            {
                var todo = Todos.SingleOrDefault(t => t.Id == id);
                if (todo == null)
                {
                    return TypedResults.NotFound();
                }
                todo.Label = label;
                todo.IsDone = isDone;
                todo.UpdatedDate = DateTime.UtcNow;
                return TypedResults.Ok(todo);
            });

            app.MapPatch("/Todos/{id}/IsDone", (int id, bool isDone) =>
            {
                var todo = Todos.SingleOrDefault(t => t.Id == id);
                if (todo == null)
                {
                    return TypedResults.NotFound();
                }
                todo.IsDone = isDone;
                return Results.Json(new { id = todo.Id, isDone = todo.IsDone });
            });

            app.MapDelete("/Todos/{id}", (int id) =>
            {
                var todo = Todos.SingleOrDefault(t => t.Id == id);
                if (todo == null)
                {
                    return TypedResults.NotFound();
                }
                Todos = Todos.Where(t => t.Id != id).ToList();
                return Results.Ok();
            });
        }
    }

}