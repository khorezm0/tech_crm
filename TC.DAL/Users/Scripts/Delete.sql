UPDATE
    tc_user
SET deleted_time  = now(),
    modified_time = now()
WHERE id = @Id
RETURNING id;