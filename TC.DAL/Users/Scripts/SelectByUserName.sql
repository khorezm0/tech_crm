select id                     as Id,
       created_time           as CreatedTime,
       deleted_time           as DeletedTime,
       user_name              as UserName,
       phone_number           as PhoneNumber,
       phone_number_confirmed as PhoneNumberConfirmed,
       email                  as Email,
       email_confirmed        as EmailConfirmed,
       password_hash          as PasswordHash,
       first_name             as FirstName,
       last_name              as LastName,
       modified_time          as ModifiedTime
from tc_user
WHERE user_name = :UserName
   or email = :UserName
   or phone_number = :UserName
;