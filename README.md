# chat-support-task

MoneybaseTask.Hosting.WebApi - Startup project

I've solved this task making a few assumptions:

- Task says "Once the session queue is full, unless it's during office hours and an overflow is available" and "Agents are on a 3 shift basis 8hrs each."

My understanding was that there are agents available 24h/24h, and given the fact that there's no explicit hours in the task question to determine the 'office hours', I've assumed the morning and the afternoon shifts would be during the 'office hours', whereas the night shift would be outside of the 'office hours' => I've only applied an overflow team for the Morning and Afternoon shifts.

- Task says "The maximum queue length allowed is the capacity of the team multiplied by 1.5" and "Same rules applies for overflow; once full, the chat is refused."
I have assumed that the maximum queue length would apply to both the regular team and also for the overflow team
