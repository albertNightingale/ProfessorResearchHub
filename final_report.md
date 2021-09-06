# Final Project - URC by Webable
Albert (Jiaming) Liu

Lucas Falslev

Hunter Moffat

Kyle Skinner


## Abstract
We have enhanced the URC project to add functionality to increase the usability for all users of the site. We implemented enhanced account features. Professors have increased control over managing a research opportunity's available positions and hiring students. Students can receive notifications when hired and about positions that match their skills. And administrators have to do less work in maintaining the opportunities listings.

## URLS
ec2-54-198-66-224.compute-1.amazonaws.com (NO LONGER ACTIVE)

## Introduction
The account management was the first step in increasing the site's usability across all role types. Users can now manage their account by changing passwords and resetting forgotten passwords. Also, users can include information about themselves in a profile section. This will aid students, in particular, to be able to prefill information that will be needed as they apply for different research opportunities, greatly speeding up the application process.
 
When research opportunities are created, more information about the opportunity can now be included. This helps professors to better describe the position, and helps students determine if the opportunity is a match for them. Available research positions are an important addition for the project, so professors and administrators can indicate how many students can be accepted to a position based on that opportunity's budget and requirements. Also, professors are able to review student applications and accept a student for a research position from the student application details screen. When a student is accepted for a research position, the total number of positions is automatically decremented and when all positions have been filled, the research opportunity is no longer displayed in the general opportunity listings page. This assists administrators, as they will have less work to do to maintain that listings page.
 
As mentioned earlier, students are now able to fill out a profile section which includes information about themselves that can then be used to prefill an application. This helps the students not worry about filling out the same information several times across applications, which makes the user experience greatly enhanced. Also, we have placed a particular focus on the student's skills, giving them a specific page to manage their skills. When a research opportunity is created, or updated, requiring the skills that match the student's, the student is sent a notification that there is a research opportunity that matches their skills, and therefore they may be more interested in, or have a greater chance of being accepted to. Also, students receive a notification when they are accepted to a research opportunity.
 
The notification section allows students to go to one area to see all their unread notifications and mark them as read if they no longer wish to see them. The notifications feature was implemented specifically in a way that will allow it to be extended to professors and administrators, if that functionality is desired in the future.

## Feature Table

| Feature Name | Scope | Primary Programmer | Time Spent | File/Function | LoC |
|:------------:|:-----:|:------------------:|:----------:|:-------------:|:---:|
| Able to see an account management system |front end | Albert | 2 hrs | Area/Identity/Pages | 10 |
| Able to change the user profile information | front end | Albert | 10 hrs | Area/Identity/Pages/Account/Manage/Index | 239 |
| Create Student Profile and database | back end | Albert | 13hr | Areas/Data/ | 441 |
| Able to access the website remotely through an EC2 instance | Hosting | Albert | 3 hour | appsettings.Production.json | 5 |
| Opportunity Slots |back end | Hunter | 3 hrs | OpportunitiesSeeding, Opportunities Model |~20  |
| Applicant List shows applications to specific opportunities |front and back end | Hunter | 10 hrs | Applications Controler, details view and index view.  Opportunities list view | ~120 |
| Accepting application notifies student |back end | Hunter | 5 hrs | ApplicationsController, site.js | ~50 |
| Accepting Application decrements slots in opportunities | back end | Hunter | 1 hr | Applications Controller | ~30 |
| Opportunities with 0 slots available no longer are viewable to students |front and back end | Hunter | 1 hr | Opportunities list view | ~10 |
| Accepting Applicants removes them from the applicant list of professor | back and front end | Hunter | 3 hr | Applications Controller, index view | ~40 |
| Able to create notifications attached to a user | back end | Kyle | 12 hrs | Model/Notification Controllers/NotificationsController SeedUsersRolesDB.AddNotifications | 156 |
| Able to see list of notifications and mark read | full | Kyle | 3 hrs | Views/Notifications/Index | 50 |
| Create skills db table and associate with student | back end | Kyle | 4 hrs | Model/Skill Controllers/SkillsController SeedUsersRolesDB.AddSkills | 77 |
| Add screen to see skills and update/delete/add new skills | full scope | Kyle | 3 hrs | Controllers/SkillsController Views/Skills/Index Indentity/Pages/Index | 87 |
| When research opportunity is created or edited, notifications sent to students with matching skills | back end | Kyle | 2 hrs | Controllers/OpportunitiesController | 30 |
| Link application to opportunity | back end | Lucas | 15 hrs | OpportunitySeeding ApplicationsController Models/Application Opportunities/Index Opportunities/Details | 60 |
| Autofill application fields | Full Stack | Lucas | 2 hrs | ApplicationsController Applications/Create | 50 |
| Edit/Apply button for opportunities | front end | Lucas | 2 hrs | Opportunities/Index Opportunities/Details | 10 |


