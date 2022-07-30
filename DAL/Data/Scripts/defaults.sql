-- User with login dev and password 123123ASDasd!

insert into AspNetUsers (Id, FirstName, LastName, CreatedTime, DeletedTime, UserName, NormalizedUserName, Email,
                         NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber,
                         PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
values (
           '5b504b00-4363-443f-8e86-91d44cef807e',
           'Developer','A','2022-07-31 02:50:20.2828621', NULL,
           'dev', 'DEV', 'dev@mail.ru', 'DEV@MAIL.RU', 0,'AQAAAAEAACcQAAAAEN2CdEbQV1DVceM8MJjYQlVPnTZ2hq43Fa9g5SK6t0dSLnHlkr0kblZFnMpF23fIyg==',
           'RMEZI46DLZFMZCFGPQ2FVAGJEUEYUA4R', '32f57c38-e8a9-4d7e-8a32-96ebb3cbd283','+79999999999', 0,0, NULL,1,0
       );