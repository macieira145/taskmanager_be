using CardExchange.Dto;

namespace CardExchange.Entities;

public partial class Task
{
    public int Id { get; set; }

    public int UsersId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Completed { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual User? User { get; set; }

    public Task(int id)
    {
        this.Id = id;
    }

    public Task(TaskDTO taskDTO)
    {
        if(taskDTO.Id is not null)
            this.Id = (int)taskDTO.Id;
        this.Title = taskDTO.Title;
        this.Description = taskDTO.Description;
        this.Completed = taskDTO.Completed;
    }
}
