public interface IPilotContainer
{
    bool ContainsPilot(Pilot_SO pilot);
    bool RemovePilot(Pilot_SO pilot);
    bool AddNewPilot(Pilot_SO pilot);
    bool IsFull();
    int PilotCount(Pilot_SO pilot);


}