## Individual Contribution Table

| Team Member | Time Spent on Project | Lines of Code Committed |
|:-----------:|:---------------------:|:-----------------------:|
| Lucas Falslev    | 20 hours  | 120 |
| Hunter Moffat   | ~25 hours  | ~275 |
| Jiaming Liu    | 28 hours  | 681 |
| Kyle Skinner    | 35 hours  | 406 |

## Individual Contribution Summary
### Jiaming Liu
Jiaming added a userProfile table to the UsersRoles schema, using username (in this case, log in email) as a foreign key that ties to the Users table that holds the authentication information. The available operations on the user profile enables the student to change their profile information. Adding the userProfile object enables the skill matching feature that are done by other team members. Furthermore, Jiaming published the project to the EC2 instance so other users could access the website without having the project code available to them.

### Lucas Falslev
My focus for the final project was primarily around applications. My contributions were focused on creating a one to many relationship between opportunitites and applications that would allow students to apply to specific opportunities and professors to keep track of applications to their opportunitites. Adding the relationship itself was easy enough, but since so much was already implemented I had to go through a lot of project between Opportunities and Applications to pass the opportunity from view to controller and back to (a different) view. I also updated the CRUD forms for application to autofill user information and later the profile information added by Albert. Finally I cleaned up some of the application views to make it more usable and easier on the eyes. 

### Hunter Moffat
My contributions were to make opportunities have "slots" or a set number of positions available.  Once all the positions of an opportunity are filled they will no longer be displayed to students view of the opportunities page.  A professor can view the applicants to their opportunities in their applications list page, when they view an application, they can select "accept applicant" which removes the application from their list and decrements the positions available value of the opportunity object in the database.  On top of this, I worked closely with Kyle to set up notifications for students who have applied to an opportunity.  When a student is accepted for an opportunity, a notification will appear notifying them of their acceptance.  This implementation required significant changes to the opportunities database as well as the application controller and the views associated with both opportunities and applications.  

### Kyle Skinner
My contributions were related to notifications and skills. A lot of my work was around creating the models for those features. I had the biggest struggle with notifications because I was trying to associate them with the user identity. I found it difficult to figure out the best way to tie notifications to a user in the correct ASP.NET Core way. I went back and forth on several different implementations before settling on what I believe to be the best solution. Although I tried to approach most of the assignment with YAGNI (You Ain't Gonna Need It) in mind, I did implement notifications in a way that will allow for them to be extended to any user. I can see this being useful for notifying a professor when someone applies to their research opportunity, for example. After all the work I did on notifications, the skills model and implementation was easier. Tying the skills and notifications model in with their views took a little more work, too. Again, I went back and forth with a couple different implementations with notifications, and then extended the knowledge I gained to skills. I also did work to parse the required skills for an opportunity and then send notifications to students with those skills. I also spent a good deal of time pair programming, which helped create a more cohesive solution across a few of the features.

## Performance Level
We performed at a good level. The challenges of COVID forcing us to work remotely, definitely impacted our performance. While we did accomplish all of the features we wanted to, we would have liked to increase the visual polish of the project. All in all, we were able to hold planning meetings, pair program, and enforce correct Git procedures, such as utilizing pull requests and code reviews, to ensure our team was successful despite the difficulty of not being able to meet in person.

## Summary
We were able to work very effectively through disciplined coding practices, excellent planning, and by establishing a clear vision of what we wanted to accomplish from the beginning of the project. The enhancements that we have brought to the URC project, including account management, user profiles, available research positions, notifications, and skill management, are essential additions to the utility of the project. We believe these features have turned the URC project into something that has transformed the application into something that serves its users, no matter what role they are in. 
