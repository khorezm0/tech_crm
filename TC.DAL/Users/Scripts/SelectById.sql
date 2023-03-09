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
       last_name              as LastName
from tc_user
WHERE id = :Id
;