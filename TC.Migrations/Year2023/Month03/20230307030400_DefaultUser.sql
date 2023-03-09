-- User with login dev and password 123123ASDasd!

insert into tc_user (created_time, deleted_time, user_name, phone_number, phone_number_confirmed, email,
                     email_confirmed, password_hash, first_name, last_name)
values (now(), null, 'dev', '+79999999999', true, 'dev@dev.com', true, 'AHGmhKWqvxOMN2VhEFbH5KShHensvNUDixwBOS6ZM6LokakMtZdNrqnC09zx2PXkEQ==', 'Dev', 'Dev');