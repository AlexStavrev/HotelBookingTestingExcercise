§
h/Users/hk/Documents/Undervisning/Test/Solutions/HotelBooking_Clean/HotelBooking.Core/Entities/Booking.cs
	namespace 	
HotelBooking
 
. 
Core 
{ 
public 

class 
Booking 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
DateTime 
	StartDate !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 
DateTime		 
EndDate		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public

 
bool

 
IsActive

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
public 
int 

CustomerId 
{ 
get  #
;# $
set% (
;( )
}* +
public 
int 
RoomId 
{ 
get 
;  
set! $
;$ %
}& '
public 
virtual 
Customer 
Customer  (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
virtual 
Room 
Room  
{! "
get# &
;& '
set( +
;+ ,
}- .
} 
} ô
i/Users/hk/Documents/Undervisning/Test/Solutions/HotelBooking_Clean/HotelBooking.Core/Entities/Customer.cs
	namespace 	
HotelBooking
 
. 
Core 
{ 
public 

class 
Customer 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
} 
}		 Ú
e/Users/hk/Documents/Undervisning/Test/Solutions/HotelBooking_Clean/HotelBooking.Core/Entities/Room.cs
	namespace 	
HotelBooking
 
. 
Core 
{ 
public 

class 
Room 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
} ‰
r/Users/hk/Documents/Undervisning/Test/Solutions/HotelBooking_Clean/HotelBooking.Core/Interfaces/IBookingManager.cs
	namespace 	
HotelBooking
 
. 
Core 
{ 
public 

	interface 
IBookingManager $
{ 
bool 
CreateBooking 
( 
Booking "
booking# *
)* +
;+ ,
int		 
FindAvailableRoom		 
(		 
DateTime		 &
	startDate		' 0
,		0 1
DateTime		2 :
endDate		; B
)		B C
;		C D
List

 
<

 
DateTime

 
>

 !
GetFullyOccupiedDates

 ,
(

, -
DateTime

- 5
	startDate

6 ?
,

? @
DateTime

A I
endDate

J Q
)

Q R
;

R S
} 
} ¹
n/Users/hk/Documents/Undervisning/Test/Solutions/HotelBooking_Clean/HotelBooking.Core/Interfaces/IRepository.cs
	namespace 	
HotelBooking
 
. 
Core 
{ 
public 

	interface 
IRepository  
<  !
T! "
>" #
{ 
IEnumerable 
< 
T 
> 
GetAll 
( 
) 
;  
T 	
Get
 
( 
int 
id 
) 
; 
void		 
Add		 
(		 
T		 
entity		 
)		 
;		 
void

 
Edit

 
(

 
T

 
entity

 
)

 
;

 
void 
Remove 
( 
int 
id 
) 
; 
} 
} Æ6
o/Users/hk/Documents/Undervisning/Test/Solutions/HotelBooking_Clean/HotelBooking.Core/Services/BookingManager.cs
	namespace 	
HotelBooking
 
. 
Core 
{ 
public 

class 
BookingManager 
:  !
IBookingManager" 1
{ 
private		 
IRepository		 
<		 
Booking		 #
>		# $
bookingRepository		% 6
;		6 7
private

 
IRepository

 
<

 
Room

  
>

  !
roomRepository

" 0
;

0 1
public 
BookingManager 
( 
IRepository )
<) *
Booking* 1
>1 2
bookingRepository3 D
,D E
IRepositoryF Q
<Q R
RoomR V
>V W
roomRepositoryX f
)f g
{ 	
this 
. 
bookingRepository "
=# $
bookingRepository% 6
;6 7
this 
. 
roomRepository 
=  !
roomRepository" 0
;0 1
} 	
public 
bool 
CreateBooking !
(! "
Booking" )
booking* 1
)1 2
{ 	
int 
roomId 
= 
FindAvailableRoom *
(* +
booking+ 2
.2 3
	StartDate3 <
,< =
booking> E
.E F
EndDateF M
)M N
;N O
if 
( 
roomId 
>= 
$num 
) 
{ 
booking 
. 
RoomId 
=  
roomId! '
;' (
booking 
. 
IsActive  
=! "
true# '
;' (
bookingRepository !
.! "
Add" %
(% &
booking& -
)- .
;. /
return 
true 
; 
} 
else 
{ 
return   
false   
;   
}!! 
}"" 	
public$$ 
int$$ 
FindAvailableRoom$$ $
($$$ %
DateTime$$% -
	startDate$$. 7
,$$7 8
DateTime$$9 A
endDate$$B I
)$$I J
{%% 	
if&& 
(&& 
	startDate&& 
<=&& 
DateTime&& %
.&&% &
Today&&& +
||&&, .
	startDate&&/ 8
>&&9 :
endDate&&; B
)&&B C
throw'' 
new'' 
ArgumentException'' +
(''+ ,
$str'', n
)''n o
;''o p
var)) 
activeBookings)) 
=))  
bookingRepository))! 2
.))2 3
GetAll))3 9
())9 :
))): ;
.)); <
Where))< A
())A B
b))B C
=>))D F
b))G H
.))H I
IsActive))I Q
)))Q R
;))R S
foreach** 
(** 
var** 
room** 
in**  
roomRepository**! /
.**/ 0
GetAll**0 6
(**6 7
)**7 8
)**8 9
{++ 
var,, (
activeBookingsForCurrentRoom,, 0
=,,1 2
activeBookings,,3 A
.,,A B
Where,,B G
(,,G H
b,,H I
=>,,J L
b,,M N
.,,N O
RoomId,,O U
==,,V X
room,,Y ]
.,,] ^
Id,,^ `
),,` a
;,,a b
if-- 
(-- (
activeBookingsForCurrentRoom-- 0
.--0 1
All--1 4
(--4 5
b--5 6
=>--7 9
	startDate--: C
<--D E
b--F G
.--G H
	StartDate--H Q
&&--R T
endDate.. 
<.. 
b.. 
...  
	StartDate..  )
||..* ,
	startDate..- 6
>..7 8
b..9 :
...: ;
EndDate..; B
&&..C E
endDate..F M
>..N O
b..P Q
...Q R
EndDate..R Y
)..Y Z
)..Z [
{// 
return00 
room00 
.00  
Id00  "
;00" #
}11 
}22 
return33 
-33 
$num33 
;33 
}44 	
public66 
List66 
<66 
DateTime66 
>66 !
GetFullyOccupiedDates66 3
(663 4
DateTime664 <
	startDate66= F
,66F G
DateTime66H P
endDate66Q X
)66X Y
{77 	
if88 
(88 
	startDate88 
>88 
endDate88 #
)88# $
throw99 
new99 
ArgumentException99 +
(99+ ,
$str99, _
)99_ `
;99` a
List;; 
<;; 
DateTime;; 
>;; 
fullyOccupiedDates;; -
=;;. /
new;;0 3
List;;4 8
<;;8 9
DateTime;;9 A
>;;A B
(;;B C
);;C D
;;;D E
int<< 
	noOfRooms<< 
=<< 
roomRepository<< *
.<<* +
GetAll<<+ 1
(<<1 2
)<<2 3
.<<3 4
Count<<4 9
(<<9 :
)<<: ;
;<<; <
var== 
bookings== 
=== 
bookingRepository== ,
.==, -
GetAll==- 3
(==3 4
)==4 5
;==5 6
if?? 
(?? 
bookings?? 
.?? 
Any?? 
(?? 
)?? 
)?? 
{@@ 
forAA 
(AA 
DateTimeAA 
dAA 
=AA  !
	startDateAA" +
;AA+ ,
dAA- .
<=AA/ 1
endDateAA2 9
;AA9 :
dAA; <
=AA= >
dAA? @
.AA@ A
AddDaysAAA H
(AAH I
$numAAI J
)AAJ K
)AAK L
{BB 
varCC 
noOfBookingsCC $
=CC% &
fromCC' +
bCC, -
inCC. 0
bookingsCC1 9
whereDD' ,
bDD- .
.DD. /
IsActiveDD/ 7
&&DD8 :
dDD; <
>=DD= ?
bDD@ A
.DDA B
	StartDateDDB K
&&DDL N
dDDO P
<=DDQ S
bDDT U
.DDU V
EndDateDDV ]
selectEE' -
bEE. /
;EE/ 0
ifFF 
(FF 
noOfBookingsFF $
.FF$ %
CountFF% *
(FF* +
)FF+ ,
>=FF- /
	noOfRoomsFF0 9
)FF9 :
fullyOccupiedDatesGG *
.GG* +
AddGG+ .
(GG. /
dGG/ 0
)GG0 1
;GG1 2
}HH 
}II 
returnJJ 
fullyOccupiedDatesJJ %
;JJ% &
}KK 	
}MM 
}NN 