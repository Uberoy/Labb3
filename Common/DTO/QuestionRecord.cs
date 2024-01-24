namespace Common.DTO;

public record QuestionRecord(string Id, string Description, List<string> Answers, int CorrectAnswer, List<CategoryRecord> Categories);