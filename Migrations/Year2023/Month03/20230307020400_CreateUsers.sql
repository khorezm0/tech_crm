create table tc_user
(
    id serial not null
        constraint pk__tc_user
            primary key,
    created_time timestamp not null,
    deleted_time timestamp,
    modified_time timestamp,
    user_name varchar not null,
    phone_number varchar,
    phone_number_confirmed bool not null,
    email varchar,
    email_confirmed bool not null,
    password_hash varchar not null,
    first_name varchar,
    last_name varchar
);

create index ix__tc_user__phone_number
    on tc_user (phone_number)
;
create unique index ux__tc_user__phone_number
    on tc_user (phone_number)
    where phone_number IS NOT NULL
;

create index ix__tc_user__user_name
    on tc_user (user_name)
;
create unique index ux__tc_user__user_name
    on tc_user (user_name)
    where user_name IS NOT NULL
;

create index ix__tc_user__email
    on tc_user (email)
;
create unique index ux__tc_user__email
    on tc_user (email)
    where email IS NOT NULL
;