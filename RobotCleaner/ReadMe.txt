RobotCleaner
0.
------------------------
To run solution you should 
0. build solution (required libraries will be downloded by Nuget);
1. Move to RobotCleaner.exe (bin/Debug);
2. Run the command in the next format: RobotCleaner.exe test1.json test1.result.json


1.
------------------------
Architecture comments:
0. All Settings are located in App.Config
	a) if we want to change the number of consumed battery units for any instruction or back off strategy commands, we should only change value in AppConfig;
1. Interface IMachineCleaner makes it possible to replace robot with something else;
2. InctructionExecutor is a class that:
	a) has Dictionary Mapping between Instructions (TL, TR, B...) and Commands implementing these Instructions (TurnLeft, TurnRight, Back...);
	b) invokes the appropriate Command, taken from Dictionary Mapping;
	c) enables us to:
	   1) easily add a new type of Instructions and Commands (for example, to dance), without changing the existing code;
           2) change the logic of a Command without touching other Commands;
	   2) manage process flow based on result of a Command;
3. Interface IBackOffStrategiesExecutor helps to change the logic of back off strategy;
4. Interface IBackOffInstructionsInitializer helps to change, for instance, the source of back off strategies or replace them;