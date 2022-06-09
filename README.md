# Diamond-Room
Application that allows the management of a hotel, boarding house etc. from the point of view of the client, but also of the administrator/employee

## First-View
Initially we are simple visitors, where the functionalities are limited, but we can still see the available offers and we can see some details about them and  the free rooms for a specified period of time, also the extra price if we select extra features, but can't make a reservation.
If we want to make a reservation we must have an account, automatically to be logged in. Also when we are logged in we can view the history of our reservations.
Of course we also have an area dedicated to staff (staff is considered either an employee or administrator)
The administrator can perform CRUD operations on most of the tables in the database and of course he can see all the currently active reservations in the hotel,
and many more. The employee is an extra logged in user with the possibility to view the reservations and change their status (paid, unpaid, various comments)

![firstVieww](https://user-images.githubusercontent.com/95618244/172885830-ba36da97-2a89-45d4-b728-b1e1c5b95718.JPG)

![offer1](https://user-images.githubusercontent.com/95618244/172886026-5a4d4911-5092-4705-b515-d3ffd480c5f7.JPG)

![register](https://user-images.githubusercontent.com/95618244/172886041-781ef64f-6366-4ba0-8fd3-9e7faaaae1b0.JPG)

## Access-levels
- level 0 (Admin)
- level 1 (Employee)
- level 2 (client)
- ? no level (guest)

## Now-working-at
- Create the reservation and make the payment, also in the payment menu we will have the possiblity to add a voucher 
