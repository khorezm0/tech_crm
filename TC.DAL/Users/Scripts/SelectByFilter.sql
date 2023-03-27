select t.id                     as Id,
       t.created_time           as CreatedTime,
       t.deleted_time           as DeletedTime,
       t.user_name              as UserName,
       t.phone_number           as PhoneNumber,
       t.phone_number_confirmed as PhoneNumberConfirmed,
       t.email                  as Email,
       t.email_confirmed        as EmailConfirmed,
       t.password_hash          as PasswordHash,
       t.first_name             as FirstName,
       t.last_name              as LastName,
       t.modified_time          as ModifiedTime
from tc_user t
WHERE t.deleted_time is null
--UserIds-- and t.id = any(@UserIds)
ORDER BY id
LIMIT :Limit
OFFSET :Offset;