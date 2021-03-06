select * from INFORMATION_SCHEMA.DOMAIN_CONSTRAINTS;

select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS;
select * from INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE;
select * from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE;
select * from INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS;



select * 
from INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE t
on tc.CONSTRAINT_CATALOG = t.CONSTRAINT_CATALOG
and tc.CONSTRAINT_SCHEMA = t.CONSTRAINT_SCHEMA
and tc.CONSTRAINT_NAME = t.CONSTRAINT_NAME
INNER JOIN  INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE c
on t.CONSTRAINT_CATALOG = c.CONSTRAINT_CATALOG
and t.CONSTRAINT_SCHEMA = c.CONSTRAINT_SCHEMA
and t.CONSTRAINT_NAME = c.CONSTRAINT_NAME
where tc.CONSTRAINT_TYPE = 'FOREIGN KEY'
order by t.TABLE_NAME, t.CONSTRAINT_NAME
