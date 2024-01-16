namespace Common.DTO;

public record QuizRecord(string Id, string Name, string Description, List<QuestionRecord> Questions);
//Vad borde jag ha en lista av? Questions? QuestionRecords?