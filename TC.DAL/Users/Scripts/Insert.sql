INSERT INTO tc_user
(created_time, deleted_time, user_name, phone_number, phone_number_confirmed, email,
 email_confirmed, password_hash, first_name, last_name)
VALUES (:CreatedTime, :DeletedTime, :UserName, :PhoneNumber, :PhoneNumberConfirmed, :Email, :EmailConfirmed,
        :PasswordHash, :FirstName, :LastName)
returning id as Id,
    created_time as CreatedTime,
    deleted_time as DeletedTime,
    user_name as UserName,
    phone_number as PhoneNumber,
    phone_number_confirmed as PhoneNumberConfirmed,
    email as Email,
    email_confirmed as EmailConfirmed,
    password_hash as PasswordHash,
    first_name as FirstName,
    last_name as LastName
;