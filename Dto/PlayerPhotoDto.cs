namespace ApiRest.Dto;

public class PlayerPhotoDto(int Id, string Name)
{
    public int Id { get; set; } = Id;

    public string Name { get; set; } = Name;
}