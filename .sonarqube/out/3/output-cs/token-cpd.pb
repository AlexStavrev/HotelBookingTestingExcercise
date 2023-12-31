�4
x/Users/hk/Documents/Undervisning/Test/Solutions/HotelBooking_Clean/HotelBooking.WebApi/Controllers/BookingsController.cs
	namespace 	
HotelBooking
 
. 
WebApi 
. 
Controllers )
{ 
[ 
ApiController 
] 
[		 
Route		 

(		
 
$str		 
)		 
]		 
public

 

class

 
BookingsController

 #
:

$ %

Controller

& 0
{ 
private 
IRepository 
< 
Booking #
># $
bookingRepository% 6
;6 7
private 
IRepository 
< 
Customer $
>$ %
customerRepository& 8
;8 9
private 
IRepository 
< 
Room  
>  !
roomRepository" 0
;0 1
private 
IBookingManager 
bookingManager  .
;. /
public 
BookingsController !
(! "
IRepository" -
<- .
Booking. 5
>5 6
bookingRepos7 C
,C D
IRepositoryE P
<P Q
RoomQ U
>U V
	roomReposW `
,` a
IRepository 
< 
Customer  
>  !
customerRepos" /
,/ 0
IBookingManager1 @
managerA H
)H I
{ 	
bookingRepository 
= 
bookingRepos  ,
;, -
roomRepository 
= 
	roomRepos &
;& '
customerRepository 
=  
customerRepos! .
;. /
bookingManager 
= 
manager $
;$ %
} 	
[ 	
HttpGet	 
( 
Name 
= 
$str %
)% &
]& '
public 
IEnumerable 
< 
Booking "
>" #
Get$ '
(' (
)( )
{ 	
return 
bookingRepository $
.$ %
GetAll% +
(+ ,
), -
;- .
} 	
["" 	
HttpGet""	 
("" 
$str"" 
,"" 
Name"" 
="" 
$str""  ,
)"", -
]""- .
public## 
IActionResult## 
Get##  
(##  !
int##! $
id##% '
)##' (
{$$ 	
var%% 
item%% 
=%% 
bookingRepository%% (
.%%( )
Get%%) ,
(%%, -
id%%- /
)%%/ 0
;%%0 1
if&& 
(&& 
item&& 
==&& 
null&& 
)&& 
{'' 
return(( 
NotFound(( 
(((  
)((  !
;((! "
})) 
return** 
new** 
ObjectResult** #
(**# $
item**$ (
)**( )
;**) *
}++ 	
[.. 	
HttpPost..	 
].. 
public// 
IActionResult// 
Post// !
(//! "
[//" #
FromBody//# +
]//+ ,
Booking//, 3
booking//4 ;
)//; <
{00 	
if11 
(11 
booking11 
==11 
null11 
)11  
{22 
return33 

BadRequest33 !
(33! "
)33" #
;33# $
}44 
bool66 
created66 
=66 
bookingManager66 )
.66) *
CreateBooking66* 7
(667 8
booking668 ?
)66? @
;66@ A
if88 
(88 
created88 
)88 
{99 
return:: 
CreatedAtRoute:: %
(::% &
$str::& 3
,::3 4
null::5 9
)::9 :
;::: ;
};; 
else<< 
{== 
return>> 
Conflict>> 
(>>  
$str>>  v
)>>v w
;>>w x
}?? 
}AA 	
[DD 	
HttpPutDD	 
(DD 
$strDD 
)DD 
]DD 
publicEE 
IActionResultEE 
PutEE  
(EE  !
intEE! $
idEE% '
,EE' (
[EE) *
FromBodyEE* 2
]EE2 3
BookingEE3 :
bookingEE; B
)EEB C
{FF 	
ifGG 
(GG 
bookingGG 
==GG 
nullGG 
||GG  "
bookingGG# *
.GG* +
IdGG+ -
!=GG. 0
idGG1 3
)GG3 4
{HH 
returnII 

BadRequestII !
(II! "
)II" #
;II# $
}JJ 
varLL 
modifiedBookingLL 
=LL  !
bookingRepositoryLL" 3
.LL3 4
GetLL4 7
(LL7 8
idLL8 :
)LL: ;
;LL; <
ifNN 
(NN 
modifiedBookingNN 
==NN  "
nullNN# '
)NN' (
{OO 
returnPP 
NotFoundPP 
(PP  
)PP  !
;PP! "
}QQ 
modifiedBookingVV 
.VV 
IsActiveVV $
=VV% &
bookingVV' .
.VV. /
IsActiveVV/ 7
;VV7 8
modifiedBookingWW 
.WW 

CustomerIdWW &
=WW' (
bookingWW) 0
.WW0 1

CustomerIdWW1 ;
;WW; <
bookingRepositoryYY 
.YY 
EditYY "
(YY" #
modifiedBookingYY# 2
)YY2 3
;YY3 4
returnZZ 
	NoContentZZ 
(ZZ 
)ZZ 
;ZZ 
}[[ 	
[^^ 	

HttpDelete^^	 
(^^ 
$str^^ 
)^^ 
]^^ 
public__ 
IActionResult__ 
Delete__ #
(__# $
int__$ '
id__( *
)__* +
{`` 	
ifaa 
(aa 
bookingRepositoryaa !
.aa! "
Getaa" %
(aa% &
idaa& (
)aa( )
==aa* ,
nullaa- 1
)aa1 2
{bb 
returncc 
NotFoundcc 
(cc  
)cc  !
;cc! "
}dd 
bookingRepositoryff 
.ff 
Removeff $
(ff$ %
idff% '
)ff' (
;ff( )
returngg 
	NoContentgg 
(gg 
)gg 
;gg 
}hh 	
}jj 
}kk �

y/Users/hk/Documents/Undervisning/Test/Solutions/HotelBooking_Clean/HotelBooking.WebApi/Controllers/CustomersController.cs
	namespace 	
HotelBooking
 
. 
WebApi 
. 
Controllers )
{ 
[ 
ApiController 
] 
[		 
Route		 

(		
 
$str		 
)		 
]		 
public

 

class

 
CustomersController

 $
:

% &

Controller

' 1
{ 
private 
readonly 
IRepository $
<$ %
Customer% -
>- .

repository/ 9
;9 :
public 
CustomersController "
(" #
IRepository# .
<. /
Customer/ 7
>7 8
repos9 >
)> ?
{ 	

repository 
= 
repos 
; 
} 	
[ 	
HttpGet	 
] 
public 
IEnumerable 
< 
Customer #
># $
Get% (
(( )
)) *
{ 	
return 

repository 
. 
GetAll $
($ %
)% &
;& '
} 	
} 
} �
u/Users/hk/Documents/Undervisning/Test/Solutions/HotelBooking_Clean/HotelBooking.WebApi/Controllers/RoomsController.cs
	namespace 	
HotelBooking
 
. 
WebApi 
. 
Controllers )
{ 
[		 
ApiController		 
]		 
[

 
Route

 

(


 
$str

 
)

 
]

 
public 

class 
RoomsController  
:! "

Controller# -
{ 
private 
readonly 
IRepository $
<$ %
Room% )
>) *

repository+ 5
;5 6
public 
RoomsController 
( 
IRepository *
<* +
Room+ /
>/ 0
repos1 6
)6 7
{ 	

repository 
= 
repos 
; 
} 	
[ 	
HttpGet	 
( 
Name 
= 
$str "
)" #
]# $
public 
IEnumerable 
< 
Room 
>  
Get! $
($ %
)% &
{ 	
return 

repository 
. 
GetAll $
($ %
)% &
;& '
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
IActionResult 
Get  
(  !
int! $
id% '
)' (
{ 	
var 
item 
= 

repository !
.! "
Get" %
(% &
id& (
)( )
;) *
if   
(   
item   
==   
null   
)   
{!! 
return"" 
NotFound"" 
(""  
)""  !
;""! "
}## 
return$$ 
new$$ 
ObjectResult$$ #
($$# $
item$$$ (
)$$( )
;$$) *
}%% 	
[(( 	
HttpPost((	 
](( 
public)) 
IActionResult)) 
Post)) !
())! "
[))" #
FromBody))# +
]))+ ,
Room))- 1
room))2 6
)))6 7
{** 	
if++ 
(++ 
room++ 
==++ 
null++ 
)++ 
{,, 
return-- 

BadRequest-- !
(--! "
)--" #
;--# $
}.. 

repository00 
.00 
Add00 
(00 
room00 
)00  
;00  !
return11 
CreatedAtRoute11 !
(11! "
$str11" ,
,11, -
null11. 2
)112 3
;113 4
}22 	
[66 	

HttpDelete66	 
(66 
$str66 
)66 
]66 
public77 
IActionResult77 
Delete77 #
(77# $
int77$ '
id77( *
)77* +
{88 	
if99 
(99 
id99 
>99 
$num99 
)99 
{:: 

repository;; 
.;; 
Remove;; !
(;;! "
id;;" $
);;$ %
;;;% &
return<< 
	NoContent<<  
(<<  !
)<<! "
;<<" #
}== 
else>> 
{>> 
return?? 

BadRequest?? !
(??! "
)??" #
;??# $
}@@ 
}AA 	
}CC 
}DD �!
a/Users/hk/Documents/Undervisning/Test/Solutions/HotelBooking_Clean/HotelBooking.WebApi/Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder

 
.

 
Services

 
.

 
AddDbContext

 
<

 
HotelBookingContext

 1
