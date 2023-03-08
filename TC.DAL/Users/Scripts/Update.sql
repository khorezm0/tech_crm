UPDATE tc_user 
SET
    deleted_time           = :DeletedTime,
    user_name              = :UserName,
    phone_number           = :PhoneNumber,
    phone_number_confirmed = :PhoneNumberConfirmed,
    email                  = :Email,
    email_confirmed        = :EmailConfirmed,
    password_hash          = :PasswordHash,
    first_name             = :FirstName,
    last_name              = :LastName
from tc_user
WHERE id = :Id
returning
    id                     as Id,
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
;