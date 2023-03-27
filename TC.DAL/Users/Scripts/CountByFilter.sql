select COUNT(id)
from tc_user
WHERE deleted_time is null
--UserIds-- and id = any(:UserIds)
;