>

1 2
(

2 3
opt

3 6
=>

7 9
opt

: =
.

= >
UseInMemoryDatabase

> Q
(

Q R
$str

R b
)

b c
)

c d
;

d e
builder 
. 
Services 
. 
	AddScoped 
< 
IRepository &
<& '
Room' +
>+ ,
,, -
RoomRepository. <
>< =
(= >
)> ?
;? @
builder 
. 
Services 
. 
	AddScoped 
< 
IRepository &
<& '
Customer' /
>/ 0
,0 1
CustomerRepository2 D
>D E
(E F
)F G
;G H
builder 
. 
Services 
. 
	AddScoped 
< 
IRepository &
<& '
Booking' .
>. /
,/ 0
BookingRepository1 B
>B C
(C D
)D E
;E F
builder 
. 
Services 
. 
	AddScoped 
< 
IBookingManager *
,* +
BookingManager, :
>: ;
(; <
)< =
;= >
builder 
. 
Services 
. 
AddTransient 
< 
IDbInitializer ,
,, -
DbInitializer. ;
>; <
(< =
)= >
;> ?
builder 
. 
Services 
. 
AddControllers 
(  
)  !
;! "
builder 
. 
Services 
. #
AddEndpointsApiExplorer (
(( )
)) *
;* +
builder 
. 
Services 
. 
AddSwaggerGen 
( 
)  
;  !
var 
app 
= 	
builder
 
