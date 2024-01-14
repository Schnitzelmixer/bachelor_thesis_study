using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;

public class CsvManager
{
    private string _filePath;

    private char _separator;

    public CsvManager(string filePath, char separator = ';')
    {
        this._filePath = filePath;
        this._separator = separator;

        this.WriteHeadings();
    }

    public void SaveParticipant(Participant participant)
    {
        Participant partialParticipant = this.GetPartialParticipant();

        if (partialParticipant != null && partialParticipant.Id == participant.Id)
        {
            this.UpdateParticipant(participant);
        }
        else
        {
            this.WriteParticipant(participant);
        }
    }

    private void WriteParticipant(Participant participant)
    {
        StreamWriter streamWriter = new StreamWriter(_filePath, true);
        streamWriter.WriteLine(TransformParticipantToCells(participant));
        streamWriter.Close();
    }

    private void UpdateParticipant(Participant participant)
    {
        string[] lines = File.ReadAllLines(_filePath);

        int idIndex = Participant.GetIndexForProperty("Id");

        // skip headings
        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(this._separator);

            if (cells[idIndex].Equals(participant.Id))
            {
                lines[i] = TransformParticipantToCells(participant);

                break;
            }
        }

        File.WriteAllLines(_filePath, lines);
    }

    private void WriteHeadings()
    {
        StreamReader streamReader = new StreamReader(_filePath);
        string line = streamReader.ReadLine();
        streamReader.Close();

        if (line == null)
        {
            string[] headings =
                typeof(Participant)
                    .GetProperties()
                    .Select(property => property.Name)
                    .ToArray();

            StreamWriter streamWriter = new StreamWriter(_filePath);
            streamWriter.WriteLine(TransformCellsToString(headings));
            streamWriter.Close();
        }
    }

    public Participant GetPartialParticipant()
    {
        string[] lines = File.ReadAllLines(_filePath);

        // skip headings
        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(this._separator);

            if (cells.Contains(""))
            {
                return this.TransformCellsToParticipant(cells);
            }
        }

        return null;
    }

    public string GetNewParticipantId()
    {
        string[] lines = File.ReadAllLines(_filePath);

        string lastId = null;
        int idIndex = Participant.GetIndexForProperty("Id");

        // skip headings
        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(this._separator);

            if (cells[idIndex] != "")
            {
                lastId = cells[idIndex];
            }
        }

        return lastId == null ? Participant.startIndex.ToString() : (int.Parse(lastId) + 1).ToString();
    }

    private string TransformCellsToString(string[] cells)
    {
        return string.Join(_separator.ToString(), cells);
    }

    private string TransformParticipantToCells(Participant participant)
    {
        string[] values =
            typeof(Participant)
                .GetProperties()
                .Select(property => property.GetValue(participant)?.ToString())
                .ToArray();

        return TransformCellsToString(values);
    }

    private Participant TransformCellsToParticipant(string[] cells)
    {
        return new Participant
        {
            Id = cells.Length - 1 >= 0 ? cells[0] : "",
            Article0 = cells.Length - 1 >= 1 ? cells[1] : "",
            Indicator0 = cells.Length - 1 >= 2 ? cells[2] : "",
            Article1 = cells.Length - 1 >= 3 ? cells[3] : "",
            Indicator1 = cells.Length - 1 >= 4 ? cells[4] : "",
            Article2 = cells.Length - 1 >= 5 ? cells[5] : "",
            Indicator2 = cells.Length - 1 >= 6 ? cells[6] : "",
            Article3 = cells.Length - 1 >= 7 ? cells[7] : "",
            Indicator3 = cells.Length - 1 >= 8 ? cells[8] : "",
        };
    }
}
