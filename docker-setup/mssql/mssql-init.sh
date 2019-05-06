#!/bin/bash

# wait for MSSQL server to start
sleep 15s

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -i /mssql/01-init-database.sql