. 
Build 
( 
) 
; 
if 
( 
app 
. 
Environment 
. 
IsDevelopment !
(! "
)" #
)# $
{ 
app 
. 

UseSwagger 
( 
) 
; 
app 
. 
UseSwaggerUI 
( 
) 
; 
using   	
(  
 
var   
scope   
=   
app   
.   
Services   #
.  # $
CreateScope  $ /
(  / 0
)  0 1
)  1 2
{!! 
var"" 
services"" 
="" 
scope"" 
."" 
ServiceProvider"" ,
;"", -
var## 
	dbContext## 
=## 
services##  
.##  !

GetService##! +
<##+ ,
HotelBookingContext##, ?
>##? @
(##@ A
)##A B
;##B C
var$$ 
dbInitializer$$ 
=$$ 
services$$ $
.$$$ %

GetService$$% /
<$$/ 0
IDbInitializer$$0 >
>$$> ?
($$? @
)$$@ A
;$$A B
dbInitializer%% 
.%% 

Initialize%%  
(%%  !
	dbContext%%! *
)%%* +
;%%+ ,
}&& 
}'' 
app)) 
.)) 
UseHttpsRedirection)) 
()) 
))) 
;)) 
app++ 
.++ 
UseAuthorization++ 
(++ 
)++ 
;++ 
app-- 
.-- 
MapControllers-- 
(-- 
)-- 
;-- 
app// 
.// 
Run// 
(// 
)// 	
;//	 
